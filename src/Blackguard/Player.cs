using System;
using System.IO;
using System.Numerics;
using Blackguard.Entities;
using Blackguard.Items;
using Blackguard.Tiles;
using Blackguard.UI;
using Blackguard.UI.Popups;
using Blackguard.UI.Scenes;
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
    public Point ChunkPosition => Position.ToChunkPosition();
    public Vector2 OldPosition;
    // Velocity is measured in tiles per tick
    public Vector2 Velocity;
    public float maxVelocity = 0.5f;
    public int nearbyEntities = 0;
    public int IFrames = 0;
    public int RegenDelay = 0;

    // Stats
    public int MaxHealth = 100;
    private float health;
    public float Health {
        get {
            return health;
        }
        set {
            health = Math.Clamp(value, int.MinValue, MaxHealth);
        }
    }
    public float HealthRegenPerTick = 0.05f;
    public int MaxMana = 100;
    private float mana;
    public float Mana {
        get {
            return mana;
        }
        set {
            mana = Math.Clamp(value, 0, MaxMana);
        }
    }
    public float ManaRegenPerTick = 0.05f;

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

        Health = MaxHealth;
        Mana = MaxMana;
    }

    public void RunTick(Game state) {
        if (Health < 0) {
            state.OpenPopup(
                new InfoPopup(InfoType.Info,
                [
                    "               You died!              ",
                    "Your player and world will be deleted."
                ],
                (s) => {
                    s.ForwardScene<MainMenuScene>();
                    s.Player.Delete();
                    s.Player = null!;
                    s.World.Delete();
                    s.World = null!;
                }
            ),
            true);
        }

        InputHandler input = state.Input;

        bool left = input.KeyHeld('a');
        bool right = input.KeyHeld('d');
        bool up = input.KeyHeld('w');
        bool down = input.KeyHeld('s');
        bool noWASD = !left && !right && !up && !down;
        float accel = 0.01f;

        if (up && !down) {
            if (Velocity.Y > 0f)
                Velocity.Y *= 0.9f;

            Velocity.Y -= accel;
            if (Velocity.Y < -maxVelocity)
                Velocity.Y = -maxVelocity;
        }
        else if (down && !up) {
            if (Velocity.Y < 0f)
                Velocity.Y *= 0.9f;

            Velocity.Y += accel;
            if (Velocity.Y > maxVelocity)
                Velocity.Y = maxVelocity;
        }
        else if (Velocity.Y < -0.1 || Velocity.Y > 0.1) {
            Velocity.Y *= 0.9f;
        }
        else {
            Velocity.Y = 0f;
        }

        if (left && !right) {
            if (Velocity.X > 0f)
                Velocity.X *= 0.9f;

            Velocity.X -= accel;
            if (Velocity.X < -maxVelocity)
                Velocity.X = -maxVelocity;
        }
        else if (right && !left) {
            if (Velocity.X < 0f)
                Velocity.X *= 0.9f;

            Velocity.X += accel;
            if (Velocity.X > maxVelocity)
                Velocity.X = maxVelocity;
        }
        else if (Velocity.X < -accel || Velocity.X > accel) {
            Velocity.X *= 0.9f;
        }
        else {
            Velocity.X = 0f;
        }

        // This is the best that can be done unless a better input solution is found.
        bool inc1 = false;
        int incx = 0;
        int incy = 0;
        if (noWASD) {
            bool left1 = input.KeyHit('a');
            bool right1 = input.KeyHit('d');
            bool up1 = input.KeyHit('w');
            bool down1 = input.KeyHit('s');

            if (left1 && !right1) {
                incx = -1;
                inc1 = true;
            }
            else if (right1 && !left1) {
                incx = 1;
                inc1 = true;
            }

            if (up1 && !down1) {
                incy = -1;
                inc1 = true;
            }
            else if (down1 && !up1) {
                incy = 1;
                inc1 = true;
            }
        }

        if (Velocity.X != 0 || Velocity.Y != 0 || inc1) {
            float nPosX = Position.X + Velocity.X + incx;
            float nPosY = Position.Y + Velocity.Y + incy;

            Tile? next = state.World.GetTile(new Point((int)nPosX, (int)nPosY));
            if (next is not null && !next.Value.Foreground) {
                OldPosition = Position;
                Position.X = nPosX;
                Position.Y = nPosY;
                state.ViewOrigin.X = (int)Position.X - NCurses.Columns / 2;
                state.ViewOrigin.Y = (int)Position.Y - NCurses.Lines / 2;
            }
        }

        if (input.KeyHit('e'))
            state.OpenPopup(new InventoryPopup(Inventory), true);

        if (input.KeyHit('p'))
            state.OpenPopup(new PausePopup(), true);

        if (IFrames > 0)
            IFrames--;

        if (RegenDelay > 0)
            RegenDelay--;

        if (RegenDelay == 0)
            Health += HealthRegenPerTick;

        Mana += ManaRegenPerTick;
    }

    public void Render(Drawable drawable, int x, int y) {
        drawable.AddLinesWithHighlight(segments: (Highlight, x, y, Glyph));
    }

    public void Damage(ref Entity e, int amount, int iFrames, bool knockback = true) {
        if (IFrames > 0)
            return;

        Health -= amount;
        IFrames = iFrames;
        RegenDelay = iFrames * 2;

        if (!knockback)
            return;

        // TODO: Modify damage amount based on modifiers. Needs to be discussed further

        if (e.Velocity.X == 0 && e.Velocity.Y == 0) {
            if (Velocity.X == 0 && Velocity.Y == 0)
                Velocity = OldPosition.DirectionTo(Position).Normalize() * -0.75f;
            else
                Velocity = Velocity.Normalize() * -0.75f;
        }
        else
            Velocity = e.Velocity.Normalize() * 0.75f;
    }

    public bool Collides(Entity e) {
        return Utils.Intersect(Position.X, Position.Y, 1, 1, e.Position.X, e.Position.Y, e.Type.Width, e.Type.Height) > 0;
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
        state.ViewOrigin = new Point(
            (int)Position.X - NCurses.Columns / 2,
            (int)Position.Y - NCurses.Lines / 2
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
