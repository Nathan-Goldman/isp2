#include <curses.h>
#include <locale.h>
#include <stdbool.h>

WINDOW *initialize_ncurses() {
    setlocale(LC_ALL, "");

    initscr();

    if (!has_colors() || !can_change_color()) {
        endwin();
        printf("%s",
               "Terminal does not support colors. Please use a terminal with "
               "support for colors.");
        return NULL;
    }

    curs_set(0); // Hide the cursor
    cbreak(); // Don't buffer lines. Make input available without waiting for a
              // newline
    noecho(); // Don't print input
    nodelay(stdscr,
            true);        // Make input immediately available instead of waiting
    keypad(stdscr, true); // Process intput instead of giving raw keycodes
    set_escdelay(
        0); // Don't wait when escape is pressed, process the key immediately
    start_color(); // Initialize colors
    mousemask(ALL_MOUSE_EVENTS | REPORT_MOUSE_POSITION,
              NULL);         // Set mouse events we are interested in
    mouseinterval(0);        // Don't wait after clicks to report double clicks
    printf("\033[?1003h\n"); // Enable mouse motion reporting

    return stdscr;
}
