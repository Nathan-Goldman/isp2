using Blackguard.Utilities;

namespace Blackguard.Entities;

public struct Entity {
    public EntityDefinition Type;

    public Point Position;
    public readonly Point ChunkPosition => Position.ToChunkPosition();
    public int Health;
    public int Mana;
    public int Speed;

    public Entity(EntityDefinition type, Point position) {
        Type = type;
        Position = position;
    }

    public readonly bool Collides(Entity e) {
        return Utils.Intersect(Position.X, Position.Y, 1, 1, e.Position.X, e.Position.Y, e.Type.Width, e.Type.Height) > 0;
    }
}
