using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;

namespace WindowsGame1
{
    class Brick
    {
        Texture2D texture;
        Rectangle location;
        Color tint;
        bool alive;

        bool countBricks;
        public int marks = 0;
        bool checkFrozen1;

        Game1 game1;

        public Rectangle Location
        {
            get { return location; }
        }

        public Brick(Texture2D texture, Rectangle location, Color tint, bool chekFrozen)
        {
            this.texture = texture;
            this.location = location;
            this.tint = tint;
            this.alive = true;
            this.checkFrozen1 = chekFrozen;
        }
        public bool CheckCollision(Ball1 ball1)
        {
            countBricks = false;

            if (alive && ball1.Bounds.Intersects(location) && !checkFrozen1)
            {
                alive = false;
                ball1.Deflection(this);
                if (ball1.OffBottom())
                {
                    countBricks = false;
                }
                else
                    countBricks = true;
            }
            else if (alive && ball1.Bounds.Intersects(location) && checkFrozen1)
                ball1.Deflection(this);
            return countBricks;
        }
        //countBricks = countBricks - 1;

        public void Draw(SpriteBatch spriteBatch)
        {
            if (alive)
                spriteBatch.Draw(texture, location, tint);
        }
    }
}
