using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace PluckingFruits
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Monky monky;
        Fruit fruit;

        Rectangle screenRectangle;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 750;
            graphics.PreferredBackBufferHeight = 600;

            screenRectangle = new Rectangle(
                0,
                0,
                graphics.PreferredBackBufferWidth,
                graphics.PreferredBackBufferHeight);
        }


        
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D monkyImage = Content.Load<Texture2D>("monky");
            monky = new Monky(monkyImage, screenRectangle);

            Texture2D orangeImage = Content.Load<Texture2D>("NewOrange");
            fruit = new Fruit(orangeImage, screenRectangle);
            // TODO: use this.Content to load your game content here

            StartGame();
        }

        private void StartGame()
        {
            monky.SetInStartPosition();
            fruit.SetInStartPosition();
            //fruit.SetInStartPosition(monky.GetBounds());
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //monky.SetInStartPosition();
            fruit.Update();
            monky.Update();

            fruit.MonkyCollision(monky.GetBounds());


            if (fruit.OffBottom())
                StartGame();
            //monky.GetBounds();
            // TODO: Add your update logic here
            //fruit.SetInStartPosition();
            //fruit.Update();



            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            fruit.Draw(spriteBatch);
            monky.Draw(spriteBatch);

            

            spriteBatch.End();

            base.Draw(gameTime);
            
        }
    }
}
