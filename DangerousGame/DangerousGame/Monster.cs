using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DangerousGame
{
    class Monster
    {
        private string Name;
        private int Level;
        private float HealthPercentage = 100.0f;

        private Texture2D Texture;

        public Monster(Texture2D texture, string name)
        {
            Texture = texture;
            Name = name;
        }

        public string GetName()
        {
            return Name;
        }

        public int GetLevel()
        {
            return Level;
        }

        public void Reset(int level)
        {
            HealthPercentage = 100.0f;
            Level = level;
        }

        public Texture2D GetTexture()
        {
            return Texture;
        }
    }
}
