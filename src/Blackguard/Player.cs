using System;
using System.IO;
using System.Numerics;
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

    // Stats
    public int MaxMana = 100;
    public int MaxHealth = 100;
    public int MaxSpeed;
    public double BluntEffect;
    public double SlashEffect;
    public double PierceEffect;
    public double MagicEffect;
    public double BaseEffect;
    public double FireEffect;
    public double ElectricityEffect;
    public double IceEffect;
    public double WaterEffect;
    public double EarthEffect;
    public double MindEffect;
    public int Health;
    public int Mana;
    public int Speed;

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
    }

    public void Render(Drawable drawable, int x, int y) {
        drawable.AddLinesWithHighlight((Highlight, x, y, Glyph));
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
