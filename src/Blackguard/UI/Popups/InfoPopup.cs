using System;
using System.Linq;
using Blackguard.UI.Elements;
using Blackguard.Utilities;
using Mindmagma.Curses;

namespace Blackguard.UI.Popups;

public enum InfoType {
    Info,
    Warning,
    Error
}

public class InfoPopup : Popup {
    private readonly UIContainer container;
    private readonly Highlight highlight;

    public InfoPopup(InfoType type, string[] contents, Action<Game>? callback = null) : base(Highlight.Text, contents.Max(s => s.Length) + 2, contents.Length + 4) {
        highlight = type switch {
            InfoType.Info => Highlight.Text,
            InfoType.Warning => Highlight.TextWarning,
            InfoType.Error => Highlight.TextError,
            _ => Highlight.Text,
        };

        container = new(Alignment.Center) {
            Border = true,
            BorderSel = highlight,
            BorderUnsel = highlight,
        };

        container.Add(new UIText(contents, Alignment.Center) { Highlight = highlight });
        container.Add(new UISpace(0, 1));
        container.Add(new UIButton(["Ok"], (s) => {
            callback?.Invoke(s);
            s.ClosePopup(this);
        }));

        container.Select();
        container.SelectFirstSelectable();
    }

    public override void HandleTermResize() => CenterPopup();

    public override void Render(Game state) {
        container.Render(Panel, 0, 0, Panel.w, Panel.h);
    }

    public override bool RunTick(Game state) {
        if (!Focused)
            return true;

        if (state.Input.KeyHit(CursesKey.DOWN))
            container.Next(true);

        if (state.Input.KeyHit(CursesKey.UP))
            container.Prev(true);

        container.ProcessInput(state);

        return true;
    }
}
