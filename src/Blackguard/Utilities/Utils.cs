using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using Mindmagma.Curses;

namespace Blackguard.Utilities;

public static class Utils {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfOutOfBounds(int x, int y, int w, int h) {
        if (x < 0 || y < 0 || y + h > NCurses.Lines || x + w > NCurses.Columns)
            throw new Exception($"Attempted to draw out of bounds! The window is {NCurses.Columns}x{NCurses.Lines}, but a line was printed at {x}x{y}, ending at {x + w}");
    }

    // Return values are signed in accordance to the behavior of the skip argument for rendering partial chunks, so they are negated
    public static bool CheckOutOfBounds(int x, int y, int w, int h, int maxw, int maxh, out int byX, out int byY) {
        bool ret = false;

        if (x < 0) {
            byX = -x;
            ret = true;
        }
        else if (x + w > maxw) {
            byX = maxw - (x + w);
            ret = true;
        }
        else
            byX = 0;

        if (y < 0) {
            byY = -y;
            ret = true;
        }
        else if (y + h > maxh) {
            byY = maxh - (y + h);
            ret = true;
        }
        else
            byY = 0;

        return ret;
    }

#pragma warning disable IDE1006 // Shut up naming violation
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void mvwchgat(nint window, int x, int y, int len, uint attr, short pair) {
        NCurses.WindowMove(window, y, x);
        NCurses.WindowChangeAttribute(window, len, attr, pair, nint.Zero);
    }
#pragma warning restore IDE1006

    /// <summary>
    /// Returns the NOT BOUNDS CHECKED position relative to the top left of the screen
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Point ToScreenPos(Point topLeft, Point pos) {
        return pos - topLeft;
    }

    private static readonly string[] largeTextGlyphs = [
        " ▄█▄ █▀▀▀▄▄▀▀▀▄█▀▀▄ █▀▀▀▀█▀▀▀▀▄▀▀▀▄█   █▀▀█▀▀  ▀█▀█  ▄▀█    █   ███  █▄▀▀▀▄█▀▀▀▄▄▀▀▀▄█▀▀▄ ▄▀▀▀▄▀▀█▀▀█   ██   ██   ██   ██   █▀▀▀▀█▄▀▀▀▄ ▄█  ▄▀▀▀▄▄▀▀▀▄  ▄█ █▀▀▀▀▄▀▀▀▄▀▀▀▀█▄▀▀▀▄▄▀▀▀▄      ▄▄  ",
        " █ █ █▄▄▄▀█    █   ██▄▄▄ █▄▄▄ █    █▄▄▄█  █     █ █▄▀  █    ██ ███ █ ██   ██▄▄▄▀█   ██▄▄▀ ▀▄▄▄   █  █   █▀▄ ▄▀█ ▄ █ ▀▄▀  ▀▄▀   ▄▀ █▀▄ █  █     ▄▀  ▄▄▀▄▀ █ ▀▄▄▄ █▄▄▄    █ ▀▄▄▄▀▀▄▄▄█      ▀▀  ",
        "█▀▀▀██   ██   ▄█  ▄▀█    █    █  ▀██   █  █  ▄  █ █ ▀▄ █    █ █ ██ ▀▄██   ██    █ ▀▄▀█  █ ▄   █  █  █   █ █ █ █ █ █▄▀ ▀▄  █  ▄▀   █  ▀█  █  ▄▀▀  ▄   █▀▀▀█▀▄   ██   █  █  █   █▄   █      ▄▄  ",
        "▀   ▀▀▀▀▀  ▀▀▀ ▀▀▀  ▀▀▀▀▀▀     ▀▀▀ ▀   ▀▀▀▀▀▀ ▀▀  ▀   ▀▀▀▀▀▀▀ ▀ ▀▀  ▀▀ ▀▀▀ ▀     ▀▀ ▀▀   ▀ ▀▀▀   ▀   ▀▀▀   ▀   ▀ ▀ ▀   ▀  ▀  ▀▀▀▀▀ ▀▀▀ ▀▀▀▀▀▀▀▀▀▀ ▀▀▀    ▀  ▀▀▀  ▀▀▀  ▀    ▀▀▀  ▀▀▀       ▀▀  "
    ];

    // WARN: Terrible hardcode. Works well enough.
    public static string[] ToLargeText(this string str) {
        List<ReadOnlyMemory<char>[]> glyphs = new();

        void AddGlyph(int start) {
            glyphs.Add([
                largeTextGlyphs[0].AsMemory(start, 5),
                largeTextGlyphs[1].AsMemory(start, 5),
                largeTextGlyphs[2].AsMemory(start, 5),
                largeTextGlyphs[3].AsMemory(start, 5)
            ]);
        }

        foreach (char c in str.ToLower()) {
            if (c >= 48 && c <= 57) // Numbers 0-9
                AddGlyph((c - 22) * 5);
            else if (c >= 97 && c <= 127) // a-z
                AddGlyph((c - 97) * 5);
            else if (c == 32) // space
                AddGlyph(36 * 5);
            else if (c == 58) // colon
                AddGlyph(37 * 5);
        }

        // This should work but doesn't. Would be nice to make this method somewhat less ugly.
        /* StringBuilder[] ret = [ */
        /*     new StringBuilder().AppendJoin(' ', glyphs[0]), */
        /*     new StringBuilder().AppendJoin(' ', glyphs[1]), */
        /*     new StringBuilder().AppendJoin(' ', glyphs[2]), */
        /*     new StringBuilder().AppendJoin(' ', glyphs[3]), */
        /* ]; */

        StringBuilder[] ret = [new(), new(), new(), new()];

        string format = "{0}";
        bool set = false;
        foreach (ReadOnlyMemory<char>[] mem in glyphs) {
            ret[0].AppendFormat(format, mem[0]);
            ret[1].AppendFormat(format, mem[1]);
            ret[2].AppendFormat(format, mem[2]);
            ret[3].AppendFormat(format, mem[3]);

            // Kind of hacky. Oh well
            if (!set) {
                format = " {0}";
                set = true;
            }
        }

        return [
            ret[0].ToString(),
            ret[1].ToString(),
            ret[2].ToString(),
            ret[3].ToString()
        ];
    }

    public static string Pad(this string s, int w) {
        if (s.Length == 0)
            return s;

        if (s.Length > w)
            return s[..w];

        return s + new string(' ', w - s.Length);
    }

    // If positive, then the two rectangles intersect
    public static float Intersect(float x1f, float y1f, int w1, int h1, float x2f, float y2f, int w2, int h2) {
        int x1 = (int)x1f;
        int y1 = (int)y1f;
        int x2 = (int)x2f;
        int y2 = (int)y2f;

        return Math.Max(0f, Math.Min(x1 + w1, x2 + w2) - Math.Max(x1, x2))
            * Math.Max(0f, Math.Min(y1 + h1, y2 + h2) - Math.Max(y1, y2));
    }

    public static Point ToChunkPosition(this Point p) => new((int)Math.Floor((float)p.X / Chunk.CHUNKSIZE), (int)Math.Floor((float)p.Y / Chunk.CHUNKSIZE));

    public static Point ToChunkPosition(this Vector2 v) => new((int)Math.Floor(v.X / Chunk.CHUNKSIZE), (int)Math.Floor(v.Y / Chunk.CHUNKSIZE));

    public static Point ToWorldPosition(this Point p) => p * Chunk.CHUNKSIZE;

    public static Vector2 Normalize(this Vector2 v) {
        float len = v.Length();

        return new(v.X / len, v.Y / len);
    }

    public static Vector2 DirectionTo(this Vector2 from, Vector2 to) => to - from;
}
