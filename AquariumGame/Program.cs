using System;
using System.Collections.Generic;
using System.IO;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

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

        // Load a font file (falls back to Arial if yours isn't found)
        string fontPath = "Assets\\Fonts\\LeFih.ttf";
        if (!File.Exists(fontPath))
        {
            fontPath = "C:\\Windows\\Fonts\\arial.ttf";
        }
        Font font = new(fontPath);

        // Create the message
        Text message = new(font, "Pika!", 30)
        {
            FillColor = Color.White,
            Position = new Vector2f(280, 270)
        };

        // Limit the framerate
        window.SetFramerateLimit(60);

        // ----- FISH SETUP -----
        FloatRect tank = new FloatRect(new Vector2f(0, 0), new Vector2f(800, 600));

        Random rng = new Random();
        List<Fish> fishes = new List<Fish>();
        for (int i = 0; i < 5; i++)
        {
            fishes.Add(new Fish(new Vector2f(rng.Next(50, 750), rng.Next(50, 550))));
        }

        // Press F to feed all the fish
        window.KeyPressed += (sender, e) =>
        {
            if (e.Code == Keyboard.Key.F)
            {
                foreach (Fish fish in fishes)
                {
                    fish.Feed();
                }
            }
        };

        Clock clock = new Clock();

        // Main game loop
        while (window.IsOpen)
        {
            // Process window events
            window.DispatchEvents();

            // Time since last frame, in seconds
            float dt = clock.Restart().AsSeconds();

            // Update all the fish
            foreach (Fish fish in fishes)
            {
                fish.Update(dt, tank);
            }

            // Clear the screen with a blue water background
            window.Clear(new Color(30, 120, 190));

            // Draw the fish
            foreach (Fish fish in fishes)
            {
                fish.Draw(window);
            }

            // Draw the message
            window.Draw(message);

            // Display everything
            window.Display();
        }
    }
}