using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System;

namespace Test
{
    class IHM

    {

        static public Vector2f size = new Vector2f(1080, 720);
        static public bool DebugDraw = false;
        RenderWindow window = new RenderWindow(new VideoMode((uint)size.X, (uint)size.Y), "Game");
        Terrain LeTerrain;
        static public int generation = 0;
        static public int sleep=30;
        public IHM(Terrain t)
        {
            LeTerrain = t;
            window.SetFramerateLimit(60);
            loopmain();
        }
        static void OnClose(object sender, EventArgs e)
        {
            // Close the window when OnClose event is received
            RenderWindow window = (RenderWindow)sender;
            window.Close();
        }

        private void loopmain()
        {
            // Create the main windo
          
            window.Closed += new EventHandler(OnClose);
            // Load a sprite to display
            
            // Start the game loop
            while (window.IsOpen)
            {
                // Process events
                window.DispatchEvents();
                if(Keyboard.IsKeyPressed(Keyboard.Key.K))
                    DebugDraw=!DebugDraw;
                if (Keyboard.IsKeyPressed(Keyboard.Key.S))
                {
                    if (sleep > 1)
                        sleep--;
                }
                else if (Keyboard.IsKeyPressed(Keyboard.Key.P))
                    sleep += 2;
                     
                // Clear screen
                window.Clear(new Color(0,0,128));
                window.SetTitle("Generation numero : "+generation.ToString());
                // Draw the sprite
                LeTerrain.draw(window);
                // Update the window
                window.Display();
            }

        }
    }
}
