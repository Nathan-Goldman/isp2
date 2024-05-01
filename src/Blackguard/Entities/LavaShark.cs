namespace Blackguard.Entities;

public class LavaShark : EntityDefinition {
    public LavaShark() {
        Glyph = 'L';
        Name = "Lava Shark";

        MaxHealth = 150;
        MaxMana = 50;
        MaxSpeed = 100;
        BluntEffect = .75;
        SlashEffect = 1.0;
        PierceEffect = 1.0;
        MagicEffect = 1.0;
        BaseEffect = 1.0;
        FireEffect = 0;
        ElectricityEffect = .25;
        IceEffect = 2.0;
        WaterEffect = 1.5;
        EarthEffect = 1.0;
        MindEffect = 1.0;
    }
}
