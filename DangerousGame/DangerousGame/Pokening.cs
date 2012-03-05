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
        public static Pokening Instance { get; private set; }
        GraphicsDeviceManager Graphics;
        SpriteBatch SpriteBatch;

        public enum Screens
        {
            WorldScreen,
            FightingScreen,
            ItemScreen,
            MonsterScreen
        };

        private Screens CurrentScreen;
        private Screens PreviousScreen;

        WorldScreen WorldScreen;
        FightingScreen FightingScreen;
        SpriteFont SpriteFont;

        List<Monster> Monsters = new List<Monster>();

        Player Player;

        public Pokening()
        {

            Instance = this;

            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.Window.Title = "Pokening";

            CurrentScreen = Screens.WorldScreen;

            WorldScreen = new WorldScreen();
            FightingScreen = new FightingScreen();

            // Window size
            this.Graphics.PreferredBackBufferWidth = Properties.WindowWidth;
            this.Graphics.PreferredBackBufferHeight = Properties.WindowHeight;
        }

        public WorldScreen GetWorldScreen()
        {
            return this.WorldScreen;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            this.IsMouseVisible = true;

            Texture2D squirtle = Content.Load<Texture2D>("squirtle");
            Texture2D bulbasaur = Content.Load<Texture2D>("bulbasaur");
            Texture2D charmander = Content.Load<Texture2D>("charmander");
            Texture2D pikachu = Content.Load<Texture2D>("pikachu");


            Monster mSquirtle = new Monster(squirtle, "Squirtle");
            Monster mBulbasaur = new Monster(bulbasaur, "Bulbasaur");
            Monster mCharmander = new Monster(charmander, "Charmander");
            Monster mPikachu = new Monster(pikachu, "Pikachu");

            Monsters.Add(mSquirtle);
            Monsters.Add(mBulbasaur);
            Monsters.Add(mCharmander);
            Monsters.Add(mPikachu);

            Player = new Player(Monsters);

            WorldScreen.Initialize(this.Content);
            FightingScreen.Initialize(this.Content);
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
            FightingScreen.LoadContent(this.Content);
            SpriteFont = this.Content.Load<SpriteFont>("Calibri");

            //Objects.LoadTiles(this.Content, "objects");
            //Objects.CreateMap(this.Content, "objectsMap");
            
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

            PreviousScreen = CurrentScreen;

            if (CurrentScreen == Screens.WorldScreen)
            {
                CurrentScreen = WorldScreen.Update(gameTime);
            }
            else if (CurrentScreen == Screens.FightingScreen)
            {
                CurrentScreen = FightingScreen.Update(gameTime);
            }

            if (PreviousScreen != CurrentScreen && CurrentScreen == Screens.FightingScreen)
            {
                Random rand = new Random(gameTime.TotalGameTime.Milliseconds);
                int i = rand.Next(0, Monsters.Count);

                rand = new Random((int)gameTime.TotalGameTime.TotalSeconds);
                int lvl = rand.Next(1, 5);

                List<Attack> attacks = new List<Attack>();
                attacks.Add(new Attack(15, "Tackle"));
                attacks.Add(new Attack(0, "Growl"));

                Monster monster = (Monster)Monsters[i].Clone();
                monster.Reset(lvl, attacks);

                Battle battle = new Battle(Player, monster);
                FightingScreen.Reinitialize(battle);
            }
            
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

            if (CurrentScreen == Screens.WorldScreen)
            {
                WorldScreen.Draw(Graphics, SpriteBatch);
            }
            else if (CurrentScreen == Screens.FightingScreen)
            {
                FightingScreen.Draw(Graphics, SpriteBatch);
            }

            SpriteBatch.DrawString(SpriteFont, "v0.3.0", new Vector2(749, 4), Color.Black);
            SpriteBatch.DrawString(SpriteFont, "v0.3.0", new Vector2(751, 4), Color.Black);
            SpriteBatch.DrawString(SpriteFont, "v0.3.0", new Vector2(749, 6), Color.Black);
            SpriteBatch.DrawString(SpriteFont, "v0.3.0", new Vector2(751, 6), Color.Black);
            SpriteBatch.DrawString(SpriteFont, "v0.3.0", new Vector2(750, 5), Color.White);
            
            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
