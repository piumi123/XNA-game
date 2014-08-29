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

namespace WindowsGame1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font1;
        //SpriteFont font2;

        Paddle1 paddle1;
        Ball1 ball1;
        orenge orange1;
        Rectangle screenRectangle;
        Rectangle paddleLocation;
        Bomb bomb1
            ;

        public int bricksWide = 10;
        public int bricksHigh = 5;
        Texture2D brickImage;
        Texture2D orange;
        Texture2D bomb;
        Brick[,] bricks;

        int count = 0;
        int countBricks;
        bool colided;
        bool offmessage;
        int marks = 0;

        int newMarks;
        int oldScore=0;
        int higherScore=0;

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

            Texture2D tempTexture = Content.Load<Texture2D>("paddle");
            paddle1 = new Paddle1(tempTexture, screenRectangle);

            tempTexture = Content.Load<Texture2D>("monky");
            ball1 = new Ball1(tempTexture, screenRectangle);

            brickImage = Content.Load<Texture2D>("NewOrange");

            orange = Content.Load<Texture2D>("orange1");
            orange1 = new orenge(orange, screenRectangle);

            //bomb = Content.Load<Texture2D>("bomb");
            bomb1 = new Bomb(bomb, screenRectangle);


            font1 = Content.Load<SpriteFont>("FontLabel");

            StartGame();
            // TODO: use this.Content to load your game content here
        }

        public void StartGame()
        {
            paddle1.SetInStartPosition();
            ball1.SetInStartPosition(paddle1.GetBounds());
            orange1.SetInStartPosition(paddle1.GetBounds());
            bricks = new Brick[bricksWide, bricksHigh];
            for (int y = 0; y < bricksHigh; y++)
            {
                Color tint = Color.White;
                Random r = new Random();
                Color newColor = new Color(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255), r.Next(0, 255));
                switch (y)
                {
                    case 0:
                        tint = Color.Blue;
                        break;
                    case 1:
                        tint = Color.Red;
                        break;
                    case 2:
                        tint = Color.Green;
                        break;
                    case 3:
                        tint = Color.Yellow;
                        break;
                    case 4:
                        tint = Color.Purple;
                        break;
                }

                for (int x = 0; x < bricksWide; x++)
                {

                    if (y < 4 && y > 1 && x < 6 && x > 3)
                    {
                        bricks[x, y] = new Brick(
                        brickImage,
                        new Rectangle(
                        x * brickImage.Width,
                        y * brickImage.Height,
                        brickImage.Width,
                        brickImage.Height),
                        Color.White, true);
                    }
                    else
                    {
                        bricks[x, y] = new Brick(
                        brickImage,
                        new Rectangle(
                        x * brickImage.Width,
                        y * brickImage.Height,
                        brickImage.Width,
                        brickImage.Height),
                        tint,false);
                    }
                }
            }
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
            //int count = 0;
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            paddle1.Update();
            ball1.Update();
            orange1.Update();


            foreach (Brick brick in bricks)
            {

                //if (countBricks < 3)
               // {
                    //countBricks++;
                   colided= brick.CheckCollision(ball1);

                   if (colided )
                    {
                        
                        marks = marks + 2;
                        

                        countBricks++;
                        if (countBricks == 46)
                        {
                            System.Windows.Forms.MessageBox.Show("Next Level :)");
                            bricksHigh = bricksHigh + 5;
                            const float ballStartSpeed = 8f;
                            ball1.SetInStartPosition(paddleLocation);
                            StartGame();
                        }
                    } newMarks = marks;
            }

            ball1.PaddleCollision(paddle1.GetBounds());
            orange1.PaddleCollision(paddle1.GetBounds());

            if (count < 4)
            {
                if (ball1.OffBottom())
                {
                    oldScore = higherScore;
                    if (oldScore > newMarks)
                    {
                        higherScore = oldScore;
                    }
                    else
                        higherScore = newMarks;
                    marks = 0;
                    count++;
                    StartGame();
                    
                }
            }
            else
            {
                offmessage = true;
                //System.Windows.Forms.MessageBox.Show("Game Over!!!");
                
            }

            if (offmessage)
            {
                System.Windows.Forms.MessageBox.Show("Game Over!!!");
                offmessage = false;
                count = 0;
                StartGame();
            }

            
               
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            

            paddle1.Draw(spriteBatch);
            ball1.Draw(spriteBatch);
            orange1.Draw(spriteBatch);
            bomb1.Draw(spriteBatch);

            foreach (Brick brick in bricks)
                brick.Draw(spriteBatch);

            //foreach (Brick brick in bricks)
                //if (ball1.OffBottom())
                //{
                //    spriteBatch.DrawString(font1, "Points : " + brick.marks, new Vector2(0, 0), Color.White);
                //}
                //else
                    spriteBatch.DrawString(font1, "Points : " + marks, new Vector2(0, 0), Color.White);

                    spriteBatch.DrawString(font1, "Higher Score : " + higherScore, new Vector2(500,0), Color.White);

             spriteBatch.End();

             
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
