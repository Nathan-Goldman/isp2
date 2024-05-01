using System;

namespace Blackguard.UI;

[Flags]
public enum Alignment {
    Left       = 1 << 0, // 1
    Right      = 1 << 1, // 2
    Center     = 1 << 2, // 4
    Fill       = 1 << 3, // 8
    Top        = 1 << 4, // 32
    Bottom     = 1 << 5, // 64
    Vertical   = 1 << 6, // 128
    Horizontal = 1 << 7, // 256
}

public static class AlignmentMethods {
    private const byte HAlignmentsMask = (byte)(Alignment.Left | Alignment.Right | Alignment.Center);
    private const byte VAlignmentsMask = (byte)(Alignment.Top | Alignment.Bottom);
    private const byte OrientationsMask = (byte)(Alignment.Vertical | Alignment.Horizontal);

    // Function adds values to an alignment, forcing only one of HAlignments and one of VAlignments to be selected
    public static void UpdateAlignment(this ref Alignment alignment, Alignment changed) {
        byte byteChanged = (byte)changed;

        // If this is nonzero, it means one of the alignment fields is being changed
        if ((byteChanged & HAlignmentsMask) != 0)
            alignment &= ~(Alignment)HAlignmentsMask;

        if ((byteChanged & VAlignmentsMask) != 0)
            alignment &= ~(Alignment)VAlignmentsMask;

        if ((byteChanged & OrientationsMask) != 0)
            alignment &= ~(Alignment)OrientationsMask;

        alignment |= (Alignment)byteChanged;
    }
}
