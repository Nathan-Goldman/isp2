using System.IO;
using System.Numerics;

namespace Blackguard.Items;

public struct Item {
    public ItemDefinition Type;

    // Instance data
    public int Stack;
    public bool InWorld;
    public Vector2 Position;

    public Item(ItemDefinition type, int stack = 1, bool inWorld = false, Vector2 position = default) {
        Type = type;
        Stack = stack;
        InWorld = inWorld;
        Position = position;
    }

    public readonly void Serialize(BinaryWriter w) {
        w.Write(Type.Id);
        w.Write(Stack);
        w.Write(InWorld);
        w.Write(Position.X);
        w.Write(Position.Y);
    }

    public static Item Deserialize(BinaryReader r) {
        int id = r.ReadInt32();
        int stack = r.ReadInt32();
        bool inWorld = r.ReadBoolean();
        Vector2 position = new(r.ReadSingle(), r.ReadSingle());

        return new Item(Registry.GetDefinition<ItemDefinition>(id), stack, inWorld, position);
    }
}
