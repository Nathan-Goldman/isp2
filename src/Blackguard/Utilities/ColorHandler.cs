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
    Grass,
    GrassDark,
    Wood,
    WoodDark,
    VolcanoStone,
    VolcanoStoneDark,
    Lava,
    LavaDark,
    Snow,
    SnowDark,
    Ice,
    IceDark,
    MagicWaste,
    MagicWasteDark,
    MagicWasteDetail,
    MagicWasteDetailDark,
    Sand,
    SandDark,
    Cactus,
    CactusDark,
    Stone,
    StoneDark,
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
    Grass,
    GrassShadow,
    Wood,
    WoodShadow,
    VolcanoStone,
    VolcanoStoneShadow,
    Lava,
    LavaShadow,
    Snow,
    SnowShadow,
    Ice,
    IceShadow,
    MagicWaste,
    MagicWasteShadow,
    MagicWasteDetail,
    MagicWasteDetailShadow,
    Sand,
    SandShadow,
    Cactus,
    CactusShadow,
    Stone,
    StoneShadow,
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
    Grass,
    GrassShadow,
    Wood,
    WoodShadow,
    VolcanoStone,
    VolcanoStoneShadow,
    Lava,
    LavaShadow,
    Snow,
    SnowShadow,
    Ice,
    IceShadow,
    MagicWaste,
    MagicWasteShadow,
    MagicWasteDetail,
    MagicWasteDetailShadow,
    Sand,
    SandShadow,
    Cactus,
    CactusShadow,
    Stone,
    StoneShadow,
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

        [  84, 140,  59 ],  // Grass
        [  53, 111,  63 ],  // GrassDark
        [ 124,  87,  58 ],  // Wood
        [  98,  64,  50 ],  // WoodDark
        [ 139, 109, 104 ],  // VolcanoStone
        [ 116,  94,  97 ],  // VolcanoStoneDark
        [ 243,  97,  18 ],  // Lava
        [ 216,  48,  22 ],  // LavaDark
        [ 217, 225, 228 ],  // Snow
        [ 182, 185, 199 ],  // SnowDark
        [ 177, 208, 237 ],  // Ice
        [ 169, 178, 220 ],  // IceDark
        [ 186, 154, 181 ],  // MagicWaste
        [ 154, 130, 154 ],  // MagicWasteDark
        [ 194, 123, 160 ],  // MagicWasteDetail
        [ 155, 103, 141 ],  // MagicWasteDetailDark
        [ 246, 178, 107 ],  // Sand
        [ 199, 145, 109 ],  // SandDark
        [ 110, 138, 102 ],  // Cactus
        [  87, 112,  97 ],  // CactusDark
        [ 121, 123, 132 ],  // Stone
        [  98,  96, 100 ],  // StoneDark
    ];

    public static readonly Color[][] ColorPairDefs = [
        [ Color.TextFg,               Color.TextBg     ], // Text
        [ Color.TextBg,               Color.TextFg     ], // TextSel
        [ Color.Red,                  Color.TextBg     ], // Warning
        [ Color.TextBg,               Color.Red        ], // WarningSel
        [ Color.Yellow,               Color.TextBg     ], // Error
        [ Color.TextBright,           Color.TextBg     ], // Bold
        [ Color.TextBg,               Color.TextBright ], // BoldSel
        [ Color.TextQuieter,          Color.TextBg     ], // Dim
        [ Color.TextQuietest,         Color.TextBg     ], // Hidden
        [ Color.White,                Color.TextBg     ], // White
        [ Color.Gray70,               Color.TextBg     ], // LightGray
        [ Color.Gray30,               Color.TextBg     ], // DarkGray
        [ Color.Black,                Color.TextBg     ], // Black
        [ Color.Red,                  Color.TextBg     ], // EmphasisRed
        [ Color.Green,                Color.TextBg     ], // EmphasisGreen
        [ Color.Blue,                 Color.TextBg     ], // EmphasisBlue
        [ Color.Red,                  Color.TextBg     ], // Red
        [ Color.RedOrange,            Color.TextBg     ], // RedOrange
        [ Color.Orange,               Color.TextBg     ], // Orange
        [ Color.Yellow,               Color.TextBg     ], // Yellow
        [ Color.Yuck,                 Color.TextBg     ], // Yuck
        [ Color.Green,                Color.TextBg     ], // Green
        [ Color.Teal,                 Color.TextBg     ], // Teal
        [ Color.Blue,                 Color.TextBg     ], // Blue
        [ Color.DeepBlue,             Color.TextBg     ], // DeepBlue
        [ Color.Purple,               Color.TextBg     ], // Purple
        [ Color.Magenta,              Color.TextBg     ], // Magenta
        [ Color.Pink,                 Color.TextBg     ], // Pink

        [ Color.LightRed,             Color.TextBg     ], // LightRed
        [ Color.LightRedOrange,       Color.TextBg     ], // LightRedOrange
        [ Color.LightOrange,          Color.TextBg     ], // LightOrange
        [ Color.LightYellow,          Color.TextBg     ], // LightYellow
        [ Color.LightYuck,            Color.TextBg     ], // LightYuck
        [ Color.LightGreen,           Color.TextBg     ], // LightGreen
        [ Color.LightTeal,            Color.TextBg     ], // LightTeal
        [ Color.LightBlue,            Color.TextBg     ], // LightBlue
        [ Color.LightDeepBlue,        Color.TextBg     ], // LightDeepBlue
        [ Color.LightPurple,          Color.TextBg     ], // LightPurple
        [ Color.LightMagenta,         Color.TextBg     ], // LightMagenta
        [ Color.LightPink,            Color.TextBg     ], // LightPink

        [ Color.DarkRed,              Color.TextBg     ], // DarkRed
        [ Color.DarkRedOrange,        Color.TextBg     ], // DarkRedOrange
        [ Color.DarkOrange,           Color.TextBg     ], // DarkOrange
        [ Color.DarkYellow,           Color.TextBg     ], // DarkYellow
        [ Color.DarkYuck,             Color.TextBg     ], // DarkYuck
        [ Color.DarkGreen,            Color.TextBg     ], // DarkGreen
        [ Color.DarkTeal,             Color.TextBg     ], // DarkTeal
        [ Color.DarkBlue,             Color.TextBg     ], // DarkBlue
        [ Color.DarkDeepBlue,         Color.TextBg     ], // DarkDeepBlue
        [ Color.DarkPurple,           Color.TextBg     ], // DarkPurple
        [ Color.DarkMagenta,          Color.TextBg     ], // DarkMagenta
        [ Color.DarkPink,             Color.TextBg     ], // DarkPink

        [ Color.Grass,                Color.TextBg     ], // Grass
        [ Color.GrassDark,            Color.TextBg     ], // GrassShadow
        [ Color.Wood,                 Color.TextBg     ], // Wood
        [ Color.WoodDark,             Color.TextBg     ], // WoodShadow
        [ Color.VolcanoStone,         Color.TextBg     ], // VolcanoStone
        [ Color.VolcanoStoneDark,     Color.TextBg     ], // VolcanoStoneShadow
        [ Color.Lava,                 Color.TextBg     ], // Lava
        [ Color.LavaDark,             Color.TextBg     ], // LavaShadow
        [ Color.Snow,                 Color.TextBg     ], // Snow
        [ Color.SnowDark,             Color.TextBg     ], // SnowShadow
        [ Color.Ice,                  Color.TextBg     ], // Ice
        [ Color.IceDark,              Color.TextBg     ], // IceShadow
        [ Color.MagicWaste,           Color.TextBg     ], // MagicWaste
        [ Color.MagicWasteDark,       Color.TextBg     ], // MagicWasteShadow
        [ Color.MagicWasteDetail,     Color.TextBg     ], // MagicWasteDetail
        [ Color.MagicWasteDetailDark, Color.TextBg     ], // MagicWasteDetailShadow
        [ Color.Sand,                 Color.TextBg     ], // Sand
        [ Color.SandDark,             Color.TextBg     ], // SandShadow
        [ Color.Cactus,               Color.TextBg     ], // Cactus
        [ Color.CactusDark,           Color.TextBg     ], // CactusShadow
        [ Color.Stone,                Color.TextBg     ], // Stone
        [ Color.StoneDark,            Color.TextBg     ], // StoneShadow
    ];

    public static readonly Dictionary<Highlight, (ColorPair pair, uint attr)> HighlightDefs = new() {
        { Highlight.Text,                       (ColorPair.Text,                   0)},
        { Highlight.TextSel,                    (ColorPair.TextSel,                0)},
        { Highlight.TextSelUnderline,           (ColorPair.TextSel,                CursesAttribute.UNDERLINE) },
        { Highlight.TextWarning,                (ColorPair.Warning,                0)},
        { Highlight.TextWarningSel,             (ColorPair.WarningSel,             0)},
        { Highlight.TextWarningSelUnderline,    (ColorPair.WarningSel,             0)},
        { Highlight.TextError,                  (ColorPair.Error,                  0)},
        { Highlight.TextBold,                   (ColorPair.Bold,                   CursesAttribute.BOLD)},
        { Highlight.TextBoldSel,                (ColorPair.BoldSel,                CursesAttribute.BOLD)},
        { Highlight.TextBoldSelUnderline,       (ColorPair.BoldSel,                CursesAttribute.BOLD | CursesAttribute.UNDERLINE)},
        { Highlight.TextDim,                    (ColorPair.Dim,                    0)},
        { Highlight.TextHidden,                 (ColorPair.Hidden,                 CursesAttribute.DIM)},
        { Highlight.TextWhite,                  (ColorPair.White,                  0)},
        { Highlight.TextLightGray,              (ColorPair.LightGray,              0)},
        { Highlight.TextDarkGray,               (ColorPair.DarkGray,               0)},
        { Highlight.TextBlack,                  (ColorPair.Black,                  0)},
        { Highlight.TextEmphasisRed,            (ColorPair.EmphasisRed,            CursesAttribute.BOLD)},
        { Highlight.TextEmphasisGreen,          (ColorPair.EmphasisGreen,          CursesAttribute.BOLD)},
        { Highlight.TextEmphasisBlue,           (ColorPair.EmphasisBlue,           CursesAttribute.BOLD)},
        { Highlight.Red,                    (ColorPair.Red,                    0)},
        { Highlight.RedOrange,              (ColorPair.RedOrange,              0)},
        { Highlight.Orange,                 (ColorPair.Orange,                 0)},
        { Highlight.Yellow,                 (ColorPair.Yellow,                 0)},
        { Highlight.Yuck,                   (ColorPair.Yuck,                   0)},
        { Highlight.Green,                  (ColorPair.Green,                  0)},
        { Highlight.Teal,                   (ColorPair.Teal,                   0)},
        { Highlight.Blue,                   (ColorPair.Blue,                   0)},
        { Highlight.DeepBlue,               (ColorPair.DeepBlue,               0)},
        { Highlight.Purple,                 (ColorPair.Purple,                 0)},
        { Highlight.Magenta,                (ColorPair.Magenta,                0)},
        { Highlight.Pink,                   (ColorPair.Pink,                   0)},

        { Highlight.LightRed,               (ColorPair.LightRed,               0)},
        { Highlight.LightRedOrange,         (ColorPair.LightRedOrange,         0)},
        { Highlight.LightOrange,            (ColorPair.LightOrange,            0)},
        { Highlight.LightYellow,            (ColorPair.LightYellow,            0)},
        { Highlight.LightYuck,              (ColorPair.LightYuck,              0)},
        { Highlight.LightGreen,             (ColorPair.LightGreen,             0)},
        { Highlight.LightTeal,              (ColorPair.LightTeal,              0)},
        { Highlight.LightBlue,              (ColorPair.LightBlue,              0)},
        { Highlight.LightDeepBlue,          (ColorPair.LightDeepBlue,          0)},
        { Highlight.LightPurple,            (ColorPair.LightPurple,            0)},
        { Highlight.LightMagenta,           (ColorPair.LightMagenta,           0)},
        { Highlight.LightPink,              (ColorPair.LightPink,              0)},

        { Highlight.DarkRed,                (ColorPair.DarkRed,                0)},
        { Highlight.DarkRedOrange,          (ColorPair.DarkRedOrange,          0)},
        { Highlight.DarkOrange,             (ColorPair.DarkOrange,             0)},
        { Highlight.DarkYellow,             (ColorPair.DarkYellow,             0)},
        { Highlight.DarkYuck,               (ColorPair.DarkYuck,               0)},
        { Highlight.DarkGreen,              (ColorPair.DarkGreen,              0)},
        { Highlight.DarkTeal,               (ColorPair.DarkTeal,               0)},
        { Highlight.DarkBlue,               (ColorPair.DarkBlue,               0)},
        { Highlight.DarkDeepBlue,           (ColorPair.DarkDeepBlue,           0)},
        { Highlight.DarkPurple,             (ColorPair.DarkPurple,             0)},
        { Highlight.DarkMagenta,            (ColorPair.DarkMagenta,            0)},
        { Highlight.DarkPink,               (ColorPair.DarkPink,               0)},

        { Highlight.Grass,                  (ColorPair.Grass,                  0)},
        { Highlight.GrassShadow,            (ColorPair.GrassShadow,            0)},
        { Highlight.Wood,                   (ColorPair.Wood,                   0)},
        { Highlight.WoodShadow,             (ColorPair.WoodShadow,             0)},
        { Highlight.VolcanoStone,           (ColorPair.VolcanoStone,           0)},
        { Highlight.VolcanoStoneShadow,     (ColorPair.VolcanoStoneShadow,     0)},
        { Highlight.Snow,                   (ColorPair.Snow,                   0)},
        { Highlight.SnowShadow,             (ColorPair.SnowShadow,             0)},
        { Highlight.Ice,                    (ColorPair.Ice,                    0)},
        { Highlight.IceShadow,              (ColorPair.IceShadow,              0)},
        { Highlight.MagicWaste,             (ColorPair.MagicWaste,             0)},
        { Highlight.MagicWasteShadow,       (ColorPair.MagicWasteShadow,       0)},
        { Highlight.MagicWasteDetail,       (ColorPair.MagicWasteDetail,       0)},
        { Highlight.MagicWasteDetailShadow, (ColorPair.MagicWasteDetailShadow, 0)},
        { Highlight.Sand,                   (ColorPair.Sand,                   0)},
        { Highlight.SandShadow,             (ColorPair.SandShadow,             0)},
        { Highlight.Cactus,                 (ColorPair.Cactus,                 0)},
        { Highlight.CactusShadow,           (ColorPair.CactusShadow,           0)},
        { Highlight.Stone,                  (ColorPair.Stone,                  0)},
        { Highlight.StoneShadow,            (ColorPair.StoneShadow,            0)},
    };

    // Gets the pair number
    public static short GetPair(this Highlight highlight) => (short)HighlightDefs[highlight].pair;

    // Gets the pair attr
    public static uint GetPairAttr(this Highlight highlight) => NCurses.ColorPair(highlight.GetPair());

    // Gets other attrs (underline, bold, etc)
    public static uint GetAttr(this Highlight highlight) => HighlightDefs[highlight].attr;

    // Combines the color pair attr and the other attrs (underline, bold, etc) into one single uint used by some functions
    public static uint AsMixedAttr(this Highlight highlight) => highlight.GetPairAttr() | highlight.GetAttr();

    public static void Init() {
        for (short i = 0; i < ColorDefs.Length; i++) {
            NCurses.InitColor((short)(i + 8), ColorDefs[i][0], ColorDefs[i][1], ColorDefs[i][2]);
        }

        for (short i = 0; i < ColorPairDefs.Length; i++) {
            NCurses.InitPair((short)(i + 1), (short)ColorPairDefs[i][0], (short)ColorPairDefs[i][1]);
        }
    }
}
