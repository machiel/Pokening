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
        public string Sort;

        public enum TileProperties
        {
            Aggressive,
            Obstacle,
            Normal
        };

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
        private TileProperties Property;

        public string objectColor;
        public int ObjectType;

        // From top left to right bottom the number of which tile it is
        public int TileType;

        private Texture2D TileMap;

        public Tile(string color, int type, Texture2D tileMap, TileProperties obstacle, string objectColor)
        {
            this.Sort = color;
            this.Type = type;
            this.TileMap = tileMap;
            this.Property = obstacle;
            this.objectColor = objectColor;

            if (Sort == Properties.TileColorCodes.Grass)
                TileType = 21;
            else if (Sort == Properties.TileColorCodes.Path && type == TL)
                TileType = 3;
            else if (Sort == Properties.TileColorCodes.Path && type == TM)
                TileType = 4;
            else if (Sort == Properties.TileColorCodes.Path && type == TR)
                TileType = 5;
            else if (Sort == Properties.TileColorCodes.Path && type == L)
                TileType = 13;
            else if (Sort == Properties.TileColorCodes.Path && type == M)
                TileType = 14;
            else if (Sort == Properties.TileColorCodes.Path && type == R)
                TileType = 15;
            else if (Sort == Properties.TileColorCodes.Path && type == BL)
                TileType = 23;
            else if (Sort == Properties.TileColorCodes.Path && type == BM)
                TileType = 24;
            else if (Sort == Properties.TileColorCodes.Path && type == BR)
                TileType = 25;
            else if (Sort == Properties.TileColorCodes.Roof && type == BM)
                TileType = 28;
            else if (Sort == Properties.TileColorCodes.Roof && type == BL)
                TileType = 26;
            else if (Sort == Properties.TileColorCodes.Roof && type == BR)
                TileType = 30;
            else if (Sort == Properties.TileColorCodes.Roof && (type == TL || type == TM || type == TR))
                TileType = 8;
            else if (Sort == Properties.TileColorCodes.Roof)
                TileType = 18;
            else if (Sort == Properties.TileColorCodes.Wall && type == BL)
                TileType = 36;
            else if (Sort == Properties.TileColorCodes.Wall && type == BR)
                TileType = 40;
            else if (Sort == Properties.TileColorCodes.Wall && type == BM)
                TileType = 38;
            else if (Sort == Properties.TileColorCodes.Wall && (type == M || type == TM))
                TileType = 27;
            else if (Sort == Properties.TileColorCodes.Wall && (type == L || type == TL))
                TileType = 19;
            else if (Sort == Properties.TileColorCodes.Wall && (type == R || type == TR))
                TileType = 29;
            else if (Sort == Properties.TileColorCodes.Forest && type == M)
                TileType = 52;
            else if (Sort == Properties.TileColorCodes.Forest && type == R)
                TileType = 53;
            else if (Sort == Properties.TileColorCodes.Forest && type == L)
                TileType = 51;
            else if (Sort == Properties.TileColorCodes.Forest && type == TM)
                TileType = 42;
            else if (Sort == Properties.TileColorCodes.Forest && type == TR)
                TileType = 43;
            else if (Sort == Properties.TileColorCodes.Forest && type == TL)
                TileType = 41;
            else if (Sort == Properties.TileColorCodes.Forest && type == BM)
                TileType = 62;
            else if (Sort == Properties.TileColorCodes.Forest && type == BR)
                TileType = 63;
            else if (Sort == Properties.TileColorCodes.Forest && type == BL)
                TileType = 61;
            else if (Sort == Properties.TileColorCodes.TallGrass && (type == L || type == TL || type == BL))
                TileType = 33;
            else if (Sort == Properties.TileColorCodes.TallGrass && (type == R || type == TR || type == BR))
                TileType = 35;
            else if (Sort == Properties.TileColorCodes.TallGrass)
                TileType = 34;

            if (objectColor == Properties.TileColorCodes.Window)
                ObjectType = 1;
            else if (objectColor == Properties.TileColorCodes.Door)
                ObjectType = 2;
        }

        public bool IsObstacle()
        {
            return this.Property == TileProperties.Obstacle;
        }

        public TileProperties GetProperty()
        {
            return Property;
        }

        public Rectangle GetTile()
        {
            int y = ((TileType - 1) / (TileMap.Width / Properties.TileHeight)) * Properties.TileHeight;
            int x = ((TileType - 1) % (TileMap.Width / Properties.TileWidth)) * Properties.TileWidth;
            return new Rectangle(x, y, x + Properties.TileWidth, y + Properties.TileHeight);
        }

        public Rectangle GetObject()
        {
            int y = ((ObjectType - 1) / (TileMap.Width / Properties.TileHeight)) * Properties.TileHeight;
            int x = ((ObjectType - 1) % (TileMap.Width / Properties.TileWidth)) * Properties.TileWidth;
            return new Rectangle(x, y, x + Properties.TileWidth, y + Properties.TileHeight);
        }
    }
}
