using SFML.Graphics;
using SFML.System;

/// <summary>
/// Base class for everything that lives in the aquarium (fish, food, decorations, etc).
/// Every Component registers itself when created, so it is updated and drawn automatically.
/// </summary>

public abstract class Component
{
    private static readonly List<Component> allComponents = new();

    public Vector2f Position { get; protected set; }
    public bool IsAlive { get; protected set; } = true;

    protected Component(Vector2f startPosition)
    {
        Position = startPosition;
        allComponents.Add(this);
    }
    // Every child class must define how it updates and how it is drawn
    public abstract void Update(float deltaTime, FloatRect tankBounds);
    public abstract void Draw(RenderWindow window);

    // Updates and draws every component that exists
    public static void UpdateAndDrawAllComponents(float deltaTime, FloatRect tankBounds, RenderWindow window)
    {
        // Normal for loop, so components can be added while looping 
        for (int i = 0; i < allComponents.Count; i++)
        {
            allComponents[i].Update(deltaTime, tankBounds);
            allComponents[i].Draw(window);
        }
    }
}