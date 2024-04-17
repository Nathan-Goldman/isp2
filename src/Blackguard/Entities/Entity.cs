using System.Numerics;

namespace Blackguard.Entities;

public struct Entity {
    public EntityDefinition Type;

    public Vector2 Position;
    public int Health;
    public int Mana;
    public int Speed;
}
