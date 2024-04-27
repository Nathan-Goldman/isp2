using System.IO;
using Blackguard.Utilities;

namespace Blackguard.Items;

public struct Item {
    public ItemDefinition Type;

    // Instance data
    public int Stack;
    public bool InWorld;
    public Point Position;

    public Item(ItemDefinition type, int stack = 1, bool inWorld = false, Point position = default) {
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
        Point position = new(r.ReadInt32(), r.ReadInt32());

        return new Item(Registry.GetDefinition<ItemDefinition>(id), stack, inWorld, position);
    }
}
