using System;
using System.Linq;
using SFML;
using SFML.Window;
using SFML.Graphics;

namespace Zombie
{
    class WorldView
    {

        //view position
        public float x;
        public float y;

        //screen width and height
        public float width;
        public float height;

        //world width & height
        public float w_width;
        public float w_height;

        //view object
        public View view;

        public WorldView(float x_pos, float y_pos, float win_width, float win_height, 
            float world_width, float world_height) {
            
            //setup class variables for later use
            x   = x_pos;
            y   = y_pos;

            //screen width/height variables
            width   = win_width;
            height  = win_height;

            //world width/height variables
            w_width     = world_width;
            w_height    = world_height;

            //setup view
            view = new View(new FloatRect(x_pos, y_pos, win_width, win_height));
        }

        public void move_view(float x_speed, float y_speed) {
            x += x_speed;
            y += y_speed;
            //Console.WriteLine("x: {0}, y: {1}", x, y);
            view.Move(new Vector2f(x_speed, y_speed));
            //view = new View(new FloatRect(x, y, width, height));
            //Console.WriteLine("this should move..");
        }

        //return view
        public View get_view() {
            return view;
        }
    }
}
