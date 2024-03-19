using System.Collections.Generic;
using Mindmagma.Curses;

namespace Blackguard.Utilities;

public enum Color : short {
    TextFg = 8,
    TextBg,
    TextBright,
    TextQuiet,
    TextQuieter,
    TextQuietest,

    White,
    Gray90,
    Gray80,
    Gray70,
    Gray60,
    Gray50,
    Gray40,
    Gray30,
    Gray20,
    Gray10,
    Black,

    Red,
    RedOrange,
    Orange,
    Yellow,
    Yuck,
    Green,
    Teal,
    Blue,
    DeepBlue,
    Purple,
    Magenta,
    Pink,

    LightRed,
    LightRedOrange,
    LightOrange,
    LightYellow,
    LightYuck,
    LightGreen,
    LightTeal,
    LightBlue,
    LightDeepBlue,
    LightPurple,
    LightMagenta,
    LightPink,

    DarkRed,
    DarkRorange,
    DarkOrange,
    DarkYellow,
    DarkYuck,
    DarkGreen,
    DarkTeal,
    DarkBlue,
    DarkDeepBlue,
    DarkPurple,
    DarkMagenta,
    DarkPink,
}

public enum ColorPair {
    Text = 1,
<<<<<<< HEAD
=======
    TextSel,
    Warning,
    Error,
>>>>>>> f9843fcc4d0f0dc8a43f9b54da879fc9fed0d1f1
}

public enum Highlight {
    Text,
    TextSel,
    TextSelUnderline,
    TextWarning,
    TextError,
}

public static class ColorHandler {
    // Color definitions, aligned with the Colors enum, so the 0th element is the RGB set for Text
    public static readonly short[][] ColorDefs = [
<<<<<<< HEAD
        //[ 210, 211, 212 ], // TextFg
        //[ 22,   26,   48   ], // TextBg

        [ 209, 215, 227 ],  // textNormal
        [  38,  40,  48 ],  // backgroundMenu

        [ 242, 247, 255 ],  // textBright
        [ 149, 154, 164 ],  // textQuiet
        [ 106, 110, 119 ],  // textQuieter
        [  63,  66,  74 ],  // textQuietest

        [ 255, 255, 255 ],  // white
        [ 230, 230, 230 ],  // gray90
        [ 204, 204, 204 ],  // gray80
        [ 179, 179, 179 ],  // gray70
        [ 153, 153, 153 ],  // gray60
        [ 128, 128, 128 ],  // gray50
        [ 102, 102, 102 ],  // gray40
        [  77,  77,  77 ],  // gray30
        [  51,  51,  51 ],  // gray20
        [  26,  26,  26 ],  // gray10
        [   0,   0,   0 ],  // black

        [ 242, 85,   85 ],  // red
        [ 242, 119,  85 ],  // rorange
        [ 242, 143,  85 ],  // orange
        [ 242, 205,  85 ],  // yellow
        [ 190, 242,  85 ],  // yuck
        [ 127, 242,  85 ],  // green
        [  85, 242, 199 ],  // teal
        [  85, 192, 242 ],  // blue
        [  85, 109, 242 ],  // deepBlue
        [ 167,  85, 242 ],  // purple
        [ 234,  85, 242 ],  // magenta
        [ 242,  85, 163 ],  // pink

        [ 255, 153, 153 ],  // lightRed
        [ 255, 175, 153 ],  // lightRorange
        [ 255, 191, 153 ],  // lightOrange
        [ 255, 231, 153 ],  // lightYellow
        [ 221, 255, 153 ],  // lightYuck
        [ 180, 255, 153 ],  // lightGreen
        [ 153, 255, 227 ],  // lightTeal
        [ 153, 222, 255 ],  // lightBlue
        [ 153, 168, 255 ],  // lightDeepBlue
        [ 206, 153, 255 ],  // lightPurple
        [ 250, 153, 255 ],  // lightMagenta
        [ 255, 153, 203 ],  // lightPink

        [ 153,  31,  31 ],  // darkRed
        [ 153,  57,  31 ],  // darkRorange
        [ 153,  76,  31 ],  // darkOrange
        [ 153, 124,  31 ],  // darkYellow
        [ 113, 153,  31 ],  // darkYuck
        [  63, 153,  31 ],  // darkGreen
        [  31, 153, 120 ],  // darkTeal
        [  31, 114, 153 ],  // darkBlue
        [  31,  49, 153 ],  // darkDeepBlue
        [  95,  31, 153 ],  // darkPurple
        [ 147,  31, 153 ],  // darkMagenta
        [ 153,  31,  91 ],  // darkPink
    ];

    public static readonly Color[][] ColorPairDefs = [
        [ Color.TextFg, Color.TextBg ] // It would be nice if it didn't need to specify the Color enum
    ];

    public static readonly Dictionary<Highlight, (ColorPair pair, uint attr)> HighlightDefs = new() {
        { Highlight.Text, (ColorPair.Text, 0) },
        { Highlight.TextSel, (ColorPair.Text, CursesAttribute.UNDERLINE) }
=======
        [ 209, 215, 227 ],  // TextNormal
        [ 38,  40,  48  ],  // BackgroundMenu

        [ 242, 247, 255 ],  // TextBright
        [ 149, 154, 164 ],  // TextQuiet
        [ 106, 110, 119 ],  // TextQuieter
        [ 63,  66,  74  ],  // TextQuietest

        [ 255, 255, 255 ],  // White
        [ 230, 230, 230 ],  // Gray90
        [ 204, 204, 204 ],  // Gray80
        [ 179, 179, 179 ],  // Gray70
        [ 153, 153, 153 ],  // Gray60
        [ 128, 128, 128 ],  // Gray50
        [ 102, 102, 102 ],  // Gray40
        [ 77,  77,  77  ],  // Gray30
        [ 51,  51,  51  ],  // Gray20
        [ 26,  26,  26  ],  // Gray10
        [ 0,   0,   0   ],  // Black

        [ 242, 85,  85  ],  // Red
        [ 242, 119, 85  ],  // RedOrange
        [ 242, 143, 85  ],  // Orange
        [ 242, 205, 85  ],  // Yellow
        [ 190, 242, 85  ],  // Yuck
        [ 127, 242, 85  ],  // Green
        [ 85,  242, 199 ],  // Teal
        [ 85,  192, 242 ],  // Blue
        [ 85,  109, 242 ],  // DeepBlue
        [ 167, 85,  242 ],  // Purple
        [ 234, 85,  242 ],  // Magenta
        [ 242, 85,  163 ],  // Pink

        [ 255, 153, 153 ],  // LightRed
        [ 255, 175, 153 ],  // LightRedOrange
        [ 255, 191, 153 ],  // LightOrange
        [ 255, 231, 153 ],  // LightYellow
        [ 221, 255, 153 ],  // LightYuck
        [ 180, 255, 153 ],  // LightGreen
        [ 153, 255, 227 ],  // LightTeal
        [ 153, 222, 255 ],  // LightBlue
        [ 153, 168, 255 ],  // LightDeepBlue
        [ 206, 153, 255 ],  // LightPurple
        [ 250, 153, 255 ],  // LightMagenta
        [ 255, 153, 203 ],  // LightPink

        [ 153, 31,  31  ],  // DarkRed
        [ 153, 57,  31  ],  // DarkRorange
        [ 153, 76,  31  ],  // DarkOrange
        [ 153, 124, 31  ],  // DarkYellow
        [ 113, 153, 31  ],  // DarkYuck
        [ 63,  153, 31  ],  // DarkGreen
        [ 31,  153, 120 ],  // DarkTeal
        [ 31,  114, 153 ],  // DarkBlue
        [ 31,  49,  153 ],  // DarkDeepBlue
        [ 95,  31,  153 ],  // DarkPurple
        [ 147, 31,  153 ],  // DarkMagenta
        [ 153, 31,  91  ],  // DarkPink
    ];

    public static readonly Color[][] ColorPairDefs = [
        [ Color.TextFg, Color.TextBg ], // Text
        [ Color.TextBg, Color.TextFg ], // TextSel
        [ Color.Yellow, Color.TextBg ], // Error
        [ Color.Red,    Color.TextBg ]  // Warning
    ];

    public static readonly Dictionary<Highlight, (ColorPair pair, uint attr)> HighlightDefs = new() {
        { Highlight.Text,             (ColorPair.Text,    0) },
        { Highlight.TextSel,          (ColorPair.TextSel, 0) },
        { Highlight.TextSelUnderline, (ColorPair.TextSel, CursesAttribute.UNDERLINE) },
        { Highlight.TextWarning,      (ColorPair.Warning, 0)},
        { Highlight.TextError,        (ColorPair.Error,   0)}
>>>>>>> f9843fcc4d0f0dc8a43f9b54da879fc9fed0d1f1
    };

    public static ColorPair GetPair(this Highlight highlight) => HighlightDefs[highlight].pair;

    public static uint GetAttr(this Highlight highlight) => HighlightDefs[highlight].attr;

    public static void Init() {
        for (short i = 0; i < ColorDefs.Length; i++) {
            NCurses.InitColor((short)(i + 8), ColorDefs[i][0], ColorDefs[i][1], ColorDefs[i][2]);
        }

        for (short i = 0; i < ColorPairDefs.Length; i++) {
            NCurses.InitPair((short)(i + 1), (short)ColorPairDefs[i][0], (short)ColorPairDefs[i][1]);
        }
    }
}
