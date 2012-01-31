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

        MainCharacter Player;

        Map Map;

        String displayText = "";

        Boolean gamePaused = false;

        SpriteFont spriteFont;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.Window.Title = "Pokening";

            // Window size
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
            Map = new Map();

            Player = new MainCharacter(Map);
            Player.Position.X = (400 - 16);
            Player.Position.Y = (300 - 16);
            Player.setAnimation(32, 46);
            Player.addAnimation("walkDown", new int[] { 1, 2 }, 4);
            Player.addAnimation("walkUp", new int[] { 4, 5 }, 4);
            Player.addAnimation("walkLeft", new int[] { 7, 8 }, 5);
            Player.addAnimation("walkRight", new int[] { 10, 11 }, 5);

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
            Player.LoadContent(this.Content, "mainChar");
            Map.LoadTiles(this.Content, "tiles");
            Map.CreateMap(this.Content, "map-example");
            spriteFont = Content.Load<SpriteFont>("Calibri");
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

            // Pausing game when hitting the escape key
            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                if (gamePaused) gamePaused = false;
                else gamePaused = true;
            }

            if(!gamePaused) { 
                // Allows the game to exit
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                    this.Exit();

                // Updating Pikachu
                Player.Update(gameTime);
            }
            
            // Updating parent class
            base.Update(gameTime);
        }

        private bool IntersectPixels(Sprite obj1Sprite, Sprite obj2Sprite)
        {
            // Getting the position and dimensions of both sprites
            Rectangle obj1 = new Rectangle((int)obj1Sprite.Position.X, (int)obj1Sprite.Position.Y, obj1Sprite.Size.Width, obj1Sprite.Size.Height);
            Rectangle obj2 = new Rectangle((int)obj2Sprite.Position.X, (int)obj2Sprite.Position.Y, obj2Sprite.Size.Width, obj2Sprite.Size.Height);

            // Getting the texture data from both sprites
            Color[] obj1Texture = obj1Sprite.getTextureData();
            Color[] obj2Texture = obj2Sprite.getTextureData();

            // Find the bounds of the rectangle intersection
            int top = Math.Max(obj1.Top, obj2.Top);
            int bottom = Math.Min(obj1.Bottom, obj2.Bottom);
            int left = Math.Max(obj1.Left, obj2.Left);
            int right = Math.Min(obj1.Right, obj2.Right);

            // Check every point within the intersection bounds
            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    // Get the color of both pixels at this point
                    Color colorA = obj1Texture[(x - obj1.Left) + ((y - obj1.Top) * obj1.Width)];
                    Color colorB = obj2Texture[(x - obj2.Left) + ((y - obj2.Top) * obj2.Width)];

                    // If both pixels are not completely transparent,
                    if (colorA.A != 0 && colorB.A != 0)
                    {
                        // Then an intersection has been found
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
            // Clearing the stag with specified color
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            Map.Draw(spriteBatch);
            Player.Draw(spriteBatch);
            
            spriteBatch.DrawString(spriteFont, displayText, new Vector2(600, 10), Color.Black);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
