using System;
using System.Collections;
using System.Linq;
using Blackguard.Utilities;

namespace Blackguard.UI.Popups;

public class DebugPopup : Popup {
    private const int WIDTH = 40;

    private static string FormatIEnumerable(string start, bool cond, IEnumerable objects) {
        if (!cond)
            return start;

        int wNoBorder = WIDTH - 2;
        string line = start + string.Join(' ', objects.Cast<object>().Select(o => o.ToString()));

        if (line.Length > wNoBorder)
            line = line[..wNoBorder];

        if (line.Length < wNoBorder)
            line += new string(' ', wNoBorder - line.Length);

        return line;
    }

    private static readonly (Highlight h, Func<Game, string> f)[] segments = [
        (Highlight.Text, (state) => $"Ticks: {state.ticks}"),
        (Highlight.Text, (state) => $"Seconds (from ticks): {state.ticks / 60f}"),
        (Highlight.Text, (state) => $"Elapsed Time: {DateTime.Now - state.Start}"),
        (Highlight.Text, (state) => $"Seconds (from time): {(DateTime.Now - state.Start).TotalSeconds}"),
        (Highlight.Text, (state) => FormatIEnumerable("Keycodes: ", state.Input.HasInputThisTick(), state.Input.Keycodes())),
        (Highlight.Text, (state) => FormatIEnumerable("Keynames: ", state.Input.HasInputThisTick(), state.Input.Keynames())),
        (Highlight.Text, (state) => $"Player position: {(state.Player != null ? state.Player.Position : 0)}"),
        (Highlight.Text, (state) => $"Player velocity: {(state.Player != null ? state.Player.Velocity: 0)}"),
        (Highlight.Text, (state) => $"Player chunk position: {(state.Player != null ? state.Player.ChunkPosition : 0)}"),
        (Highlight.Text, (state) => $"Player nearby Entities: {(state.Player != null ? state.Player.nearbyEntities : 0)}"),
        (Highlight.Text, (state) => $"j held: {state.Input.KeyHeld('j')}"),
        (Highlight.Text, (state) => $"j held: {state.Input.timers['j', 0]} {state.Input.timers['j', 1]}"),
    ];

    public DebugPopup() : base(Highlight.Text, 2, 2, WIDTH, segments.Length + 2) { }

    public override void HandleTermResize() { } // Do nothing. It's in the top left of the screen, hopefully nothing bad will happen.

    public override bool RunTick(Game state) => true;

    public override void Render(Game state) {
        Panel.DrawBorder(Highlight.Text);

        (Highlight h, int x, int y, string)[] processedSegments = segments.Select((s, i) => (s.h, 1, i + 1, s.f(state).Pad(38))).ToArray();

        Panel.AddLinesWithHighlight(processedSegments);
    }
}
