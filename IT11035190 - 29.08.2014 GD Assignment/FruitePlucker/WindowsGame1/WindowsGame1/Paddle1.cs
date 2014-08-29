﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame1
{
    class Paddle1
    {
        Vector2 position;
        Vector2 motion;
        float paddleSpeed = 8f;
        KeyboardState keyboardState;
        GamePadState gamePadState;
        Texture2D texture;
        Rectangle screenBounds;
        public Paddle1(Texture2D texture, Rectangle screenBounds)
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
                motion.X *= paddleSpeed;
                position += motion;
                LockPaddle();
        }

        private void LockPaddle()
        {
            if (position.X < -200)
                position.X = -200;
            if (position.X + texture.Width > screenBounds.Width)
                position.X = screenBounds.Width - texture.Width;
        }

        public void SetInStartPosition()
        {
            position.X = (screenBounds.Width - texture.Width) / 2;
            position.Y = screenBounds.Height - texture.Height - 5;
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
                                                
    }
}
