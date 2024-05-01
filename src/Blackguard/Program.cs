using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Blackguard.Entities;
using Blackguard.Items;
using Blackguard.Tiles;
using Blackguard.Utilities;
using Mindmagma.Curses;

namespace Blackguard;

public class LibraryNames : CursesLibraryNames {
    public override bool ReplaceLinuxDefaults => false;

    public override List<string> NamesLinux => new() { "libncursesw.so" };

    public override List<string> NamesWindows => Program.Platform.ExtractNativeDependencies();
}

public class LibraryNames2 : PanelLibraryNames {
    public override bool ReplaceLinuxDefaults => false;

    public override List<string> NamesLinux => new() { "libpanelw.so" };

    // TODO: On windows with PDCurses there is no longer a separate panel library
    public override List<string> NamesWindows => Program.Platform.ExtractNativeDependencies();
}

public static class Program {
    public static Platform Platform { get; private set; }

    // Not useful for now, but may be helpful for later
    public static nint StdScreen { get; private set; }

    static Program() {
        Platform = Platform.GetPlatform();
        Platform.Configure();
    }

    [DllImport("cursesinit")]
    private static extern nint initialize_ncurses();

    public static void Main(string[] args) {
        // Any arg parsing we eventually implement should be here, before any initialization

        // Initialization moved into c library because it stops input-related things from breaking... somehow
        StdScreen = initialize_ncurses();

        if (StdScreen == nint.Zero)
            Environment.Exit(1);

        ColorHandler.Init(); // Initialize all of our color pairs and highlights
        Registry.InitializeDefinitionType<EntityDefinition>();
        Registry.InitializeDefinitionType<ItemDefinition>();
        Registry.InitializeDefinitionType<TileDefinition>(); // Initialize Tile definitions

        Console.CancelKeyPress += SIGINT; // Register this so that NCurses can uninitialize if ctrl-c is pressed

        Exception? exception = null;
        // Control is passed off to the game
        try {
            new Game().Run();
        }
        catch (Exception e) {
            exception = e;
        }
        finally {
            NCurses.EndWin();
            Console.WriteLine("\x1b[?1003l"); // Disable console mouse movement reporting

            if (exception != null)
                Console.WriteLine(exception);
        }
    }

    private static void SIGINT(object? sender, ConsoleCancelEventArgs e) {
        NCurses.EndWin();
        Console.WriteLine("\x1b[?1003l"); // Disable console mouse movement reporting
    }
}
