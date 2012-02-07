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

        public void Initialize()
        {
            
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
        }
    }
}
