using System;
using SFML.Graphics;
using SFML.Window;

class Program
{
    static void Main()
    {
        RenderWindow window = new(
            VideoMode.DesktopMode,
            "My First SFML Game",
            Styles.Resize,
            State.Fullscreen
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