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
    class DrawableAttack
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public Vector2 Direction;

        private Texture2D Texture;

        public DrawableAttack(Vector2 position, Texture2D texture)
        {
            Position = position;
            Direction = new Vector2(1, -2);
            Velocity = new Vector2(1, 1);
            Texture = texture;
        }

        public void Update(GameTime gameTime)
        {
            Position += (Direction * Velocity);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 mapPosition)
        {
            spriteBatch.Draw(Texture, Position - mapPosition, Color.White);
        }
    }
}
