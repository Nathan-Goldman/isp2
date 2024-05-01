using System;
using System.Linq;
using Blackguard.UI.Elements;
using Blackguard.Utilities;
using Mindmagma.Curses;

namespace Blackguard.UI.Popups;

public class ConfirmationPopup : Popup {
    private readonly UIContainer container;

    public Highlight TextHighlight = Highlight.Text;
    public Highlight Border = Highlight.Text;

    public ConfirmationPopup(string[] contents, Action<Game>? cancel, Action<Game>? ok) : base(Highlight.Text, contents.Max(s => s.Length) + 2, contents.Length + 5) {
        container = new(Alignment.Center) {
            Border = true,
            BorderSel = Border,
            BorderUnsel = Border,
        };

        container.Add(new UIText(contents) { Highlight = TextHighlight });
        container.Add(new UISpace(0, 1));
        container.Add(new UIContainer(
            Alignment.Horizontal | Alignment.Fill,
            new UISpace(4, 0),
            new UIButton(["Cancel"], (s) => {
                cancel?.Invoke(s);
                s.ClosePopup(this);
            }),
            new UIButton(["Ok"], (s) => {
                ok?.Invoke(s);
                s.ClosePopup(this);
            }),
            new UISpace(4, 0)
        ));

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

        if (state.Input.KeyHit(CursesKey.LEFT))
            container.Prev(true);

        if (state.Input.KeyHit(CursesKey.RIGHT))
            container.Next(true);

        container.ProcessInput(state);

        return true;
    }
}
