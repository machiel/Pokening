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

        const int GRASSTILE = 0;
        const int DIRTTILE = 1;

        /// <summary>
        /// Adds new content to the TileType array
        /// </summary>
        public void LoadTiles(ContentManager ContentManager, string TheAsset)
        {
            TileMap = ContentManager.Load<Texture2D>(TheAsset);
            Color[] TextureData = new Color[TileMap.Width * TileMap.Height];
            TileMap.GetData(TextureData);
        }

        /// <summary>
        /// Adds new content to the TileType array
        /// </summary>
        public void CreateMap(ContentManager ContentManager, string TheAsset)
        {
            MapImage = ContentManager.Load<Texture2D>(TheAsset);
            Texture2D ObstacleMap = ContentManager.Load<Texture2D>(TheAsset + "-obstacle");
            Color[] TextureData = new Color[MapImage.Width * MapImage.Height];
            Color[] ObstacleTextureData = new Color[MapImage.Width * MapImage.Height];
            MapImage.GetData(TextureData);
            ObstacleMap.GetData(ObstacleTextureData);

            // Check every point within the intersection bounds
            for (int x = 0; x < MapImage.Width; x++)
            {
                Tiles.Add(new List<Tile>());
                for (int y = 0; y < MapImage.Height; y++)
                {

                    bool IsObstacle = false;

                    // Get the color of both pixels at this point
                    Color Color = TextureData[(x) + (y * MapImage.Width)];
                    Color ObstacleColor = ObstacleTextureData[(x) + (y * MapImage.Width)]; 
                    string ColorString = Color.R.ToString() + Color.G.ToString() + Color.B.ToString();
                    string obstacleColorString = ObstacleColor.R.ToString() + ObstacleColor.G.ToString() + ObstacleColor.B.ToString();
                    
                    // If the color of this tile in the obstacle map is black
                    // then this tile is an obstacle
                    if (obstacleColorString == "000")
                    {
                        IsObstacle = true;
                    }

                    Color tileTL = GetTileColor(TextureData, MapImage, (x - 1), (y - 1));
                    Color tileTM = GetTileColor(TextureData, MapImage, x, (y - 1));
                    Color tileTR = GetTileColor(TextureData, MapImage, (x + 1), (y - 1));
                    Color tileL = GetTileColor(TextureData, MapImage, (x - 1), y);
                    Color tileR = GetTileColor(TextureData, MapImage, (x + 1), y);
                    Color tileBL = GetTileColor(TextureData, MapImage, (x - 1), (y + 1));
                    Color tileBM = GetTileColor(TextureData, MapImage, x, (y + 1));
                    Color tileBR = GetTileColor(TextureData, MapImage, (x + 1), (y + 1));

                    int Type;
                    if (Color != tileL && Color != tileTM)
                        Type = Tile.TL;
                    else if (Color == tileL && Color == tileR && Color != tileTM)
                        Type = Tile.TM;
                    else if (Color != tileR && Color != tileTM)
                        Type = Tile.TR;
                    else if (Color != tileL && Color == tileTM && Color == tileBM)
                        Type = Tile.L;
                    else if (Color != tileR && Color == tileTM && Color == tileBM)
                        Type = Tile.R;
                    else if (Color != tileL && Color != tileBM)
                        Type = Tile.BL;
                    else if (Color == tileL && Color == tileR && Color != tileBM)
                        Type = Tile.BM;
                    else if (Color != tileR && Color != tileBM)
                        Type = Tile.BR;
                    else
                        Type = Tile.M;

                    Tiles[x].Add(new Tile(ColorString, Type, TileMap, IsObstacle));
                }
            }
        }

        private Color GetTileColor(Color[] TextureData, Texture2D MapImage, int X, int Y)
        {
            Color Color;
            if (X >= 0 && Y >= 0 && X + (Y * MapImage.Width) < TextureData.Length)
                Color = TextureData[(X + (Y * MapImage.Width))];
            else
                Color = new Color();
            return Color;
        }

        public void Draw(SpriteBatch SpriteBatch)
        {

            Vector2 InversedPosition = Position * -1;

            // For each tile drawing the texture corresponding with that tile
            for(int X = 0; X < Tiles.Capacity; X++)
            {
                int NewX = X * 50;
                for (int Y = 0; Y < Tiles[X].Count; Y++)
                {
                    Tile Tile = Tiles[X][Y];
                    Rectangle TileBoundaries = Tile.GetTile();

                    int NewY = Y * 50;

                    Vector2 DrawLocation = new Vector2(NewX, NewY) + InversedPosition;

                    SpriteBatch.Draw(TileMap, DrawLocation, TileBoundaries, Color.White, 0.0f, Vector2.Zero, 1, SpriteEffects.None, 0);
                }
            }
        }

        public void SetCenterPosition(Vector2 CenterPoint)
        {
            Position.X = CenterPoint.X - 400;
            Position.Y = CenterPoint.Y - 300;
        }

        /// <summary>
        /// Checking or the given sprite can walk to the 'newPosition' he wants to go
        /// </summary>
        /// <param name="NewPosition">His new position</param>
        public bool MayWalk(Vector2 NewPosition)
        {
            Vector2 CenterPoint = MainCharacter.GetCenter();

            int x = (int)(NewPosition.X) / 50;
            int y = (int)(NewPosition.Y) / 50;

            return !Tiles[x][y].IsObstacle();

        }
    }
}
