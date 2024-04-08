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
    DarkRedOrange,
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
    TextSel,
    Warning,
    WarningSel,
    Error,
    Bold,
    BoldSel,
    Dim,
    Hidden,
    White,
    LightGray,
    DarkGray,
    Black,
    EmphasisRed,
    EmphasisGreen,
    EmphasisBlue,
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
    DarkRedOrange,
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

public enum Highlight {
    Text,
    TextSel,
    TextSelUnderline,
    TextWarning,
    TextWarningSel,
    TextWarningSelUnderline,
    TextError,
    TextBold,
    TextBoldSel,
    TextBoldSelUnderline,
    TextDim,
    TextHidden,
    TextWhite,
    TextLightGray,
    TextDarkGray,
    TextBlack,
    TextEmphasisRed,
    TextEmphasisGreen,
    TextEmphasisBlue,
    TextRedOrange,
    TextOrange,
    TextYellow,
    TextYuck,
    TextGreen,
    TextTeal,
    TextBlue,
    TextDeepBlue,
    TextPurple,
    TextMagenta,
    TextPink,
    TextLightRed,
    TextLightRedOrange,
    TextLightOrange,
    TextLightYellow,
    TextLightYuck,
    TextLightGreen,
    TextLightTeal,
    TextLightBlue,
    TextLightDeepBlue,
    TextLightPurple,
    TextLightMagenta,
    TextLightPink,
    TextDarkRed,
    TextDarkRedOrange,
    TextDarkOrange,
    TextDarkYellow,
    TextDarkYuck,
    TextDarkGreen,
    TextDarkTeal,
    TextDarkBlue,
    TextDarkDeepBlue,
    TextDarkPurple,
    TextDarkMagenta,
    TextDarkPink,
}

public static class ColorHandler {
    // Color definitions, aligned with the Colors enum, so the 0th element is the RGB set for Text
    public static readonly short[][] ColorDefs = [
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
        [ 153, 57,  31  ],  // DarkRedOrange
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
        [ Color.TextFg,         Color.TextBg     ], // Text
        [ Color.TextBg,         Color.TextFg     ], // TextSel
        [ Color.Red,            Color.TextBg     ], // Warning
        [ Color.TextBg,         Color.Red        ], // WarningSel
        [ Color.Yellow,         Color.TextBg     ], // Error
        [ Color.TextBright,     Color.TextBg     ], // Bold
        [ Color.TextBg,         Color.TextBright ], // BoldSel
        [ Color.TextQuieter,    Color.TextBg     ], // Dim
        [ Color.TextQuietest,   Color.TextBg     ], // Hidden
        [ Color.White,          Color.TextBg     ], // White
        [ Color.Gray70,         Color.TextBg     ], // LightGray
        [ Color.Gray30,         Color.TextBg     ], // DarkGray
        [ Color.Black,          Color.TextBg     ], // Black
        [ Color.Red,            Color.TextBg     ], // EmphasisRed
        [ Color.Green,          Color.TextBg     ], // EmphasisGreen
        [ Color.Blue,           Color.TextBg     ], // EmphasisBlue

        [ Color.RedOrange,      Color.TextBg     ], // RedOrange
        [ Color.Orange,         Color.TextBg     ], // Orange
        [ Color.Yellow,         Color.TextBg     ], // Yellow
        [ Color.Yuck,           Color.TextBg     ], // Yuck
        [ Color.Green,          Color.TextBg     ], // Green
        [ Color.Teal,           Color.TextBg     ], // Teal
        [ Color.Blue,           Color.TextBg     ], // Blue
        [ Color.DeepBlue,       Color.TextBg     ], // DeepBlue
        [ Color.Purple,         Color.TextBg     ], // Purple
        [ Color.Magenta,        Color.TextBg     ], // Magenta
        [ Color.Pink,           Color.TextBg     ], // Pink

        [ Color.LightRed,       Color.TextBg     ], // LightRed
        [ Color.LightRedOrange, Color.TextBg     ], // LightRedOrange
        [ Color.LightOrange,    Color.TextBg     ], // LightOrange
        [ Color.LightYellow,    Color.TextBg     ], // LightYellow
        [ Color.LightYuck,      Color.TextBg     ], // LightYuck
        [ Color.LightGreen,     Color.TextBg     ], // LightGreen
        [ Color.LightTeal,      Color.TextBg     ], // LightTeal
        [ Color.LightBlue,      Color.TextBg     ], // LightBlue
        [ Color.LightDeepBlue,  Color.TextBg     ], // LightDeepBlue
        [ Color.LightPurple,    Color.TextBg     ], // LightPurple
        [ Color.LightMagenta,   Color.TextBg     ], // LightMagenta
        [ Color.LightPink,      Color.TextBg     ], // LightPink

        [ Color.DarkRed,        Color.TextBg     ], // DarkRed
        [ Color.DarkRedOrange,  Color.TextBg     ], // DarkRedOrange
        [ Color.DarkOrange,     Color.TextBg     ], // DarkOrange
        [ Color.DarkYellow,     Color.TextBg     ], // DarkYellow
        [ Color.DarkYuck,       Color.TextBg     ], // DarkYuck
        [ Color.DarkGreen,      Color.TextBg     ], // DarkGreen
        [ Color.DarkTeal,       Color.TextBg     ], // DarkTeal
        [ Color.DarkBlue,       Color.TextBg     ], // DarkBlue
        [ Color.DarkDeepBlue,   Color.TextBg     ], // DarkDeepBlue
        [ Color.DarkPurple,     Color.TextBg     ], // DarkPurple
        [ Color.DarkMagenta,    Color.TextBg     ], // DarkMagenta
        [ Color.DarkPink,       Color.TextBg     ], // DarkPink
    ];

    public static readonly Dictionary<Highlight,    (ColorPair pair, uint attr)> HighlightDefs = new() {
        { Highlight.Text,                    (ColorPair.Text,           0) },
        { Highlight.TextSel,                 (ColorPair.TextSel,        0) },
        { Highlight.TextSelUnderline,        (ColorPair.TextSel,        CursesAttribute.UNDERLINE) },
        { Highlight.TextWarning,             (ColorPair.Warning,        0)},
        { Highlight.TextWarningSel,          (ColorPair.WarningSel,     0)},
        { Highlight.TextWarningSelUnderline, (ColorPair.WarningSel,     0)},
        { Highlight.TextError,               (ColorPair.Error,          0)},
        { Highlight.TextBold,                (ColorPair.Bold,           CursesAttribute.BOLD)},
        { Highlight.TextBoldSel,             (ColorPair.BoldSel,        CursesAttribute.BOLD)},
        { Highlight.TextBoldSelUnderline,    (ColorPair.BoldSel,        CursesAttribute.BOLD, CursesAttribute.UNDERLINE)},
        { Highlight.TextDim,                 (ColorPair.Dim,            0)},
        { Highlight.TextHidden,              (ColorPair.Hidden,         CursesAttribute.DIM)},
        { Highlight.TextWhite,               (ColorPair.White,          0)},
        { Highlight.TextLightGray,           (ColorPair.LightGray,      0)},
        { Highlight.TextDarkGray,            (ColorPair.DarkGray,       0)},
        { Highlight.TextBlack,               (ColorPair.Black,          0)},
        { Highlight.TextEmphasisRed,         (ColorPair.EmphasisRed,    CursesAttribute.BOLD)},
        { Highlight.TextEmphasisGreen,       (ColorPair.EmphasisGreen,  CursesAttribute.BOLD)},
        { Highlight.TextEmphasisBlue,        (ColorPair.EmphasisBlue,   CursesAttribute.BOLD)}

        { Highlight.TextRedOrange,           (ColorPair.RedOrange,      0)},
        { Highlight.TextOrange,              (ColorPair.Orange,         0)},
        { Highlight.TextYellow,              (ColorPair.Yellow,         0)},
        { Highlight.TextYuck,                (ColorPair.Yuck,           0)},
        { Highlight.TextGreen,               (ColorPair.Green,          0)},
        { Highlight.TextTeal,                (ColorPair.Teal,           0)},
        { Highlight.TextBlue,                (ColorPair.Blue,           0)},
        { Highlight.TextDeepBlue,            (ColorPair.DeepBlue,       0)},
        { Highlight.TextPurple,              (ColorPair.Purple,         0)},
        { Highlight.TextMagenta,             (ColorPair.Magenta,        0)},
        { Highlight.TextPink,                (ColorPair.Pink,           0)},

        { Highlight.TextLightRed,            (ColorPair.LightRed,       0)},
        { Highlight.TextLightRedOrange,      (ColorPair.LightRedOrange, 0)},
        { Highlight.TextLightOrange,         (ColorPair.LightOrange,    0)},
        { Highlight.TextLightYellow,         (ColorPair.LightYellow,    0)},
        { Highlight.TextLightYuck,           (ColorPair.LightYuck,      0)},
        { Highlight.TextLightGreen,          (ColorPair.LightGreen,     0)},
        { Highlight.TextLightTeal,           (ColorPair.LightTeal,      0)},
        { Highlight.TextLightBlue,           (ColorPair.LightBlue,      0)},
        { Highlight.TextLightDeepBlue,       (ColorPair.LightDeepBlue,  0)},
        { Highlight.TextLightPurple,         (ColorPair.LightPurple,    0)},
        { Highlight.TextLightMagenta,        (ColorPair.LightMagenta,   0)},
        { Highlight.TextLightPink,           (ColorPair.LightPink,      0)},

        { Highlight.TextDarkRed,             (ColorPair.DarkRed,        0)},
        { Highlight.TextDarkRedOrange,       (ColorPair.DarkRedOrange,  0)},
        { Highlight.TextDarkOrange,          (ColorPair.DarkOrange,     0)},
        { Highlight.TextDarkYellow,          (ColorPair.DarkYellow,     0)},
        { Highlight.TextDarkYuck,            (ColorPair.DarkYuck,       0)},
        { Highlight.TextDarkGreen,           (ColorPair.DarkGreen,      0)},
        { Highlight.TextDarkTeal,            (ColorPair.DarkTeal,       0)},
        { Highlight.TextDarkBlue,            (ColorPair.DarkBlue,       0)},
        { Highlight.TextDarkDeepBlue,        (ColorPair.DarkDeepBlue,   0)},
        { Highlight.TextDarkPurple,          (ColorPair.DarkPurple,     0)},
        { Highlight.TextDarkMagenta,         (ColorPair.DarkMagenta,    0)},
        { Highlight.TextDarkPink,            (ColorPair.DarkPink,       0)},
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
