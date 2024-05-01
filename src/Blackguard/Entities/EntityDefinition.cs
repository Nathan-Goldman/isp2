using Blackguard.UI;
using Blackguard.Utilities;
using SpawnCondition = System.Func<Blackguard.Game, bool>;

namespace Blackguard.Entities;

public abstract class EntityDefinition : Registry.Definition {
    public char Glyph = 'u';
    public int Width = 1;
    public int Height = 1;

    public int MaxMana;
    public int MaxHealth;
    public int MaxSpeed;
    public int Damage;
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

    public int DataSize = 0;
    public int DataFSize = 0;

    public SpawnCondition[]? SpawnConditions;

    // Methods will be implemented later as needed
    public virtual void Render(ref Entity e, Drawable drawable, int x, int y, int skipx = 0, int skipy = 0) {
        // Default impl just draws the entity's glyph. Since it's a single char skipx and skipy can be ignored
        drawable.AddCharWithHighlight(Highlight.Text, x, y, Glyph);
    }

    public virtual void AI(Game state, ref Entity e) {
        if (state.Player.Collides(e)) {
            state.Player.Damage(ref e, Damage, 60);
        }
    }
}
