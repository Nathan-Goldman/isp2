using System;
using System.Collections.Generic;
using System.IO;
using Blackguard.Entities;
using Blackguard.Tiles;
using Blackguard.UI;
using Blackguard.Utilities;
using Mindmagma.Curses;
using Newtonsoft.Json;

using SpawnCondition = System.Func<Blackguard.Game, bool>;

namespace Blackguard;

[JsonObject(MemberSerialization.OptIn)]
public class World {
    [JsonProperty]
    public string Name { get; private set; }

    [JsonProperty]
    public DateTime CreationDate { get; private set; }

    [JsonProperty]
    public TimeSpan Playtime { get; private set; }

    [JsonProperty]
    public int Seed { get; private set; }

    private WorldGen gen = null!;
    public string BaseSavePath => Path.Combine(Game.WorldsPath, Name);
    public string MetaSavePath => Path.Combine(BaseSavePath, "meta");
    public string ChunksPath => Path.Combine(BaseSavePath, "Chunks");

    public Dictionary<Point, Chunk> ChunksByPosition;
    public Point simulationDistance; // Number of chunks to simulate on all four sides of the player

    private List<(EntityDefinition, SpawnCondition)> spawnConditions = null!;

    public World(string name) {
        Name = name;
        CreationDate = DateTime.Now;
        Playtime = TimeSpan.Zero;
        ChunksByPosition = new();
    }

    public static World CreateNew(Game state, string name) {
        World world = new(name) {
            Seed = (int)DateTime.Now.Ticks // I sure hope this doesn't truncate
        };
        world.Serialize();
        world.Initialize(state);

        return world;
    }

    private void LoadChunks(Point center) {
        for (int i = -simulationDistance.X; i <= simulationDistance.X; i++) {
            for (int j = -simulationDistance.Y; j <= simulationDistance.Y; j++) {
                Point position = new(center.X + i, center.Y + j);

                if (!ChunksByPosition.ContainsKey(position))
                    ChunksByPosition.Add(position, Chunk.Deserialize(ChunksPath, position) ?? gen.GenChunk(position));
            }
        }
    }

    public void Initialize(Game state) {
        gen = new WorldGen(Seed);
        spawnConditions = new List<(EntityDefinition, SpawnCondition)>();

        foreach ((Type _, Registry.Definition def) in Registry.GetRegistry<EntityDefinition>().defsByType) {
            EntityDefinition eDef = (EntityDefinition)def;

            if (eDef.SpawnConditions != null) {
                foreach (SpawnCondition c in eDef.SpawnConditions)
                    spawnConditions.Add(item: (eDef, c));
            }
        }

        HandleTermResize();

        LoadChunks(state.Player.ChunkPosition);
    }

    public void RunTick(Game state) {
        // Remove faraway chunks
        foreach ((Point position, Chunk chunk) in ChunksByPosition) {
            if (Math.Abs(position.X - state.Player.ChunkPosition.Y) > simulationDistance.X || Math.Abs(position.Y - state.Player.ChunkPosition.Y) > simulationDistance.Y) {
                ChunksByPosition.Remove(key: position);
                chunk.Serialize(ChunksPath);
                state.Player.nearbyEntities -= chunk.Entities.Count;
            }
        }

        LoadChunks(state.Player.ChunkPosition);

        int netLoadedEntities = 0;

        foreach ((_, Chunk chunk) in ChunksByPosition) {
            for (int i = 0; i < chunk.Entities.Count; i++) {
                Entity e = chunk.Entities[i];

                Point oldpos = e.ChunkPosition;
                e.Type.AI(state, ref e);

                // Entity has moved into a different chunk
                if (oldpos != e.ChunkPosition) {
                    if (ChunksByPosition.TryGetValue(e.ChunkPosition, out Chunk? newChunk)) {
                        newChunk.Entities.Add(e);
                        netLoadedEntities++;
                    }

                    chunk.Entities.Remove(e);
                    netLoadedEntities--;
                }
            }
        }

        netLoadedEntities += SpawnEntitiesInWorld(state);

        state.Player.nearbyEntities += netLoadedEntities;
    }

    private readonly List<Chunk> chunksWithEntities = new();
    public void Render(Drawable drawable, Game state, int maxw, int maxh) {
        chunksWithEntities.Clear();

        foreach ((_, Chunk chunk) in ChunksByPosition) {
            Point sp = Utils.ToScreenPos(state.ViewOrigin, chunk.WorldPosition);
            if (!Utils.CheckOutOfBounds(sp.X, sp.Y, Chunk.CHUNKSIZE, Chunk.CHUNKSIZE, maxw, maxh, out int byX, out int byY))
                chunk.Render(drawable, sp.X, sp.Y, 0, 0, state.drawChunkOutline);
            else if (Math.Abs(byX) < Chunk.CHUNKSIZE && Math.Abs(byY) < Chunk.CHUNKSIZE)
                chunk.Render(drawable, sp.X, sp.Y, byX, byY, state.drawChunkOutline);

            if (chunk.EntitesToRender)
                chunksWithEntities.Add(chunk);
        }

        // Render entities after rendering chunks to avoid entities being painted over by tiles
        foreach (Chunk chunk in chunksWithEntities) {
            for (int i = 0; i < chunk.Entities.Count; i++) {
                Entity e = chunk.Entities[i];

                Point sp = Utils.ToScreenPos(state.ViewOrigin, e.Position);
                if (!Utils.CheckOutOfBounds(sp.X, sp.Y, e.Type.Width, e.Type.Height, maxw, maxh, out int byX, out int byY))
                    e.Type.Render(ref e, drawable, sp.X, sp.Y, 0, 0);
                else if (Math.Abs(byX) < e.Type.Width && Math.Abs(byY) < e.Type.Height)
                    e.Type.Render(ref e, drawable, sp.X, sp.Y, byX, byY);
            }
        }
    }

    public Tile? GetTile(Point position) {
        Point chunkPosition = position.ToChunkPosition();

        if (!ChunksByPosition.TryGetValue(chunkPosition, out Chunk? value))
            return null;

        return value.Tiles[position.X - value.Position.X * Chunk.CHUNKSIZE, position.Y - value.Position.Y * Chunk.CHUNKSIZE];
    }

    public void Serialize() {
        Directory.CreateDirectory(BaseSavePath);
        Directory.CreateDirectory(ChunksPath);

        string json = JsonConvert.SerializeObject(this);

        File.WriteAllText(MetaSavePath, json);

        foreach ((_, Chunk chunk) in ChunksByPosition)
            chunk.Serialize(ChunksPath);
    }

    public static World? Deserialize(string path) {
        string json = File.ReadAllText(path);

        return JsonConvert.DeserializeObject<World>(json);
    }

    public void Delete() {
        Directory.Delete(BaseSavePath, true);
    }

    public void HandleTermResize() {
        simulationDistance.X = NCurses.Columns / Chunk.CHUNKSIZE * 2;
        simulationDistance.Y = NCurses.Lines / Chunk.CHUNKSIZE * 2;
    }

    // This isn't uniform. I couldn't find how to make it uniform
    private Point RandPosOffscreen(Game state) {
        int side = Game.Rand.Next(3);

        // Offsets from the player's position
        int offX = 0;
        int offY = 0;

        // All of these undershoot their maximums by somewhere between 0 and 1 chunk. Oh well
        switch (side) {
            case 0: // Left
                offX = Game.Rand.Next(-simulationDistance.X * Chunk.CHUNKSIZE, -NCurses.Columns / 2);
                offY = Game.Rand.Next(-simulationDistance.Y, simulationDistance.Y);
                break;
            case 1: // Top
                offX = Game.Rand.Next(-NCurses.Columns / 2, NCurses.Columns / 2);
                offY = Game.Rand.Next(NCurses.Lines / 2, simulationDistance.Y * Chunk.CHUNKSIZE);
                break;
            case 2: // Right
                offX = Game.Rand.Next(NCurses.Columns / 2, simulationDistance.X * Chunk.CHUNKSIZE);
                offY = Game.Rand.Next(-simulationDistance.Y, simulationDistance.Y);
                break;
            case 3: // Bottom
                offX = Game.Rand.Next(-NCurses.Columns / 2, NCurses.Columns / 2);
                offY = Game.Rand.Next(-NCurses.Lines / 2, -simulationDistance.Y * Chunk.CHUNKSIZE);
                break;
        }

        return new(state.Player.Position.X + offX, state.Player.Position.Y + offY);
    }

    public int SpawnEntitiesInWorld(Game state) {
        int numSpawned = 0;
        int spawnRate = 600;
        int maxSpawns = 5;

        // Check nearby < maxSpawns
        if (state.Player.nearbyEntities > maxSpawns || Game.Rand.Next(spawnRate) != 0)
            return 0;

        foreach ((EntityDefinition eDef, SpawnCondition condition) in spawnConditions) {
            if (condition(state)) {
                Point pos = RandPosOffscreen(state);
                ChunksByPosition[pos.ToChunkPosition()].Entities.Add(new(eDef, pos));
            }
        }

        return numSpawned;
    }
}
