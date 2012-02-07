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

        MainCharacter Player;
        Map Map;
        Map Objects;

        public void Initialize(ContentManager contentManager)
        {
            Map = new Map();
            Objects = new Map();

            Player = new MainCharacter(Map);
            //Player.Position = MainCharacter.GetCenter();
            Player.setPosition(new Vector2(Properties.WindowWidth, Properties.WindowHeight));
            //Console.Out.WriteLine("Player pos: " + Player.Position.X + ", Y: " + Player.Position.Y);
            Player.SetAnimation(Properties.MainCharacterWidth, Properties.MainCharacterHeight);
            Player.AddAnimation("walkDown", new int[] { 1, 2 }, 4);
            Player.AddAnimation("walkUp", new int[] { 4, 5 }, 4);
            Player.AddAnimation("walkLeft", new int[] { 7, 8 }, 5);
            Player.AddAnimation("walkRight", new int[] { 10, 11 }, 5);
        }

        public void LoadContent(ContentManager contentManager)
        {
            Player.LoadContent(contentManager, "mainChar");
            Map.LoadTiles(contentManager, "tiles");
            Map.CreateMap(contentManager, "map-example");
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
            Player.Draw(spriteBatch);
        }
    }
}
