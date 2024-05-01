using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Blackguard.Entities;
using Blackguard.Tiles;
using Blackguard.UI;
using Blackguard.Utilities;

namespace Blackguard;

public class Chunk {
    public const int CHUNKSIZE = 20;
    public readonly Point Position;
    public Point WorldPosition => Position.ToWorldPosition();

    public Tile[,] Tiles = new Tile[CHUNKSIZE, CHUNKSIZE];
    private List<Entity> backingEntities = null!;
    public List<Entity> Entities {
        get {
            backingEntities ??= new();

            return backingEntities;
        }
        private set {
            backingEntities = value;
        }
    }

    public bool EntitesToRender => backingEntities != null && backingEntities.Count > 0;

    public Chunk(Point position) {
        Position = position;
    }

    public void Render(Drawable drawable, int x, int y, int skipx = 0, int skipy = 0, bool border = false) {
        int endi = skipx < 0 ? CHUNKSIZE + skipx : CHUNKSIZE;
        int endj = skipy < 0 ? CHUNKSIZE + skipy : CHUNKSIZE;

        for (int i = skipx > 0 ? skipx : 0; i < endi; i++) {
            for (int j = skipy > 0 ? skipy : 0; j < endj; j++) {
                Tile t = Tiles[i, j];
                drawable.AddCharWithHighlight(t.Type.Highlight, x + i, y + j, t.Type.Glyph);
            }
        }

        if (border)
            drawable.DrawBorder(Highlight.TextError, x, y, CHUNKSIZE, CHUNKSIZE, skipx, skipy);
    }


    public void Serialize(string basePath) {
        using FileStream fs = new(Path.Combine(basePath, $"{Position.X}:{Position.Y}.chunk"), FileMode.OpenOrCreate);
        using BinaryWriter w = new(fs);

        foreach (Tile t in Tiles) {
            w.Write(t.Type.Id);
            w.Write(t.Foreground);
        }
    }

    public static Chunk? Deserialize(string basePath, Point position) {
        string path = Path.Combine(basePath, $"{position.X}:{position.Y}.chunk");
        if (!File.Exists(path))
            return null;

        using FileStream fs = new(path, FileMode.Open);
        using BinaryReader r = new(fs);

        Chunk chunk = new(position);

        for (int i = 0; i < CHUNKSIZE; i++) {
            for (int j = 0; j < CHUNKSIZE; j++) {
                int id = r.ReadInt32();
                bool fg = r.ReadBoolean();

                chunk.Tiles[i, j] = new Tile(Registry.GetDefinition<TileDefinition>(id), fg);
            }
        }

        return chunk;
    }
}
