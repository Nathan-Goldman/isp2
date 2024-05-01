namespace Blackguard.Entities;

public class MimicDoor : EntityDefinition {
    public MimicDoor() {
        Glyph = 'D';
        Name = "Mimic Door";

        MaxHealth = 100;
        MaxMana = 50;
        MaxSpeed = 80;
        BluntEffect = 1.0;
        SlashEffect = 1.5;
        PierceEffect = 1.0;
        MagicEffect = 1.0;
        BaseEffect = 1.2;
        FireEffect = 1.5;
        ElectricityEffect = 0;
        IceEffect = 1.0;
        WaterEffect = .5;
        EarthEffect = 1.0;
        MindEffect = 0;
    }
}
