using SFML;
using System;
using SFML.Window;
using SFML.Graphics;
using System.Collections.Generic;

namespace Zombie 
{
    class Game 
    {
        //game properties
        public static int width       = 1280;
        public static int height      = 720;
        public static bool focused    = false;
        public static string title    = "Zombie Game";

        //keys pressed
        public static Dictionary<string, bool> keys = new Dictionary<string, bool>();

        static void Main(string[] args) {

            //setup keypresses
            keys["up"]    = false;
            keys["down"]  = false;
            keys["left"]  = false;
            keys["right"] = false;

            Game game = new Game();

            //setup window
            RenderWindow win = new RenderWindow(new VideoMode((uint) width, (uint) height), title);

            //setup frames
            win.SetFramerateLimit(60);

            //world bg
            Sprite world_bg = game.load_world_bg(@"Assets/World1.png");

            Character player = new Character(10, 10, new Color(255, 0, 0));
            player.set_pos(0,0);

            //setup events for the first time
            win.Closed      += new EventHandler(close_window);
            win.KeyPressed  += new EventHandler<KeyEventArgs>(KeysPressed);
            win.KeyReleased += new EventHandler<KeyEventArgs>(KeysReleased);
            //win.SetKeyRepeatEnabled(true);

            //load current camera view
            //View view = new View(new FloatRect(0, 200, width, height));
            WorldView worldview = new WorldView(0, 0, width, height, 2673, 2897);

            //setup world
            RenderTexture world = new RenderTexture((uint) width, (uint) height);

            while (win.IsOpen())
            {
                win.DispatchEvents();
                //Console.WriteLine(view);
                player.update_pos(ref worldview, ref keys);

                world.Clear();
                world.SetView(worldview.get_view());
                world.Draw(world_bg);
                world.Draw(player.sprite());
                world.Display();

                win.Clear();
                win.Draw(new Sprite(world.Texture));
                win.Display();
            }
        }

        public Sprite load_world_bg(string path) {
            Image bg    = new Image(path);
            Texture txt = new Texture(bg);
            return new Sprite(txt);
        }

        /*======================================================*/
        /*----------------- EVENT HANDLERS ---------------------*/
        /*======================================================*/

        /**
         * close_window()
         * closes the current window
         */
        static void close_window(object sender, EventArgs e)
        {
            RenderWindow win = (RenderWindow)sender;
            win.Close();
        }

        static void window_focused(object sender, EventArgs e) {
            focused = true;
        }

        private static void KeysPressed(object sender, KeyEventArgs e) {
            if(e.Code == Keyboard.Key.W)
                keys["up"]    = true;
            if(e.Code == Keyboard.Key.S)
                keys["down"]  = true;
            if(e.Code == Keyboard.Key.A)
                keys["left"]  = true;
            if(e.Code == Keyboard.Key.D)
                keys["right"] = true;
        }        

        private static void KeysReleased(object sender, KeyEventArgs e) {
            if(e.Code == Keyboard.Key.W)
                keys["up"]    = false;
            if(e.Code == Keyboard.Key.S)
                keys["down"]  = false;
            if(e.Code == Keyboard.Key.A)
                keys["left"]  = false;
            if(e.Code == Keyboard.Key.D)
                keys["right"] = false;
        }
    }
}
