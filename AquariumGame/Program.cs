using System;
using SFML.Graphics;
using SFML.Window;

class Program
{
    static void Main()
    {
        RenderWindow window = new RenderWindow(
            new VideoMode((800, 600)),
            "My First SFML Game"
        );

        window.Closed += (sender, e) =>
        {
            window.Close();
        };

        window.SetFramerateLimit(60);

        while (window.IsOpen)
        {
            window.DispatchEvents();

            window.Clear(Color.White);

            window.Display();
        }
    }
}