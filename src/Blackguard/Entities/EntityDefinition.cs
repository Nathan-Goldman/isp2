namespace Blackguard.Entities;

public abstract class EntityDefinition : Registry.Definition {
    public char Glyph = 'u';
    public int Width = 1;
    public int Height = 1;

    public int MaxMana;
    public int MaxHealth;
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

    // Methods will be implemented later as needed
    public virtual void Render() { }

    public virtual void AI() { }
}
