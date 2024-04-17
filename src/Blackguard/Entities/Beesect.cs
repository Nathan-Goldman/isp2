namespace Blackguard.Entities;

public class Beesect : EntityDefinition {
    public Beesect() {
        Glyph = 'B';
        Name = "Beesect";

        MaxHealth = 50;
        MaxMana = 60;
        MaxSpeed = 120;
        BluntEffect = 1.5;
        SlashEffect = 1.5;
        PierceEffect = .5;
        MagicEffect = .5;
        BaseEffect = .9;
        FireEffect = 3;
        ElectricityEffect = 1;
        IceEffect = 2;
        WaterEffect = 1;
        EarthEffect = 1;
        MindEffect = 1;
    }
}
