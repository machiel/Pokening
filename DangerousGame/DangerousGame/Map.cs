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
        public List<List<Tile>> Tiles = new List<List<Tile>>();

        private Vector2 Position = Vector2.Zero;

        // All the tile images
        private Texture2D TileMap;
        private Texture2D MapImage;

        const int GrassTile = 0;
        const int DirtTile = 1;

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
            MapImage = contentManager.Load<Texture2D>(theAsset);
            Texture2D obstacleMap = contentManager.Load<Texture2D>(theAsset + "-obstacle");
            Color[] textureData = new Color[MapImage.Width * MapImage.Height];
            Color[] obstacleTextureData = new Color[MapImage.Width * MapImage.Height];
            MapImage.GetData(textureData);
            obstacleMap.GetData(obstacleTextureData);

            // Check every point within the intersection bounds
            for (int x = 0; x < MapImage.Width; x++)
            {
                Tiles.Add(new List<Tile>());
                for (int y = 0; y < MapImage.Height; y++)
                {

                    bool isObstacle = false;

                    // Get the color of both pixels at this point
                    Color color = textureData[(x) + (y * MapImage.Width)];
                    Color obstacleColor = obstacleTextureData[(x) + (y * MapImage.Width)]; 
                    string colorString = color.R.ToString() + color.G.ToString() + color.B.ToString();
                    string obstacleColorString = obstacleColor.R.ToString() + obstacleColor.G.ToString() + obstacleColor.B.ToString();
                    
                    // If the color of this tile in the obstacle map is black
                    // then this tile is an obstacle
                    if (obstacleColorString == "000")
                    {
                        isObstacle = true;
                    }

                    Color tileTL = GetTileColor(textureData, MapImage, (x - 1), (y - 1));
                    Color tileTM = GetTileColor(textureData, MapImage, x, (y - 1));
                    Color tileTR = GetTileColor(textureData, MapImage, (x + 1), (y - 1));
                    Color tileL = GetTileColor(textureData, MapImage, (x - 1), y);
                    Color tileR = GetTileColor(textureData, MapImage, (x + 1), y);
                    Color tileBL = GetTileColor(textureData, MapImage, (x - 1), (y + 1));
                    Color tileBM = GetTileColor(textureData, MapImage, x, (y + 1));
                    Color tileBR = GetTileColor(textureData, MapImage, (x + 1), (y + 1));

                    int Type;
                    if (color != tileL && color != tileTM)
                        Type = Tile.TL;
                    else if (color == tileL && color == tileR && color != tileTM)
                        Type = Tile.TM;
                    else if (color != tileR && color != tileTM)
                        Type = Tile.TR;
                    else if (color != tileL && color == tileTM && color == tileBM)
                        Type = Tile.L;
                    else if (color != tileR && color == tileTM && color == tileBM)
                        Type = Tile.R;
                    else if (color != tileL && color != tileBM)
                        Type = Tile.BL;
                    else if (color == tileL && color == tileR && color != tileBM)
                        Type = Tile.BM;
                    else if (color != tileR && color != tileBM)
                        Type = Tile.BR;
                    else
                        Type = Tile.M;

                    Tiles[x].Add(new Tile(colorString, Type, TileMap, isObstacle));
                }
            }
        }

        private Color GetTileColor(Color[] textureData, Texture2D mapImage, int x, int y)
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

            Vector2 inversedPosition = Position * -1;

            // For each tile drawing the texture corresponding with that tile
            for(int x = 0; x < Tiles.Capacity; x++)
            {
                int newX = x * 50;
                for (int Y = 0; Y < Tiles[x].Count; Y++)
                {
                    Tile tile = Tiles[x][Y];
                    Rectangle tileBoundaries = tile.GetTile();

                    int newY = Y * 50;

                    Vector2 drawLocation = new Vector2(newX, newY) + inversedPosition;

                    spriteBatch.Draw(TileMap, drawLocation, tileBoundaries, Color.White, 0.0f, Vector2.Zero, 1, SpriteEffects.None, 0);
                }
            }
        }

        public void SetCenterPosition(Vector2 centerPoint)
        {
            Position.X = centerPoint.X - 400;
            Position.Y = centerPoint.Y - 300;
        }

        /// <summary>
        /// Checking or the given sprite can walk to the 'newPosition' he wants to go
        /// </summary>
        /// <param name="newPosition">His new position</param>
        public bool MayWalk(Vector2 newPosition)
        {
            Vector2 CenterPoint = MainCharacter.GetCenter();

            int x = (int)(newPosition.X) / 50;
            int y = (int)(newPosition.Y) / 50;

            return !Tiles[x][y].IsObstacle();

        }
    }
}
