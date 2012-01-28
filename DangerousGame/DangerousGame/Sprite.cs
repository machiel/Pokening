using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DangerousGame
{
    class Sprite
    {
        public Vector2 Position = new Vector2(0, 0);
        public Rectangle Size;
        public float Scale = 1.0f;
        protected Texture2D mTexture;
        protected Color[] textureData;

        public void LoadContent(ContentManager contentManager, string theAsset)
        {
            mTexture = contentManager.Load<Texture2D>(theAsset);
            Size = new Rectangle(0, 0, (int) (mTexture.Width * Scale), (int) (mTexture.Height * Scale));
            textureData = new Color[mTexture.Width * mTexture.Height];
            mTexture.GetData(textureData);
        }

        public Rectangle getRectangle()
        {
            return Size;
        }

        public Color[] getTextureData()
        {
            return textureData;
        }

        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(mTexture, Position,
                new Rectangle(0, 0, mTexture.Width, mTexture.Height), Color.White,
                0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }

        public void Update(GameTime gameTime, Vector2 theSpeed, Vector2 theDirection)
        {
            Position += theDirection * theSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
