using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;

namespace DangerousGame
{
    class FightBar
    {
        public void Draw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, SpriteFont spriteFont)
        {

            // Background bar
            Texture2D text = new Texture2D(graphics.GraphicsDevice, 262, 30);

            Color[] data = new Color[262 * 30];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.White;
            text.SetData(data);
            spriteBatch.Draw(text, new Vector2(261, 570), Color.White);

            Texture2D text1 = new Texture2D(graphics.GraphicsDevice, 28, 28);

            Color[] data1 = new Color[28 * 28];
            for (int i = 0; i < data1.Length; ++i) data1[i] = Color.Black;
            text1.SetData(data1);

            for (int b = 0; b < 9; b++)
            {
                spriteBatch.Draw(text1, new Vector2(262 + (29 * b), 571), Color.Black);
                spriteBatch.DrawString(spriteFont, "" + (b+1), new Vector2(276 + (29 * b), 580), Color.White);
            }

     
        }
    }
}
