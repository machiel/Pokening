using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace DangerousGame
{
    class FightingScreen : Screen
    {
        SpriteFont SpriteFont;
        Monster Monster;
        List<Monster> Monsters = new List<Monster>();

        bool IsAttacking = false;

        public void Initialize(ContentManager contentManager)
        {
            Texture2D squirtle = contentManager.Load<Texture2D>("squirtle");
            Texture2D bulbasaur = contentManager.Load<Texture2D>("bulbasaur");
            Texture2D charmander = contentManager.Load<Texture2D>("charmander");
            Texture2D pikachu = contentManager.Load<Texture2D>("pikachu");


            Monster mSquirtle = new Monster(squirtle, "Squirtle");
            Monster mBulbasaur = new Monster(bulbasaur, "Bulbasaur");
            Monster mCharmander = new Monster(charmander, "Charmander");
            Monster mPikachu = new Monster(pikachu, "Pikachu");

            Monsters.Add(mSquirtle);
            Monsters.Add(mBulbasaur);
            Monsters.Add(mCharmander);
            Monsters.Add(mPikachu);


        }

        public void Reinitialize(int seed)
        {
            Random rand = new Random(seed);
            int i = rand.Next(0, Monsters.Count);
            Console.Out.WriteLine(i);
            Monster = Monsters[i];
            Monster.Reset(rand.Next(1, 6));
        }

        public void LoadContent(ContentManager contentManager)
        {
            SpriteFont = contentManager.Load<SpriteFont>("Calibri");
        }

        public Pokening.Screens Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            Keys[] pressedKeys = keyboardState.GetPressedKeys();

            if(keyboardState.IsKeyUp(Keys.F)) {
                IsAttacking = false;
            }

            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                return Pokening.Screens.WorldScreen;
            }
            else
            {

                if (pressedKeys.Contains(Keys.F) && !IsAttacking)
                {
                    Monster.DecreaseHealth(5);
                    IsAttacking = true;
                }

                return Pokening.Screens.FightingScreen;
            }
        }

        public void Draw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            Texture2D text = new Texture2D(graphics.GraphicsDevice, Properties.WindowWidth, Properties.WindowHeight);

            Color[] data = new Color[Properties.WindowWidth * Properties.WindowHeight];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.White;
            text.SetData(data);

            Texture2D healthTexture = new Texture2D(graphics.GraphicsDevice, 100, 20);

            int health = Monster.GetHealth();
            Console.Out.WriteLine(health);

            Color[] healthData = new Color[100 * 20];

            Console.Out.WriteLine((int) (((float)health / 100) * (100 * 20)));

            for (int x = 0; x < 100; x++) {

                Color c = Color.Green;

                if (x > (int)(((float)health / 100) * 100))
                {
                    c = Color.Red;
                }

                for (int y = 0; y < 20; y++)
                {
                    healthData[100*y + x] = c; 
                }

                
            }

            healthTexture.SetData(healthData);

            spriteBatch.Draw(text, Vector2.Zero, Color.White);
            //spriteBatch.DrawString(SpriteFont, "A wild " + Monster.GetName() + " appeared!", new Vector2(10, 30), Color.Black);
            spriteBatch.DrawString(SpriteFont, Monster.GetName().ToUpper(), new Vector2(40, 30), Color.Black);
            spriteBatch.DrawString(SpriteFont, ":L" + Monster.GetLevel(), new Vector2(50, 50), Color.Black);
            spriteBatch.DrawString(SpriteFont, "HP:", new Vector2(50, 70), Color.Black);
            spriteBatch.Draw(healthTexture, new Vector2(80, 70), Color.White);

            spriteBatch.DrawString(SpriteFont, "PIKACHU", new Vector2(600, 400), Color.Black);
            spriteBatch.DrawString(SpriteFont, ":L5", new Vector2(610, 420), Color.Black);
            spriteBatch.DrawString(SpriteFont, "HP:", new Vector2(610, 440), Color.Black);
            spriteBatch.Draw(healthTexture, new Vector2(640, 440), Color.White);

            Texture2D monsterTexture = Monster.GetTexture();
            spriteBatch.Draw(monsterTexture, new Vector2(500, 20), Color.White);

        }
    }
}
