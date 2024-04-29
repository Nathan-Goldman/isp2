using System.Numerics;
using Blackguard.Utilities;

namespace Blackguard.Entities;

public struct Entity {
    public EntityDefinition Type;

    public Vector2 Position;
    public Vector2 Velocity;
    public readonly Point ChunkPosition => Position.ToChunkPosition();
    public int Health;
    public int Mana;
    public int Speed;
    public int[] Data = null!;
    public float[] DataF = null!;

    public Entity(EntityDefinition type, Vector2 position) {
        Type = type;
        Position = position;

        if (type.DataSize > 0)
            Data = new int[type.DataSize];

        if (type.DataFSize > 0)
            DataF = new float[type.DataFSize];
    }

    public readonly bool Collides(Entity e) {
        return Utils.Intersect(Position.X, Position.Y, 1, 1, e.Position.X, e.Position.Y, e.Type.Width, e.Type.Height) > 0;
    }
}
