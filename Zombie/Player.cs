using SFML;
using System;
using SFML.Window;
using SFML.Graphics;
using System.Collections.Generic;

namespace Zombie 
{
	class Character
	{
		public int width;
		public int height;

		public Image display_image;
		public Sprite display_sprite;
		public Texture display_texture;

		public Character( int w, int h, string path ) {
			width 			= w;
			height 			= h;
			display_image 	= new Image(path);
			setup();
		}

		public Character( int w, int h, Color rgb ) {
			width 			= w;
            height 			= h;
            display_image = new Image((uint) w, (uint) h, rgb);
            setup();
		}

		public void setup() {
			display_texture = new Texture(display_image);
			display_sprite 	= new Sprite(display_texture);
		}

		public Sprite sprite() {
			return display_sprite;
		}

		public void set_pos(float x, float y) {
			display_sprite.Position = new Vector2f(x, y);
		}

		public void move(float speed_x, float speed_y) {
			set_pos(display_sprite.Position.X + speed_x, display_sprite.Position.Y + speed_y);
		}

		public void update_pos(ref WorldView view, ref Dictionary<string, bool> keys) {

			float x, y;
			float speed = 2;

			x = display_sprite.Position.X;
			y = display_sprite.Position.Y;

			if(keys["up"]) {
				y -= speed;

				if(y < view.w_height - (view.height/2) && view.y > 0) {
					view.move_view(0, -speed);
				}
			}

			if(keys["down"]) {
				y += speed;

				if(y + height > (view.height / 2) && view.y < (view.w_height - view.height)) {
					view.move_view(0, speed);
				}
			}

			if(keys["left"]) {
				x -= speed;

				//Console.WriteLine("view.w_width - view.width: {0} | view.x: {1}, view.width: {2}, view.w_width: {3}", view.w_width - view.width, view.x, view.width, view.w_width);
				//Console.WriteLine("x < view.w_width - (view.width/2): {0}, view.x > view.width/2: {1}, x: {2}, view.width: {3}, view.w_width: {4}", (x < view.w_width - (view.width/2)), (view.x > view.width/2), x, view.width, view.w_width);
				if(x < view.w_width - (view.width/2) && view.x > 0) {
					view.move_view(-speed, 0);
				}
			}

			if(keys["right"]) {
			
				x += speed;

				//check if the sprite is past the 50% window mark
				//and start moving the view if possible
				//Console.WriteLine("x + width: {0} | view.width/2: {1} | view.x: {2} | view.w_w - view.w: {3}", x + width, view.width/2, view.x, view.w_width-view.width); 
				//Console.WriteLine("view.w_width: {0} | view.width: {1}", view.w_width, view.width);
				if(x + width > (view.width / 2) && view.x < (view.w_width - view.width)) {
					view.move_view(speed, 0);
				}

			} 

			//check if we're off the screen
			y = y < 0 ? 0 : y;
			y = y + height > view.w_height ? view.w_height - height : y;

			x = x < 0 ? 0 : x;
			x = x + width > view.w_width ? view.w_width - width : x;

			set_pos(x, y);
		}

		public void keyEvents(object sender, KeyEventArgs e) {

			float x, y;
			float speed = 5;

			x = display_sprite.Position.X;
			y = display_sprite.Position.Y;

			if(e.Code.ToString() == "W") {
				y -= speed;
			}

			if(e.Code.ToString() == "S") {
				y += speed;
			}

			if(e.Code.ToString() == "A") {
				x -= speed;
			}

			if(e.Code.ToString() == "D") {
				x += speed;
			}

			set_pos(x, y);
		}
	}
}