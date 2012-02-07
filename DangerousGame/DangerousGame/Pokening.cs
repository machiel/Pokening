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
    public class Pokening : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager Graphics;
        SpriteBatch SpriteBatch;

        String DisplayText = "";
        SpriteFont SpriteFont;

        WorldScreen WorldScreen;

        public Pokening()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.Window.Title = "Pokening";

            WorldScreen = new WorldScreen();

            // Window size
            this.Graphics.PreferredBackBufferWidth = Properties.WindowWidth;
            this.Graphics.PreferredBackBufferHeight = Properties.WindowHeight;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            WorldScreen.Initialize();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary> 
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            WorldScreen.LoadContent(this.Content);

            //Objects.LoadTiles(this.Content, "objects");
            //Objects.CreateMap(this.Content, "objectsMap");
            SpriteFont = Content.Load<SpriteFont>("Calibri");
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            WorldScreen.Update(gameTime);
            
            // Updating parent class
            base.Update(gameTime);
        }

        /*
        private bool IntersectPixels(Sprite object1, Sprite object2)
        {
            // Getting the position and dimensions of both sprites
            Rectangle rectangleObject1 = new Rectangle((int)object1.Position.X, (int)object1.Position.Y, object1.Size.Width, object1.Size.Height);
            Rectangle rectangleObject2 = new Rectangle((int)object2.Position.X, (int)object2.Position.Y, object2.Size.Width, object2.Size.Height);

            // Getting the texture data from both sprites
            Color[] colorObject1 = object1.GetTextureData();
            Color[] colorObject2 = object2.GetTextureData();

            // Find the bounds of the rectangle intersection
            int top = Math.Max(rectangleObject1.Top, rectangleObject2.Top);
            int bottom = Math.Min(rectangleObject1.Bottom, rectangleObject2.Bottom);
            int left = Math.Max(rectangleObject1.Left, rectangleObject2.Left);
            int right = Math.Min(rectangleObject1.Right, rectangleObject2.Right);

            // Check every point within the intersection bounds
            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    // Get the color of both pixels at this point
                    Color color1 = colorObject1[(x - rectangleObject1.Left) + ((y - rectangleObject1.Top) * rectangleObject1.Width)];
                    Color color2 = colorObject2[(x - rectangleObject2.Left) + ((y - rectangleObject2.Top) * rectangleObject2.Width)];

                    // If both pixels are not completely transparent,
                    if (color1.A != 0 && color2.A != 0)
                    {
                        // Then an intersection has been found
                        return true;
                    }
                }
            }
            return false;
        }*/

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // Clearing the stag with specified color
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteBatch.Begin();
            WorldScreen.Draw(SpriteBatch);
                        
            SpriteBatch.DrawString(SpriteFont, DisplayText, new Vector2(600, 10), Color.Black);
            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
