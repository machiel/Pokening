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
        private int Health = 100;

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
            Health = 100;
            Level = level;
        }

        public void DecreaseHealth(int decrement)
        {
            Health -= decrement;
        }

        public int GetHealth()
        {
            return Health;
        }

        public Texture2D GetTexture()
        {
            return Texture;
        }
    }
}
