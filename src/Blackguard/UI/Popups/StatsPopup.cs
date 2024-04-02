using Blackguard.Utilities;
using Mindmagma.Curses;

namespace Blackguard.UI.Popups;

public class StatsPopup : Popup {
    private readonly Player _player;

    public StatsPopup(Player player) : base(Highlight.Text, NCurses.Columns - 32, 1, 30, 4) {
        _player = player;
    }

    public override void HandleTermResize() {
        Panel.Move(NCurses.Columns - 32, 1);
    }

    public override void Render(Game state) {
        Panel.DrawBorder(Highlight.Text);

        Panel.AddLinesWithHighlight(
            (Highlight.TextWarning, 1, 1, $"Health: {_player.Health}/{_player.MaxHealth}"),
            (Highlight.Mana, 1, 2, $"Mana: {_player.Mana}/{_player.MaxMana}")
        );
    }

    public override bool RunTick(Game state) {
        return true; // Nothing to do
    }
}
