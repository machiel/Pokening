using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Collections;

namespace DangerousGame
{

    class Map
    {
        // Array with tiles, what kind of tile on what position
        public List<List<Tile>> tiles = new List<List<Tile>>();

        private Vector2 position = Vector2.Zero;

        // All the tile images
        private Texture2D TileMap;
        private Texture2D mapImage;

        const int GRASSTILE = 0;
        const int DIRTTILE = 1;

        /// <summary>
        /// Adds new content to the TileType array
        /// </summary>
        public void LoadTiles(ContentManager contentManager, string theAsset)
        {
            TileMap = contentManager.Load<Texture2D>(theAsset);
            Color[] textureData = new Color[TileMap.Width * TileMap.Height];
            TileMap.GetData(textureData);
        }

        /// <summary>
        /// Adds new content to the TileType array
        /// </summary>
        public void CreateMap(ContentManager contentManager, string theAsset)
        {
            mapImage = contentManager.Load<Texture2D>(theAsset);
            Color[] textureData = new Color[mapImage.Width * mapImage.Height];
            mapImage.GetData(textureData);

            // Check every point within the intersection bounds
            for (int x = 0; x < mapImage.Width; x++)
            {
                tiles.Add(new List<Tile>());
                for (int y = 0; y < mapImage.Height; y++)
                {
                    // Get the color of both pixels at this point
                    Color color = textureData[(x) + (y * mapImage.Width)];
                    string colorString = color.R.ToString() + color.G.ToString() + color.B.ToString();

                    Color tileTL = getTileColor(textureData, mapImage, (x - 1), (y - 1));
                    Color tileTM = getTileColor(textureData, mapImage, x, (y - 1));
                    Color tileTR = getTileColor(textureData, mapImage, (x + 1), (y - 1));
                    Color tileL = getTileColor(textureData, mapImage, (x - 1), y);
                    Color tileR = getTileColor(textureData, mapImage, (x + 1), y);
                    Color tileBL = getTileColor(textureData, mapImage, (x - 1), (y + 1));
                    Color tileBM = getTileColor(textureData, mapImage, x, (y + 1));
                    Color tileBR = getTileColor(textureData, mapImage, (x + 1), (y + 1));

                    int type;
                    if (color != tileL && color != tileTM)
                        type = Tile.TL;
                    else if (color == tileL && color == tileR && color != tileTM)
                        type = Tile.TM;
                    else if (color != tileR && color != tileTM)
                        type = Tile.TR;
                    else if (color != tileL && color == tileTM && color == tileBM)
                        type = Tile.L;
                    else if (color != tileR && color == tileTM && color == tileBM)
                        type = Tile.R;
                    else if (color != tileL && color != tileBM)
                        type = Tile.BL;
                    else if (color == tileL && color == tileR && color != tileBM)
                        type = Tile.BM;
                    else if (color != tileR && color != tileBM)
                        type = Tile.BR;
                    else
                        type = Tile.M;

                    tiles[x].Add(new Tile(colorString, type, TileMap));
                }
            }
        }

        private Color getTileColor(Color[] textureData, Texture2D mapImage, int x, int y)
        {
            Color color;
            if (x >= 0 && y >= 0 && x + (y * mapImage.Width) < textureData.Length)
                color = textureData[(x + (y * mapImage.Width))];
            else
                color = new Color();
            return color;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // For each tile drawing the texture corresponding with that tile
            for(int x = 0; x < tiles.Capacity; x++)
            {
                for (int y = 0; y < tiles[x].Count; y++)
                {
                    Rectangle tile = tiles[x][y].getTile();
                    spriteBatch.Draw(TileMap, new Vector2(x * 50 , y * 50) + (position * -1), tile, Color.White, 0.0f, Vector2.Zero, 1, SpriteEffects.None, 0);
                }
            }
        }

        public void setCenterPosition(Vector2 centerPoint)
        {
            position.X = centerPoint.X - 400;
            position.Y = centerPoint.Y - 300;
        }

        /// <summary>
        /// Checking or the given sprite can walk to the 'newPosition' he wants to go
        /// </summary>
        /// <param name="obj1">The sprite</param>
        /// <param name="newPosition">His new position</param>
        public bool MayWalk(Sprite obj1, Vector2 newPosition)
        {
            int x = (int)newPosition.X;
            int y = (int)newPosition.Y + obj1.Size.Height;
            int width = obj1.Size.Width;

            // Since an Sprite can be at two x-tiles the same time, check those two
            int xTile1 = x / 50; // First x-tile, the most left one
            int xTile2 = (x + width) / 50; // Second x-tile, the most right one
            int yTile = y / 50;

           // if (
                //xTile1 == tiles.Length || xTile2 == tiles.Length || yTile == tiles[0].Length // Map boundaries, right and bottom
               // || xTile1 < 0 || xTile2 < 0 || yTile < 0 // Map boundaries, left and top (TODO not working really good, can still walk 1 tile outside the map)
              //  || tiles[xTile1][yTile] == DIRTTILE || this.tiles[xTile2][yTile] == DIRTTILE) // Check if object is on 'dirt' tile
               // return false;
            return true;
        }
    }
}
