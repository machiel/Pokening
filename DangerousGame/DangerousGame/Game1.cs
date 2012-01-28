using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace DangerousGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        MainCharacter mPikachu;
        Sprite mBulbasaur;
        Sprite mCharmander;
        Sprite mSquirtle;
        Sprite mBackground;

        String displayText = "";

        private bool pikachuHit = false;

        Boolean gamePaused = false;

        SpriteFont spriteFont;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.Window.Title = "Pokémon";
            this.graphics.PreferredBackBufferWidth = 800;
            this.graphics.PreferredBackBufferHeight = 600;
            //this.graphics.IsFullScreen = true;
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

            mPikachu = new MainCharacter();
            mBulbasaur = new Sprite();
            mCharmander = new Sprite();
            mSquirtle = new Sprite();
            mBackground = new Sprite();
            mBackground.Scale = 1.5f;

            mBulbasaur.Position.X = 200;
            mCharmander.Position.Y = 200;
            mSquirtle.Position.Y = 200;
            mSquirtle.Position.X = 200;

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
            mPikachu.LoadContent(this.Content, "pikachu");
            mBulbasaur.LoadContent(this.Content, "bulbasaur");
            mSquirtle.LoadContent(this.Content, "squirtle");
            mCharmander.LoadContent(this.Content, "charmander");
            mBackground.LoadContent(this.Content, "Background01");
            spriteFont = Content.Load<SpriteFont>("Calibri");
            // TODO: use this.Content to load your game content here
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

            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                if (gamePaused) gamePaused = false;
                else gamePaused = true;
            }

            if(!gamePaused) { 
                // Allows the game to exit
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                    this.Exit();


                // TODO: Add your update logic here
                mPikachu.Update(gameTime);

                Rectangle pikachuRectangle = mPikachu.getRectangle();
                Rectangle squirtleRectangle = mSquirtle.getRectangle();

                if (IntersectPixels(pikachuRectangle, mPikachu.getTextureData(), squirtleRectangle, mSquirtle.getTextureData()))
                {
                    this.displayText = "Ouch!";
                }
                else
                {
                    this.displayText = "No worries. No hits!";
                }
            }
            base.Update(gameTime);
        }

        private bool IntersectPixels(Rectangle one, Color[] textureOne, Rectangle two, Color[] textureTwo)
        {
            // Find the bounds of the rectangle intersection
            int top = Math.Max(one.Top, two.Top);
            int bottom = Math.Min(one.Bottom, two.Bottom);
            int left = Math.Max(one.Left, two.Left);
            int right = Math.Min(one.Right, two.Right);

            // Check every point within the intersection bounds
            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    // Get the color of both pixels at this point
                    Color colorA = textureOne[(x - one.Left) +
                                         (y - one.Top) * one.Width];
                    Color colorB = textureTwo[(x - two.Left) +
                                         (y - two.Top) * two.Width];

                    // If both pixels are not completely transparent,
                    if (colorA.A != 0 && colorB.A != 0)
                    {
                        // then an intersection has been found
                        return true;
                    }
                }
            }
            return false;
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
            mBackground.Draw(spriteBatch);
            mSquirtle.Draw(spriteBatch);
            mCharmander.Draw(spriteBatch);
            mBulbasaur.Draw(spriteBatch);
            mPikachu.Draw(spriteBatch);
            spriteBatch.DrawString(spriteFont, displayText, new Vector2(400, 10), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
