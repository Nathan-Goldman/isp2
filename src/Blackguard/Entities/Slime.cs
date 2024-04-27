namespace Blackguard.Entities;

public class Slime : EntityDefinition {
    public Slime() {
        Glyph = 'S';
        Name = "Slime";

        MaxHealth = 75;
        MaxMana = 50;
        MaxSpeed = 80;
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

        SpawnConditions = [(state) => {
            return true;
        }];
    }
}
