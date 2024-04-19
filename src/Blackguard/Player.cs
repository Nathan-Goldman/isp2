using System;
using System.IO;
using System.Numerics;
using Blackguard.Entities;
using Blackguard.Items;
using Blackguard.Tiles;
using Blackguard.UI;
using Blackguard.UI.Popups;
using Blackguard.Utilities;
using Mindmagma.Curses;
using static Blackguard.Game;

namespace Blackguard;

public class Player : ISizeProvider {
    // Serialized state
    public string Name { get; private set; }
    public DateTime CreationDate { get; private set; }
    public TimeSpan Playtime { get; private set; }
    public PlayerType PlayerType;
    public RaceType Race;
    public Vector2 Position;
    public Item[] Inventory;

    // Other stuff
    public string SavePath => Path.Combine(PlayersPath, Name + ".plr");
    public string Glyph { get; private set; }
    public Highlight Highlight { get; private set; }
    public Vector2 ChunkPosition => new((float)Math.Floor(Position.X / Chunk.CHUNKSIZE), (float)Math.Floor(Position.Y / Chunk.CHUNKSIZE));

    public int IFrames = 0;

    // Stats
    public int MaxHealth = 100;
    public int Health = 100;
    public int MaxMana = 100;
    public float Mana = 100;
    public float ManaRegenPerTick = 0.05f;
    public int MaxSpeed;
    public int Speed;

    // Damage and defense modifiers
    public double BluntEffect = 1f;
    public double SlashEffect = 1f;
    public double PierceEffect = 1f;
    public double MagicEffect = 1f;
    public double BaseEffect = 1f;
    public double FireEffect = 1f;
    public double ElectricityEffect = 1f;
    public double IceEffect = 1f;
    public double WaterEffect = 1f;
    public double EarthEffect = 1f;
    public double MindEffect = 1f;
    public double BluntResist = 1f;
    public double SlashResist = 1f;
    public double PierceResist = 1f;
    public double MagicResist = 1f;
    public double BaseResist = 1f;
    public double FireResist = 1f;
    public double ElectricityResist = 1f;
    public double IceResist = 1f;
    public double WaterResist = 1f;
    public double EarthResist = 1f;
    public double MindResist = 1f;

    public Player(string name, PlayerType type, RaceType race) {
        Name = name;
        Inventory = new Item[30];
        Array.Fill(Inventory, new Item(Registry.GetDefinition<Empty>()));
        CreationDate = DateTime.Now;
        Playtime = TimeSpan.Zero;
        PlayerType = type;
        Race = race;
        Glyph = "#";
    }

    public static Player CreateNew(string name, PlayerType type, RaceType race) {
        Player player = new(name, type, race);
        player.Inventory[2] = new Item(Registry.GetDefinition<Items.Dirt>(), 24);

        player.Serialize();

        return player;
    }

    public void Initialize(Game state) {
        HandleTermResize(state);
    }

    public void RunTick(Game state) {
        ProcessInput(state);
    }

    private void ProcessInput(Game state) {
        InputHandler input = state.Input;

        int changeX = 0;
        int changeY = 0;

        if (input.KeyPressed('w'))
            changeY--;

        if (input.KeyPressed('a'))
            changeX--;

        if (input.KeyPressed('s'))
            changeY++;

        if (input.KeyPressed('d'))
            changeX++;

        if (changeX != 0 || changeY != 0) {
            int nPosX = (int)Position.X + changeX;
            int nPosY = (int)Position.Y + changeY;

            Tile? next = state.World.GetTile(new Vector2(nPosX, nPosY));
            if (next is not null && !next.Value.Foreground) {
                Position.X = nPosX;
                Position.Y = nPosY;
                state.ViewOrigin.X += changeX;
                state.ViewOrigin.Y += changeY;
            }
        }

        if (input.KeyPressed('e'))
            state.OpenPopup(new InventoryPopup(Inventory), true);

        if (input.KeyPressed('p'))
            state.OpenPopup(new PausePopup(), true);

        if (IFrames > 0)
            IFrames--;

        Mana += ManaRegenPerTick;
    }

    public void Render(Drawable drawable, int x, int y) {
        drawable.AddLinesWithHighlight(segments: (Highlight, x, y, Glyph));
    }

    public void Damage(int amount, int iFrames) {
        if (IFrames > 0)
            return;

        // TODO: Modify damage amount based on modifiers. Needs to be discussed further

        Health -= amount;
        IFrames = iFrames;
    }

    public bool Collides(Entity e) {
        return Utils.Intersect((int)Position.X, (int)Position.Y, 1, 1, (int)e.Position.X, (int)e.Position.Y, e.Type.Width, e.Type.Height) > 0;
    }

    public void Serialize() {
        using FileStream fs = new(SavePath, FileMode.OpenOrCreate);
        using BinaryWriter w = new(fs);

        w.Write(Name);
        w.Write(CreationDate.ToBinary());
        w.Write(Playtime.Ticks);
        w.Write((int)PlayerType);
        w.Write((int)Race);
        w.Write(Position.X);
        w.Write(Position.Y);

        foreach (Item item in Inventory)
            item.Serialize(w);
    }

    public static Player? Deserialize(string path) {
        using FileStream fs = new(path, FileMode.Open);
        using BinaryReader r = new(fs);

        string name = r.ReadString();
        DateTime creationDate = DateTime.FromBinary(r.ReadInt64());
        TimeSpan playtime = new(r.ReadInt64());
        PlayerType type = (PlayerType)r.ReadInt32();
        RaceType race = (RaceType)r.ReadInt32();
        Vector2 position = new(r.ReadSingle(), r.ReadSingle());
        Item[] inventory = new Item[30];

        for (int i = 0; i < 30; i++)
            inventory[i] = Item.Deserialize(r);

        return new Player(name, type, race) {
            CreationDate = creationDate,
            Playtime = playtime,
            Position = position,
            Inventory = inventory,
        };
    }

    public void Delete() {
        File.Delete(SavePath);
    }

    public (int w, int h) GetSize() {
        return (1, 1); // May be expanded eventually depending on how the player is rendered
    }

    public void HandleTermResize(Game state) {
        state.ViewOrigin = new Vector2(
            (int)(Position.X - NCurses.Columns / 2),
            (int)(Position.Y - NCurses.Lines / 2)
        );
    }

}

public enum PlayerType {
    Knight,
    Archer,
    Mage,
    Barbarian,
}

public enum RaceType {
    Human,
    Ork,
    Elf,
    Dwarf,
    Demon,
    Gnome,
}
