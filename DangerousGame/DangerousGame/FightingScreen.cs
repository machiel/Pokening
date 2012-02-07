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

        public void Initialize(ContentManager contentManager)
        {
            Texture2D squirtle = contentManager.Load<Texture2D>("squirtle");
            Texture2D bulbasaur = contentManager.Load<Texture2D>("bulbasaur");
            Texture2D charmander = contentManager.Load<Texture2D>("charmander");

            Monster mSquirtle = new Monster(squirtle, "Squirtle");
            Monster mBulbasaur = new Monster(squirtle, "Bulbasaur");
            Monster mCharmander = new Monster(squirtle, "Charmander");

            Monsters.Add(mSquirtle);
            Monsters.Add(mBulbasaur);
            Monsters.Add(mCharmander);


        }

        public void Reinitialize()
        {
            Random rand = new Random();
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

            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                return Pokening.Screens.WorldScreen;
            }
            else
            {
                return Pokening.Screens.FightingScreen;
            }
        }

        public void Draw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            Texture2D text = new Texture2D(graphics.GraphicsDevice, Properties.WindowWidth, Properties.WindowHeight);

            Color[] data = new Color[Properties.WindowWidth * Properties.WindowHeight];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.White;
            text.SetData(data);

            spriteBatch.Draw(text, Vector2.Zero, Color.White);
            spriteBatch.DrawString(SpriteFont, "You're in a fight, bro! Press ESC to flee!", new Vector2(10, 10), Color.Black);
            spriteBatch.DrawString(SpriteFont, "A wild " + Monster.GetName() + " appeared!", new Vector2(10, 30), Color.Black);
            spriteBatch.DrawString(SpriteFont, "Level: " + Monster.GetLevel(), new Vector2(100, 50), Color.Black);

            Texture2D monsterTexture = Monster.GetTexture();
            spriteBatch.Draw(monsterTexture, new Vector2(500, 20), Color.White);

        }
    }
}
