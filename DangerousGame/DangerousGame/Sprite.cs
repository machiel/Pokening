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
        protected Color[] textureData;

        protected bool isAnimated = false;
        protected bool isPlaying = false;
        protected int frameWidth;
        protected int frameHeight;
        protected Animation currentAnimation;

        protected ArrayList animations = new ArrayList();

        public void LoadContent(ContentManager contentManager, string theAsset)
        {
            Texture = contentManager.Load<Texture2D>(theAsset);
            Size = new Rectangle(0, 0, (int) (Texture.Width * Scale), (int) (Texture.Height * Scale));
            textureData = new Color[Texture.Width * Texture.Height];
            Texture.GetData(textureData);
        }

        public Rectangle getRectangle()
        {
            return Size;
        }

        public Color[] getTextureData()
        {
            return textureData;
        }

        public static Vector2 getCenter()
        {
            return new Vector2(400 - 16, 300 - 16);
        }

        public void Draw(SpriteBatch SpriteBatch)
        {
            if (!isAnimated)
                SpriteBatch.Draw(Texture, Position, new Rectangle(0, 0, Texture.Width, Texture.Height), Color.White, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
            else
            {
                int framePosX = 0;
                int framePosY = 0;
                if (isPlaying)
                {
                    framePosY = (currentAnimation.getFrame() / (Texture.Width / frameWidth)) * frameHeight;
                    framePosX = (currentAnimation.getFrame() % (Texture.Width / frameWidth)) * frameWidth;
                }
                SpriteBatch.Draw(Texture, getCenter(), new Rectangle(framePosX, framePosY, frameWidth, frameHeight), Color.White, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
            }
        }

        public void Update(GameTime gameTime, Vector2 theSpeed, Vector2 theDirection)
        {
            if(isPlaying)
                if (gameTime.TotalGameTime.Ticks % (100 / currentAnimation.frameRate) == 0)
                    currentAnimation.nextFrame();
            Position += theDirection * theSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        /// <summary>
        /// Makes the sprite an animation.
        /// </summary>
        /// <param name="frameWidth">The width of one frame of the animation sprite</param>
        /// <param name="frameHeight">The height of one frame of the animation sprite</param>
        public void setAnimation(int frameWidth, int frameHeight)
        {
            isAnimated = true;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
        }

        /// <summary>
        /// Add an animation to the sprite
        /// </summary>
        /// <param name="name">The name of the animation</param>
        /// <param name="frames">What frames of the sprite will be used in this animation (i.e. int[2,3,4,8]</param>
        /// <param name="frameRate">How many times in a second the animation should procceed to the next frame</param>
        public void addAnimation(string name, int[] frames, int frameRate)
        {
            animations.Add(new Animation(name, frames, frameRate));
        }

        /// <summary>
        /// Start an already defined animation by name
        /// </summary>
        /// <param name="animationName">Name of the animation to start</param>
        public void play(string animationName)
        {
            foreach(Animation animation in animations)
            {
                if (animation.name == animationName)
                {
                    currentAnimation = animation;
                    isPlaying = true;
                }
            }
        }

        /// <summary>
        /// Stops the current animation with playing. Makes the sprite returns to
        /// its first frame
        /// </summary>
        public void stop()
        {
            isPlaying = false;
        }
    }
}
