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
        public void Initialize()
        {

        }

        public void LoadContent(ContentManager contentManager)
        {

        }

        public Pokening.Screens Update(GameTime gameTime)
        {

            return Pokening.Screens.FightingScreen;
        }

        public void Draw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            Texture2D text = new Texture2D(graphics.GraphicsDevice, Properties.WindowWidth, Properties.WindowHeight);

            Color[] data = new Color[Properties.WindowWidth * Properties.WindowHeight];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.White;
            text.SetData(data);

            spriteBatch.Draw(text, Vector2.Zero, Color.White);
        }
    }
}
