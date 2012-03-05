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
    public class DrawableMonster
    {
        public Vector2 Position = new Vector2(0, 0);
        public Rectangle Size;
        public float Scale = 1.0f;
        public Texture2D Texture;
        protected Color[] TextureData;

        protected bool IsAnimated = false;
        protected bool IsPlaying = false;
        public float Health = 100.0f;
        protected int FrameWidth;
        protected int FrameHeight;
        public Animation CurrentAnimation;

        protected ArrayList Animations = new ArrayList();

        public void LoadContent(ContentManager contentManager, string theAsset)
        {
            Texture = contentManager.Load<Texture2D>(theAsset);
            Size = new Rectangle(0, 0, (int)(Texture.Width * Scale), (int)(Texture.Height * Scale));
            TextureData = new Color[Texture.Width * Texture.Height];
            Texture.GetData(TextureData);
        }

        public Rectangle GetRectangle()
        {
            return Size;
        }

        public Color[] GetTextureData()
        {
            return TextureData;
        }

        public void DecreaseHealth(float decrease)
        {
            Health -= decrease;
        }

        public static Vector2 GetCenter()
        {
            return new Vector2((Properties.WindowWidth - Properties.MainCharacterWidth) / 2, (Properties.WindowHeight - Properties.MainCharacterHeight) / 2);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 mapPosition)
        {
            Vector2 tempPos = Position - mapPosition;

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

        public void Update(GameTime gameTime, Vector2 theSpeed, Vector2 theDirection)
        {
            if (IsPlaying)
                if (gameTime.TotalGameTime.Ticks % (100 / CurrentAnimation.FrameRate) == 0)
                    CurrentAnimation.NextFrame();
            Position += theDirection * theSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        /// <summary>
        /// Makes the sprite an animation.
        /// </summary>
        /// <param name="frameWidth">The width of one frame of the animation sprite</param>
        /// <param name="frameHeight">The height of one frame of the animation sprite</param>
        public void SetAnimation(int frameWidth, int frameHeight)
        {
            IsAnimated = true;
            this.FrameWidth = frameWidth;
            this.FrameHeight = frameHeight;
        }

        /// <summary>
        /// Add an animation to the sprite
        /// </summary>
        /// <param name="name">The name of the animation</param>
        /// <param name="frames">What frames of the sprite will be used in this animation (i.e. int[2,3,4,8]</param>
        /// <param name="frameRate">How many times in a second the animation should procceed to the next frame</param>
        public void AddAnimation(string name, int[] frames, int frameRate)
        {
            Animations.Add(new Animation(name, frames, frameRate));
        }

        /// <summary>
        /// Start an already defined animation by name
        /// </summary>
        /// <param name="animationName">Name of the animation to start</param>
        public void Play(string animationName)
        {
            foreach (Animation animation in Animations)
            {
                if (animation.Name == animationName)
                {
                    CurrentAnimation = animation;
                    IsPlaying = true;
                }
            }
        }

        /// <summary>
        /// Stops the current animation with playing. Makes the sprite returns to
        /// its first frame
        /// </summary>
        public void Stop()
        {
            IsPlaying = false;
        }
    }
}
