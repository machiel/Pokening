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

        public bool Used = false;

        public DrawableAttack(Vector2 position, Texture2D texture, Vector2 direction)
        {
            Position = position;
            direction.Normalize();
            Direction = direction;
            Velocity = new Vector2(1, 1);
            Texture = texture;
        }

        public void Update(GameTime gameTime)
        {
            List<DrawableMonster> monsters = Pokening.Instance.GetWorldScreen().Monsters;
            Rectangle aBounds = Texture.Bounds;
            Vector2 aPos = Position;

            foreach(DrawableMonster monster in monsters) {

                Rectangle bBounds = monster.Texture.Bounds;
                Vector2 bPos = monster.Position;
                
                if(aPos.X + aBounds.Width > bPos.X &&
                    aPos.X < bPos.X + bBounds.Width &&
                    aPos.Y + aBounds.Height > bPos.Y &&
                    aPos.Y < bPos.Y + bBounds.Height) 
                {

                    monster.DecreaseHealth(10);
                    Used = true;
                }

            }

            Position += (Direction * Velocity);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 mapPosition)
        {
            spriteBatch.Draw(Texture, Position - mapPosition, Color.White);
        }
    }
}
