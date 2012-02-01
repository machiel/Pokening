using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace DangerousGame
{
    class Tile
    {
        const string GRASS = "144233105";
        const string PATH = "254241154";
        public string Sort;

        public const int TL = 1;
        public const int TM = 2;
        public const int TR = 3;
        public const int L = 4;
        public const int M = 5;
        public const int R = 6;
        public const int BL = 7;
        public const int BM = 8;
        public const int BR = 9;
        public int Type;
        private bool Obstacle;

        // From top left to right bottom the number of which tile it is
        public int TileType;

        private Texture2D TileMap;

        public Tile(string color, int type, Texture2D tileMap, bool obstacle)
        {
            Sort = color;
            this.Type = type;
            this.TileMap = tileMap;
            this.Obstacle = obstacle;

            if (Sort == GRASS)
                TileType = 21;
            else if (Sort == PATH && type == TL)
                TileType = 3;
            else if (Sort == PATH && type == TM)
                TileType = 4;
            else if (Sort == PATH && type == TR)
                TileType = 5;
            else if (Sort == PATH && type == L)
                TileType = 13;
            else if (Sort == PATH && type == M)
                TileType = 14;
            else if (Sort == PATH && type == R)
                TileType = 15;
            else if (Sort == PATH && type == BL)
                TileType = 23;
            else if (Sort == PATH && type == BM)
                TileType = 24;
            else if (Sort == PATH && type == BR)
                TileType = 25;
            
        }

        public bool IsObstacle()
        {
            return this.Obstacle;
        }

        public Rectangle GetTile()
        {
            int y = ((TileType-1) / (TileMap.Width / 50)) * 50;
            int x = ((TileType-1) % (TileMap.Width / 50)) * 50;
            return new Rectangle(x, y, x + 50, y + 50);
        }
    }
}
