using System;
using SFML.Graphics;
using SFML.Window;

class Program
{
    static void Main()
    {
        // Create the window
        RenderWindow window = new RenderWindow(
            new VideoMode((800, 600)),
            "My First SFML Game"
        );

        // Close the window when the user clicks X
        window.Closed += (sender, e) =>
        {
            window.Close();
        };

        // Load a font file
        Font font = new("Assets\\Fonts\\LeFih.ttf");

        // Create the message
        Text message = new(font, "Pika!", 30)
        {
            FillColor = Color.White,
            Position = new SFML.System.Vector2f(280, 270)
        };

        // Limit the framerate
        window.SetFramerateLimit(60);

        // Main game loop
        while (window.IsOpen)
        {
            // Process window events
            window.DispatchEvents();

            // Clear the screen with a black background
            window.Clear(Color.Black);

            // Draw the message
            window.Draw(message);

            // Display everything
            window.Display();
        }
    }
}