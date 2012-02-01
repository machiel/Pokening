using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DangerousGame
{
    class Animation
    {
        public string Name;
        public int[] Frames;
        public int FrameRate;
        public int CurrentFrame;

        public Animation(string Name, int[] Frames, int FrameRate)
        {
            this.Name = Name;
            this.Frames = Frames;
            this.FrameRate = FrameRate;
            this.CurrentFrame = 0;
        }

        /// <summary>
        /// Makes the animation go to the next frame. So the drawer knows which frame to draw
        /// </summary>
        public void NextFrame()
        {
            if (CurrentFrame + 1 < Frames.Length)
                CurrentFrame++;
            else
                this.CurrentFrame = 0;
        }

        /// <summary>
        /// </summary>
        /// <returns>The current frame of the animation</returns>
        public int GetFrame()
        {
            return Frames[CurrentFrame];
        }
    }
}
