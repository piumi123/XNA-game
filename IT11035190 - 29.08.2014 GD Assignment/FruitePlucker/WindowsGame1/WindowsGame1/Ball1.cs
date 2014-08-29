using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;

namespace WindowsGame1
{
    class Ball1
    {
        Vector2 motion;
        Vector2 position;
        Rectangle bounds;

        int count = 0;
        float ballSpeed = 4;

        Texture2D texture;
        Rectangle screenBounds;

        bool collided;

        public const float ballStartSpeed = 4f;

        public Rectangle Bounds
        {
            get
            {
                bounds.X = (int)position.X;
                bounds.Y = (int)position.Y;
                return bounds;
            }
        }

        public Ball1(Texture2D texture, Rectangle screenBounds)
        {
            bounds = new Rectangle(0, 0, texture.Width, texture.Height);
            this.texture = texture;
            this.screenBounds = screenBounds;
        }

        public void Update()
        {
            collided = false;
            position += motion * ballSpeed;
            ballSpeed += 0.001f;
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

        public void SetInStartPosition(Rectangle paddleLocation)
        {
            Random rand = new Random();
            motion = new Vector2(rand.Next(2, 6), -rand.Next(2, 6));
            motion.Normalize();
            ballSpeed = ballStartSpeed;
            position.Y = paddleLocation.Y - texture.Height;
            position.X = paddleLocation.X + (paddleLocation.Width - texture.Width) / 2;
        }

        public bool OffBottom()
        {



            if (position.Y > screenBounds.Height)
            {
                return true;
                
            }
                //System.Windows.Forms.MessageBox.Show("Hello There!");

                //MessageBox.Show("Hello There!");
            
             
            return false;
            
        }

        public void PaddleCollision(Rectangle paddleLocation)
        {
            Rectangle ballLocation = new Rectangle(
            (int)position.X,
            (int)position.Y,
            texture.Width,
            texture.Height);
            if (paddleLocation.Intersects(ballLocation))
            {
                position.Y = paddleLocation.Y - texture.Height;
                motion.Y *= -1;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public void Deflection(Brick brick)
        {
            if (!collided)
            {
                motion.Y *= -1;
                collided = true;
            }
        }

    }
}
