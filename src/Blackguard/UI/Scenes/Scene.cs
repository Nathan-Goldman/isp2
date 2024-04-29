using System;
using Blackguard.UI.Elements;
using Mindmagma.Curses;

namespace Blackguard.UI.Scenes;

public abstract class Scene {
    public bool Focused = true;

    // Should be defined in the constructor inheriting Scene. Default container for the Scene where elements should be stored
    protected UIContainer container = null!;

    // Perform some arbitrary data upon some arbitrary event. Documented per Scene
    public Action<object?>? callback;

    public virtual void HandleTermResize() { }

    public virtual void Initialize(Game state) { }

    // Returns false to exit the game
    public abstract bool RunTick(Game state);

    public abstract void Render(Game state);

    // When the scene is no longer idsplayed
    public virtual void Close(Game state) { }

    // To dispose of the scene
    public virtual void Finish() { }

    // Default impl handles navigating various UI Elements. For the game's main view it should not need to use this.
    public virtual void ProcessInput(Game state) {
        if (!Focused)
            return;

        // TODO: Terminal does not report keys every tick properly, so we may have to reduce the tick rate or figure out some other way to detect keys being held
        if (state.Input.KeyHit(CursesKey.DOWN))
            container.Next(true);

        if (state.Input.KeyHit(CursesKey.UP))
            container.Prev(true);

        container.ProcessInput(state);
    }
}
