using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PluckingFruits
{
    class Monky
    {
        Vector2 position;
        Vector2 motion;
        float monkySpeed = 8f;

        KeyboardState keyboardState;
        GamePadState gamePadState;

        Texture2D texture;
        Rectangle screenBounds;
        Rectangle location;

        public Monky(Texture2D texture, Rectangle screenBounds)
        {
            this.texture = texture;
            this.screenBounds = screenBounds;
            SetInStartPosition();
        }

        public void Update()
        {
            motion = Vector2.Zero;

            keyboardState = Keyboard.GetState();
            gamePadState = GamePad.GetState(PlayerIndex.One);

            if (keyboardState.IsKeyDown(Keys.Left) ||
                gamePadState.IsButtonDown(Buttons.LeftThumbstickLeft) ||
                gamePadState.IsButtonDown(Buttons.DPadLeft))
                motion.X = -1;

            if (keyboardState.IsKeyDown(Keys.Right) ||
                gamePadState.IsButtonDown(Buttons.LeftThumbstickRight) ||
                gamePadState.IsButtonDown(Buttons.DPadRight))
                motion.X = 1;

            motion.X *= monkySpeed;
            position += motion;
            LockMonky();
        }

        private void LockMonky()
        {
            if (position.X < -200)
                position.X = -200;
            if (position.X + texture.Width > screenBounds.Width+200)
                position.X = screenBounds.Width - texture.Width+200;
        }

        public void SetInStartPosition()
        {
            position.X = (screenBounds.Width - texture.Width) / 2;
            position.Y = screenBounds.Height - texture.Height- (-150);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public Rectangle GetBounds()
        {
            return new Rectangle(
            (int)position.X,
            (int)position.Y,
            texture.Width,
            texture.Height);
        }

        public Rectangle Location
        {
            get { return Location; }
        }
    }
}
