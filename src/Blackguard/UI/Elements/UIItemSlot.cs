using Blackguard.Items;
using Blackguard.Utilities;

namespace Blackguard.UI.Elements;

public class UIItemSlot : UIElement, ISelectable {
    public Item Item { get; set; }
    public int InventoryIndex;

    public Highlight Border = Highlight.Text;
    public Highlight BorderSel = Highlight.TextSel;

    public bool Selected { get; set; }

    public UIItemSlot(Item item, int invIdx = 0) {
        Item = item;
        InventoryIndex = invIdx;
    }

    public override (int w, int h) GetSize() {
        return (30, 3);
    }

    public override void Render(Drawable drawable, int x, int y, int maxw, int maxh) {
        drawable.DrawBorder(Selected ? BorderSel : Border, x, y, 30, 3);

        if (Item.Type.Id == Registry.GetId<Empty>()) {
            drawable.AddLineWithHighlight(Highlight.Text, x + 1, y + 1, new string(' ', 28));
            return;
        }

        drawable.AddCharWithHighlight(Item.Type.GlyphHighlight, x + 2, y + 1, Item.Type.Glyph);
        drawable.AddLineWithHighlight(Item.Type.NameHighlight, x + 5, y + 1, Item.Type.Name);
    }
}
