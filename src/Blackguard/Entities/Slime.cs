using System.Numerics;
using Blackguard.Utilities;

namespace Blackguard.Entities;

public class Slime : EntityDefinition {
    public Slime() {
        Glyph = 'S';
        Name = "Slime";

        MaxHealth = 75;
        MaxMana = 50;
        MaxSpeed = 80;
        Damage = 20;
        BluntEffect = .75;
        SlashEffect = 1.5;
        PierceEffect = 1.0;
        MagicEffect = 1.0;
        BaseEffect = 1.0;
        FireEffect = .5;
        ElectricityEffect = 1.25;
        IceEffect = 1.0;
        WaterEffect = .5;
        EarthEffect = 1.0;
        MindEffect = 1.5;

        DataSize = 1;

        SpawnConditions = [(state) => {
            return true;
        }];
    }

    public override void AI(Game state, ref Entity e) {
        base.AI(state, ref e);

        Player player = state.Player;

        float cutoff = 0.01f;
        if (e.Velocity.X < -cutoff || e.Velocity.X > cutoff)
            e.Velocity.X *= 0.9f;
        else
            e.Velocity.X = 0;

        if (e.Velocity.Y < -cutoff || e.Velocity.Y > cutoff)
            e.Velocity.Y *= 0.9f;
        else
            e.Velocity.Y = 0;

        e.Position.X += e.Velocity.X;
        e.Position.Y += e.Velocity.Y;
        e.Data[0]++; // Timer

        if (e.Data[0] > 120)
            e.Data[0] = 0;
        else
            return;

        Vector2 direction = e.Position.DirectionFrom(player.Position).Normalize();
        e.Velocity = direction * 0.7f;
    }
}
