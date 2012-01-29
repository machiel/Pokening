using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace DangerousGame
{

    class Map
    {
        // Array with tiles, what kind of tile on what position
        public int[][] tiles;

        // All the tile images
        private Texture2D[] TileTypes = new Texture2D[2];

        const int GRASSTILE = 0;
        const int DIRTTILE = 1;

        public Map(int[][] tiles)
        {
            this.tiles = tiles;
        }

        public void LoadContent(ContentManager contentManager, string theAsset, int index)
        {
            TileTypes[index] = contentManager.Load<Texture2D>(theAsset);
            Color[] textureData = new Color[TileTypes[index].Width * TileTypes[index].Height];
            TileTypes[index].GetData(textureData);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for(int x = 0; x < tiles.Length; x++)
            {
                for (int y = 0; y < tiles[x].Length; y++)
                {
                    Texture2D texture = getTileTexture(tiles[x][y]);
                    spriteBatch.Draw(texture, new Vector2(x * 50, y * 50), new Rectangle(0, 0, texture.Width, texture.Height), Color.White, 0.0f, Vector2.Zero, 1, SpriteEffects.None, 0);
                }
            }
        }

        private Texture2D getTileTexture(int value)
        {
            return TileTypes[value];
        }

        public bool MayWalk(Sprite obj1, Vector2 newPosition)
        {
            int x = (int)newPosition.X;
            int y = (int)newPosition.Y + obj1.Size.Height;
            int width = obj1.Size.Width;

            // Since an Sprite can be at two x-tiles the same time, check those two
            int xTile1 = x / 50; // First x-tile, the most left one
            int xTile2 = (x + width) / 50; // Second x-tile, the most right one
            int yTile = y / 50;

            if (
                xTile1 == tiles.Length || xTile2 == tiles.Length || yTile == tiles[0].Length // Map boundaries, right and bottom
                || xTile1 < 0 || xTile2 < 0 || yTile < 0 // Map boundaries, left and top (TODO not working really good, can still walk 1 tile outside the map)
                || tiles[xTile1][yTile] == DIRTTILE || this.tiles[xTile2][yTile] == DIRTTILE) // Check if object is on 'dirt' tile
                return false;
            return true;
        }
    }
}
