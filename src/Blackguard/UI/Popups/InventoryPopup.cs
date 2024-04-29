using Blackguard.Items;
using Blackguard.UI.Elements;
using Blackguard.Utilities;
using Mindmagma.Curses;

namespace Blackguard.UI.Popups;

public class InventoryPopup : Popup {
    private readonly UIContainer container;

    private readonly Item[] _inventory;

    public Highlight Border = Highlight.Text;

    private UIItemSlot? tempSlot = null;
    private int tempIdx;
    private Highlight tempHighlight;

    public InventoryPopup(Item[] inventory) : base(Highlight.Text, 3, 3, 62, 47) {
        _inventory = inventory;

        container = new(Alignment.Left) {
            Border = true,
            BorderSel = Border,
        };

        UIContainer innerContainer = null!;
        for (int i = 0; i < inventory.Length; i++) {
            if (i % 2 == 0) {
                innerContainer = new(Alignment.Horizontal);
                container.Add(innerContainer);
            }

            innerContainer.Add(new UIItemSlot(inventory[i], i));
        }

        // Weird little hack to select the first element. Look into later.
        container.Next(true);
        container.Prev(true);
    }

    // Terminal should not be able to get small enough to cause issues with the inventory
    public override void HandleTermResize() { }

    public override bool RunTick(Game state) {
        if (!Focused)
            return true;

        if (state.Input.KeyHit(CursesKey.RIGHT))
            container.Next(true);

        if (state.Input.KeyHit(CursesKey.LEFT))
            container.Prev(true);

        if (state.Input.KeyHit(CursesKey.UP)) {
            container.Prev(true);
            container.Prev(true);
        }

        if (state.Input.KeyHit(CursesKey.DOWN)) {
            container.Next(true);
            container.Next(true);
        }

        if (state.Input.IsEnterPressed()) {
            // Prepare for swap
            UIContainer inner = (container.GetSelectedElement() as UIContainer)!;
            UIItemSlot selected = (inner.GetSelectedElement() as UIItemSlot)!;
            if (tempSlot == null && selected.Item.Type.Id != Registry.GetId<Empty>()) {
                tempSlot = selected;
                tempIdx = selected.InventoryIndex;
                tempHighlight = selected.Border;
                selected.Border = Highlight.TextWarning; // Temp
            }
            // Finish swap
            else if (tempSlot != null) {
                _inventory[tempIdx] = selected.Item;
                _inventory[selected.InventoryIndex] = tempSlot.Item;
                (tempSlot.Item, selected.Item) = (selected.Item, tempSlot.Item);
                tempSlot.Border = tempHighlight;

                tempSlot = null;
            }
        }

        if (state.Input.KeyHit(CursesKey.BACKSPACE)) {
            if (tempSlot != null) {
                tempSlot.Border = tempHighlight;
                tempSlot = null;
            }
        }

        if (state.Input.KeyHit('e')) {
            state.ClosePopup(this);
        }

        return true;
    }

    public override void Render(Game state) {
        container.Render(Panel, 0, 0, Panel.w, Panel.h);
    }
}
