using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DangerousGame
{
    class Animation
    {
        public string name;
        public int[] frames;
        public int frameRate;
        public int currentFrame;

        public Animation(string name, int[] frames, int frameRate)
        {
            this.name = name;
            this.frames = frames;
            this.frameRate = frameRate;
            this.currentFrame = 0;
        }

        /// <summary>
        /// Makes the animation go to the next frame. So the drawer knows which frame to draw
        /// </summary>
        public void nextFrame()
        {
            if (currentFrame + 1 < frames.Length)
                currentFrame++;
            else
                this.currentFrame = 0;
        }

        /// <summary>
        /// </summary>
        /// <returns>The current frame of the animation</returns>
        public int getFrame()
        {
            return frames[currentFrame];
        }
    }
}
