using Blackguard.UI.Elements;
using Blackguard.UI.Scenes;
using Blackguard.Utilities;
using Mindmagma.Curses;

namespace Blackguard.UI.Popups;

public class PausePopup : Popup {
    private readonly UIContainer container;

    public PausePopup() : base(Highlight.Text, 79, 14) {
        container = new(Alignment.Center) {
            Border = true,
        };

        container.Add(new UIButton("Resume".ToLargeText(), (s) => s.ClosePopup(this)));
        container.Add(new UIButton("Save".ToLargeText(), (s) => {
            s.Save(true);
        }));
        container.Add(new UIButton("Save and Exit".ToLargeText(), (s) => {
            s.ClosePopup(this);
            s.Save(false);
            s.ForwardScene<MainMenuScene>();
        }));

        container.Select();
        container.SelectFirstSelectable();
    }

    public override void HandleTermResize() {
        CenterPopup();
    }

    public override bool RunTick(Game state) {
        if (!Focused)
            return true;

        if (state.Input.KeyPressed(CursesKey.UP))
            container.Prev(true);

        if (state.Input.KeyPressed(CursesKey.DOWN))
            container.Next(true);

        if (state.Input.KeyPressed('p'))
            state.ClosePopup(this);

        container.ProcessInput(state);

        return true;
    }

    public override void Render(Game state) {
        container.Render(Panel, 0, 0, Panel.w, Panel.h);
    }
}
