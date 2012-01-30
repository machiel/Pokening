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
        public int type;

        // From top left to right bottom the number of which tile it is
        public int tile;

        private Texture2D _TileMap;

        public Tile(string color, int type, Texture2D _TileMap)
        {
            sort = color;
            this.type = type;
            this._TileMap = _TileMap;

            if (sort == GRASS)
                tile = 21;
            else if (sort == PATH && type == TL)
                tile = 3;
            else if (sort == PATH && type == TM)
                tile = 4;
            else if (sort == PATH && type == TR)
                tile = 5;
            else if (sort == PATH && type == L)
                tile = 13;
            else if (sort == PATH && type == M)
                tile = 14;
            else if (sort == PATH && type == R)
                tile = 15;
            else if (sort == PATH && type == BL)
                tile = 23;
            else if (sort == PATH && type == BM)
                tile = 24;
            else if (sort == PATH && type == BR)
                tile = 25;
            
        }

        public Rectangle getTile()
        {
            int y = ((tile-1) / (_TileMap.Width / 50)) * 50;
            int x = ((tile-1) % (_TileMap.Width / 50)) * 50;
            return new Rectangle(x, y, x + 50, y + 50);
        }
    }
}
