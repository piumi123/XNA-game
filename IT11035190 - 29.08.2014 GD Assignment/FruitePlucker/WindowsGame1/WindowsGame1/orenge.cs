using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;

namespace WindowsGame1
{
    class orenge
    {
        Vector2 motion;
        Vector2 position;
        Rectangle bounds;

        float orangeSpeed = 4;

        Texture2D texture;
        Rectangle screenBounds;

        public const float orangeStartSpeed = 4f;

        public Rectangle Bounds
        {
            get
            {
                bounds.X = (int)position.X;
                bounds.Y = (int)position.Y;
                return bounds;
            }
        }

        public orenge(Texture2D texture, Rectangle screenBounds)
        {
            bounds = new Rectangle(0, 0, texture.Width, texture.Height);
            this.texture = texture;
            this.screenBounds = screenBounds;
        }

        public void Update()
        {
            position += motion * orangeSpeed;
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
            orangeSpeed = orangeStartSpeed;
            position.Y = 0;
            position.X = 0;
            //position.Y = paddleLocation.Y - texture.Height;
            //position.X = paddleLocation.X + (paddleLocation.Width - texture.Width) / 2;
        }

        public bool OffBottom()
        {
            if (position.Y > screenBounds.Height)
                return true;
            return false;
        }

        public void PaddleCollision(Rectangle paddleLocation)
        {
            Rectangle orangeLocation = new Rectangle(
            (int)position.X,
            (int)position.Y,
            texture.Width,
            texture.Height);
            if (paddleLocation.Intersects(orangeLocation))
            {
                position.Y = paddleLocation.Y - texture.Height;
                motion.Y *= -1;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
