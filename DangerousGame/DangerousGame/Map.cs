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

        private Texture2D ObjectsImage;

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
            Texture2D objectsMap = contentManager.Load<Texture2D>(theAsset + "-objectsMap");
            ObjectsImage = contentManager.Load<Texture2D>(theAsset + "-objects");

            Color[] textureData = new Color[MapImage.Width * MapImage.Height];
            Color[] obstacleTextureData = new Color[MapImage.Width * MapImage.Height];
            Color[] objectsTextureData = new Color[MapImage.Width * MapImage.Height];

            MapImage.GetData(textureData);
            obstacleMap.GetData(obstacleTextureData);
            objectsMap.GetData(objectsTextureData);

            // Check every point within the intersection bounds
            for (int x = 0; x < MapImage.Width; x++)
            {
                Tiles.Add(new List<Tile>());
                for (int y = 0; y < MapImage.Height; y++)
                {

                    Tile.TileProperties tileProperty;

                    // Get the color of both pixels at this point
                    Color color = textureData[(x) + (y * MapImage.Width)];
                    Color obstacleColor = obstacleTextureData[(x) + (y * MapImage.Width)];
                    Color objectsColor = objectsTextureData[(x) + (y * MapImage.Width)];

                    string colorString = color.R.ToString() + color.G.ToString() + color.B.ToString();
                    string obstacleColorString = obstacleColor.R.ToString() + obstacleColor.G.ToString() + obstacleColor.B.ToString();
                    string objectsColorString = objectsColor.R.ToString() + objectsColor.G.ToString() + objectsColor.B.ToString();

                    // If the color of this tile in the obstacle map is black
                    // then this tile is an obstacle
                    if (obstacleColorString == Properties.TileColorCodes.Obstacle)
                        tileProperty = Tile.TileProperties.Obstacle;
                    else if (obstacleColorString == Properties.TileColorCodes.Aggressive)
                        tileProperty = Tile.TileProperties.Aggressive;
                    else
                        tileProperty = Tile.TileProperties.Normal;

                    Color tileTL = GetTileColor(textureData, MapImage, (x - 1), (y - 1));
                    Color tileTM = GetTileColor(textureData, MapImage, x, (y - 1));
                    Color tileTR = GetTileColor(textureData, MapImage, (x + 1), (y - 1));
                    Color tileL = GetTileColor(textureData, MapImage, (x - 1), y);
                    Color tileR = GetTileColor(textureData, MapImage, (x + 1), y);
                    Color tileBL = GetTileColor(textureData, MapImage, (x - 1), (y + 1));
                    Color tileBM = GetTileColor(textureData, MapImage, x, (y + 1));
                    Color tileBR = GetTileColor(textureData, MapImage, (x + 1), (y + 1));

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

                    Tiles[x].Add(new Tile(colorString, type, TileMap, tileProperty, objectsColorString));
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
            for (int x = 0; x < Tiles.Count; x++)
            {
                // Normal X position + the offset of the background
                int newX = x * Properties.TileWidth;

                for (int Y = 0; Y < Tiles[x].Count; Y++)
                {
                    Tile tile = Tiles[x][Y];

                    // Getting the position of the tile on the tile images sprite
                    Rectangle tileBoundaries = tile.GetTile();

                    // Getting the position of the tile on the objects images sprite
                    Rectangle objectBoundaries = tile.GetObject();

                    // Normal Y position + the offset of the background
                    int newY = Y * Properties.TileHeight;

                    // The position where the objects and underground should be drawn
                    Vector2 drawLocation = new Vector2(newX, newY) + inversedPosition;

                    // Drawing the underground tile for this map
                    spriteBatch.Draw(TileMap, drawLocation, new Rectangle(0, 100, 50, 150), Color.White, 0.0f, Vector2.Zero, 1, SpriteEffects.None, 0);

                    // Drawing the underground tile image on this location
                    if (tile.Sort != Properties.TileColorCodes.Grass)
                        spriteBatch.Draw(TileMap, drawLocation, tileBoundaries, Color.White, 0.0f, Vector2.Zero, 1, SpriteEffects.None, 0);

                    // Drawing the object image on this location
                    spriteBatch.Draw(ObjectsImage, drawLocation, objectBoundaries, Color.White, 0.0f, Vector2.Zero, 1, SpriteEffects.None, 0);
                }
            }
        }

        public void SetCenterPosition(Vector2 centerPoint)
        {
            Position.X = centerPoint.X - (Properties.WindowWidth / 2);
            Position.Y = centerPoint.Y - (Properties.WindowHeight / 2);
        }

        private Tile GetTileFromPosition(Vector2 pos)
        {
            int x = (int)(pos.X) / Properties.TileWidth;
            int y = (int)(pos.Y) / Properties.TileHeight;
            return Tiles[x][y];
        }

        /// <summary>
        /// Checking or the given sprite can walk to the 'newPosition' he wants to go
        /// </summary>
        /// <param name="newPosition">His new position</param>
        public bool MayWalk(Vector2 newPosition)
        {

            Tile tile = GetTileFromPosition(newPosition);
            return !tile.IsObstacle();

        }

        public bool AttackStarted(Vector2 newPosition)
        {
            Tile tile = GetTileFromPosition(newPosition);

            if (tile.GetProperty() == Tile.TileProperties.Aggressive)
            {
                Random random = new Random();
                int chance = random.Next(0, 100);
                return chance == 23;
            }

            return false;
        }
    }
}
