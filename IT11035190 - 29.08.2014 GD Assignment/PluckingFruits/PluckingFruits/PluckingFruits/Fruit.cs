using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;

namespace PluckingFruits
{
    class Fruit
    {
        Vector2 motion;
        Vector2 position;
        //Rectangle bounds;

        float orangeSpeed = 4;

        Texture2D texture;
        Rectangle screenBounds;

        Rectangle location;
        bool alive;

        public Rectangle Location
        {
            get { return location; }
        }

        //bool collided;

        //float orangeStareSpeed = 4f;

        //public Rectangle Bounds
        //{
        //    get
        //    {
        //        bounds.X = (int)position.X;
        //        bounds.Y = (int)position.Y;
        //        return bounds;
        //    }
        //}

        public Fruit(Texture2D texture, Rectangle screenBounds)
        {
            //bounds = new Rectangle(0, 0, texture.Width, texture.Height);
            this.texture = texture;
            this.screenBounds = screenBounds;
            this.alive = true;
            //this.location = location;
        }

        public void CheckCollition(Monky monky)
        {
            Rectangle orangeLocation = new Rectangle(
                (int)position.X,
                (int)position.Y,
                texture.Width,
                texture.Height);
            if (alive && monky.GetBounds().Intersects(orangeLocation))
            {
                alive = false;
            }
        }

        public void Update()
        {
            //collided = false;
            position += motion * orangeSpeed;
            //orangeSpeed += 0.001f;
            CheckWallCollision();
        }

        private void CheckWallCollision()
        {
            if (position.X < 0)
            {
                position.X = 0;
                motion.X *= -1;
            }

            if (position.X + texture.Width > screenBounds.Width)
            {
                position.X = screenBounds.Width - texture.Width;
                motion.X *= -1;
            }

            if (position.Y < 0)
            {
                position.Y = 0;
                motion.Y *= -1;
            }
        }

        public void SetInStartPosition()
        {
            motion = new Vector2(1, -1);
            //int i;
            //screenBounds.X = 0;
            //screenBounds.X = i;
            //Random rand = new Random();
            //motion = new Vector2(rand.Next(2, 2), -rand.Next(2, 2));
            //motion.Normalize();
            //orangeSpeed = orangeStareSpeed;
            //int i =0;


            for (screenBounds.X = 0; screenBounds.X < screenBounds.Width; screenBounds.X++)
            {
                position.X = screenBounds.X;
                position.Y = 0;
            }
        }

        public bool OffBottom()
        {
            if (position.Y > screenBounds.Height)
            {
                return true;

            }
            return false;
        }

        //Rectangle monkeyLocation

        public void MonkyCollision(Rectangle monkeyLocation)
        {
            Rectangle orangeLocation = new Rectangle(
                (int)position.X,
                (int)position.Y,
                texture.Width,
                texture.Height);

            if (alive && monkeyLocation.Intersects(orangeLocation))
            {
                //alive = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
           if(alive)
            spriteBatch.Draw(texture, position, Color.White);
                //spriteBatch.Draw(texture, location, Color.White);
        }

    }

   

}
