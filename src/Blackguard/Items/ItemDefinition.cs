using Blackguard.Utilities;

namespace Blackguard.Items;

public abstract class ItemDefinition : Registry.Definition {
    // Static defaults
    public char Glyph = 'u';
    public int MaxStack;
    public Highlight GlyphHighlight;
    public Highlight NameHighlight;
}
