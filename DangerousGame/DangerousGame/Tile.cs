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
        const string ROOF = "2327777";
        const string WALL = "255255255";
        const string FOREST = "7516137";
        
        public string sort;

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
            this.sort = color;
            this.Type = type;
            this.TileMap = tileMap;
            this.Obstacle = obstacle;

            if (sort == GRASS)
                TileType = 21;
            else if (sort == PATH && type == TL)
                TileType = 3;
            else if (sort == PATH && type == TM)
                TileType = 4;
            else if (sort == PATH && type == TR)
                TileType = 5;
            else if (sort == PATH && type == L)
                TileType = 13;
            else if (sort == PATH && type == M)
                TileType = 14;
            else if (sort == PATH && type == R)
                TileType = 15;
            else if (sort == PATH && type == BL)
                TileType = 23;
            else if (sort == PATH && type == BM)
                TileType = 24;
            else if (sort == PATH && type == BR)
                TileType = 25;
            else if (sort == ROOF && type == BM)
                TileType = 28;
            else if (sort == ROOF && type == BL)
                TileType = 26;
            else if (sort == ROOF && type == BR)
                TileType = 30;
            else if (sort == ROOF && (type == TL || type == TM || type == TR))
                TileType = 8;
            else if (sort == ROOF)
                TileType = 18;
            else if (sort == WALL && type == BL)
                TileType = 36;
            else if (sort == WALL && type == BR)
                TileType = 40;
            else if (sort == WALL && type == BM)
                TileType = 38;
            else if (sort == WALL && (type == M || type == TM))
                TileType = 27;
            else if (sort == WALL && (type == L || type == TL))
                TileType = 19;
            else if (sort == WALL && (type == R || type == TR))
                TileType = 29;
            else if (sort == FOREST)
                TileType = 31;
        }

        public bool IsObstacle()
        {
            return this.Obstacle;
        }

        public Rectangle GetTile()
        {
            int y = ((TileType - 1) / (TileMap.Width / Properties.TileHeight)) * Properties.TileHeight;
            int x = ((TileType - 1) % (TileMap.Width / Properties.TileWidth)) * Properties.TileWidth;
            return new Rectangle(x, y, x + Properties.TileWidth, y + Properties.TileHeight);
        }
    }
}
