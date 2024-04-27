using Blackguard.UI.Popups;
using Blackguard.Utilities;

namespace Blackguard.UI.Scenes;

public class GameScene : Scene {
    public override void Initialize(Game state) {
        state.OpenPopup(new StatsPopup(state.Player));
    }

    public override bool RunTick(Game state) {
        if (!Focused)
            return true;

        state.Player.RunTick(state);
        state.World.RunTick(state);

        return true;
    }

    public override void Render(Game state) {
        Player player = state.Player;
        Point screenPos = Utils.ToScreenPos(state.ViewOrigin, player.Position);

        state.World.Render(state.CurrentPanel, state, state.CurrentPanel.w, state.CurrentPanel.h);

        state.Player.Render(state.CurrentPanel, screenPos.X, screenPos.Y);
    }

    public override void Close(Game state) {
        state.ClosePopupsByType<StatsPopup>();
    }
}
