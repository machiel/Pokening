using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace DangerousGame
{
    class WorldScreen : Screen
    {

        Map Map;
        Map Objects;
        MainCharacter Player;

        List<DrawableMonster> Monsters = new List<DrawableMonster>();

        SpriteFont SpriteFont;

        public void Initialize(ContentManager contentManager)
        {
            Map = new Map();
            Objects = new Map();

            Player = new MainCharacter(Map);
            //Player.Position = MainCharacter.GetCenter();
            Player.setPosition(new Vector2(1774, 1030));
            //Console.Out.WriteLine("Player pos: " + Player.Position.X + ", Y: " + Player.Position.Y);
            Player.SetAnimation(Properties.MainCharacterWidth, Properties.MainCharacterHeight);
            Player.AddAnimation("walkDown", new int[] { 1, 2 }, 4);
            Player.AddAnimation("walkUp", new int[] { 4, 5 }, 4);
            Player.AddAnimation("walkLeft", new int[] { 7, 8 }, 5);
            Player.AddAnimation("walkRight", new int[] { 10, 11 }, 5);

            DrawableMonster monster = new DrawableMonster();
            monster.LoadContent(contentManager, "tree");
            monster.Position.X = 808;
            monster.Position.Y = 746;
            Monsters.Add(monster);
        }

        public void LoadContent(ContentManager contentManager)
        {
            Player.LoadContent(contentManager, "mainChar");
            Map.LoadTiles(contentManager, "tiles");
            Map.CreateMap(contentManager, "map-example");
            SpriteFont = contentManager.Load<SpriteFont>("Calibri");
        }

        public Pokening.Screens Update(GameTime gameTime)
        {
            KeyboardState KeyboardState = Keyboard.GetState();

            // Updating Pikachu
            Player.Update(gameTime);

            if (Player.GetStatus() == MainCharacter.PlayerStatus.Attacking)
            {
                return Pokening.Screens.FightingScreen;
            }
            else
            {
                return Pokening.Screens.WorldScreen;
            }

        }

        public void Draw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            Map.Draw(spriteBatch);
            Monsters[0].Draw(spriteBatch, Map.GetPosition());
            Player.Draw(spriteBatch);
            spriteBatch.DrawString(SpriteFont, "X: " + Player.Position.X + " Y: " + Player.Position.Y, new Vector2(649, 29), Color.Black);
            spriteBatch.DrawString(SpriteFont, "X: " + Player.Position.X + " Y: " + Player.Position.Y, new Vector2(651, 29), Color.Black);
            spriteBatch.DrawString(SpriteFont, "X: " + Player.Position.X + " Y: " + Player.Position.Y, new Vector2(651, 31), Color.Black);
            spriteBatch.DrawString(SpriteFont, "X: " + Player.Position.X + " Y: " + Player.Position.Y, new Vector2(649, 31), Color.Black);
            spriteBatch.DrawString(SpriteFont, "X: " + Player.Position.X + " Y: " + Player.Position.Y, new Vector2(650, 30), Color.White);
            
        }
    }
}
