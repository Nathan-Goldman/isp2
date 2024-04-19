using System.Numerics;
using Blackguard.Utilities;

namespace Blackguard.Entities;

public struct Entity {
    public EntityDefinition Type;

    public Vector2 Position;
    public int Health;
    public int Mana;
    public int Speed;

    public readonly bool Collides(Entity e) {
        return Utils.Intersect((int)Position.X, (int)Position.Y, 1, 1, (int)e.Position.X, (int)e.Position.Y, e.Type.Width, e.Type.Height) > 0;
    }
}
