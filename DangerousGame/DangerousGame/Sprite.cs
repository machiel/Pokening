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
    class Sprite
    {
        public Vector2 Position = new Vector2(0, 0);
        public Rectangle Size;
        public float Scale = 1.0f;
        protected Texture2D Texture;
        protected Color[] TextureData;

        protected bool IsAnimated = false;
        protected bool IsPlaying = false;
        protected int FrameWidth;
        protected int FrameHeight;
        protected Animation CurrentAnimation;

        protected ArrayList Animations = new ArrayList();

        public void LoadContent(ContentManager ContentManager, string TheAsset)
        {
            Texture = ContentManager.Load<Texture2D>(TheAsset);
            Size = new Rectangle(0, 0, (int) (Texture.Width * Scale), (int) (Texture.Height * Scale));
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

        public static Vector2 GetCenter()
        {
            return new Vector2(400 - 16, 300 - 16);
        }

        public void Draw(SpriteBatch SpriteBatch)
        {
            if (!IsAnimated)
                SpriteBatch.Draw(Texture, Position, new Rectangle(0, 0, Texture.Width, Texture.Height), Color.White, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
            else
            {
                int FramePosX = 0;
                int FramePosY = 0;
                if (IsPlaying)
                {
                    FramePosY = (CurrentAnimation.getFrame() / (Texture.Width / FrameWidth)) * FrameHeight;
                    FramePosX = (CurrentAnimation.getFrame() % (Texture.Width / FrameWidth)) * FrameWidth;
                }
                SpriteBatch.Draw(Texture, GetCenter(), new Rectangle(FramePosX, FramePosY, FrameWidth, FrameHeight), Color.White, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
            }
        }

        public void Update(GameTime GameTime, Vector2 TheSpeed, Vector2 TheDirection)
        {
            if(IsPlaying)
                if (GameTime.TotalGameTime.Ticks % (100 / CurrentAnimation.frameRate) == 0)
                    CurrentAnimation.nextFrame();
            Position += TheDirection * TheSpeed * (float)GameTime.ElapsedGameTime.TotalSeconds;
        }

        /// <summary>
        /// Makes the sprite an animation.
        /// </summary>
        /// <param name="FrameWidth">The width of one frame of the animation sprite</param>
        /// <param name="FrameHeight">The height of one frame of the animation sprite</param>
        public void SetAnimation(int FrameWidth, int FrameHeight)
        {
            IsAnimated = true;
            this.FrameWidth = FrameWidth;
            this.FrameHeight = FrameHeight;
        }

        /// <summary>
        /// Add an animation to the sprite
        /// </summary>
        /// <param name="Name">The name of the animation</param>
        /// <param name="Frames">What frames of the sprite will be used in this animation (i.e. int[2,3,4,8]</param>
        /// <param name="FrameRate">How many times in a second the animation should procceed to the next frame</param>
        public void AddAnimation(string Name, int[] Frames, int FrameRate)
        {
            Animations.Add(new Animation(Name, Frames, FrameRate));
        }

        /// <summary>
        /// Start an already defined animation by name
        /// </summary>
        /// <param name="AnimationName">Name of the animation to start</param>
        public void Play(string AnimationName)
        {
            foreach(Animation Animation in Animations)
            {
                if (Animation.name == AnimationName)
                {
                    CurrentAnimation = Animation;
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
