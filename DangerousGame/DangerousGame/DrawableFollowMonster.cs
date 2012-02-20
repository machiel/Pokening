using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections;

namespace DangerousGame
{
    class DrawableFollowMonster : DrawableMonster
    {

        private List<DrawableAttack> Attacks = new List<DrawableAttack>();
        Texture2D AttackTexture;

        private int LastAction = 0;

        public void AddTexture(Texture2D name)
        {
            AttackTexture = name;
        }

        public void DrawFollow(SpriteBatch spriteBatch, Vector2 mapPosition)
        {
            Vector2 tempPos = Position - mapPosition;

            for (int i = 0; i < Attacks.Count; i++)
            {
                Attacks[i].Draw(spriteBatch, mapPosition);
            }

            if (tempPos.X >= 0 && tempPos.X <= Properties.WindowWidth
                && tempPos.Y >= 0 && tempPos.Y <= Properties.WindowHeight)
            {

                if (!IsAnimated)
                    spriteBatch.Draw(Texture, tempPos, new Rectangle(0, 0, Texture.Width, Texture.Height), Color.White, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
                else
                {
                    int framePosX = 0;
                    int framePosY = 0;
                    if (IsPlaying)
                    {
                        framePosY = (CurrentAnimation.GetFrame() / (Texture.Width / FrameWidth)) * FrameHeight;
                        framePosX = (CurrentAnimation.GetFrame() % (Texture.Width / FrameWidth)) * FrameWidth;
                    }
                    spriteBatch.Draw(Texture, GetCenter(), new Rectangle(framePosX, framePosY, FrameWidth, FrameHeight), Color.White, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
                }
            }
        }

        public void UpdateFollow(GameTime gameTime)
        {

            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.D1) && gameTime.TotalGameTime.Seconds - LastAction >= 1)
            {
                DrawableAttack attack = new DrawableAttack(this.Position, AttackTexture);
                Attacks.Add(attack);

                LastAction = gameTime.TotalGameTime.Seconds;
            }

            for (int i = 0; i < Attacks.Count; i++)
            {
                Attacks[i].Update(gameTime);
            }

            //if (IsPlaying)
            //    if (gameTime.TotalGameTime.Ticks % (100 / CurrentAnimation.FrameRate) == 0)
             //       CurrentAnimation.NextFrame();
            //Position += theDirection * theSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
