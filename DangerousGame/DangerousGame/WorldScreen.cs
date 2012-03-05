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
    public class WorldScreen : Screen
    {

        Map Map;
        Map Objects;
        MainCharacter Player;

        public List<DrawableMonster> Monsters = new List<DrawableMonster>();
        DrawableFollowMonster Follow = new DrawableFollowMonster();

        FightBar FightBar = new FightBar();

        SpriteFont SpriteFont;

        public void Initialize(ContentManager contentManager)
        {
            Map = new Map();
            Objects = new Map();
            Player = new MainCharacter(Map);
            //Player.Position = MainCharacter.GetCenter();
            Player.setPosition(new Vector2(580, 528));
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

            Texture2D fireball = contentManager.Load<Texture2D>("fireball");

            Follow.LoadContent(contentManager, "fire");
            Follow.AddTexture(fireball);
            Follow.Position.X = 580;
            Follow.Position.Y = 578;
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

            Follow.Position = Player.Position - new Vector2(20, 60);
            Follow.UpdateFollow(gameTime);

            List<DrawableAttack> attacks = Follow.GetAttacks();


            for (int i = 0; i < Monsters.Count; i++)
            {
                DrawableMonster monster = Monsters[i];

                if (monster.Health <= 0)
                {
                    Monsters.Remove(monster);
                }
                else
                {
                    Vector2 pos = monster.Position;
                    pos = pos - Map.GetPosition();

                    // Monster is visible
                    if (pos.X >= 0 && pos.X <= Properties.WindowWidth
                        && pos.Y >= 0 && pos.Y <= Properties.WindowHeight)
                    {
                        for (int a = 0; a < attacks.Count; a++)
                        {
                            DrawableAttack attack = attacks[a];
                            Vector2 aPos = attack.Position - Map.GetPosition();

                            if (aPos.X >= 0 && aPos.X <= Properties.WindowWidth
                                && aPos.Y >= 0 && aPos.Y <= Properties.WindowHeight)
                            {
                                attack.Update(gameTime);
                            }
                            else
                            {
                                attacks.Remove(attack);
                            }

                        }
                    }
                }
            }

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

            for (int i = 0; i < Monsters.Count; i++)
                Monsters[i].Draw(spriteBatch, Map.GetPosition());


            Follow.DrawFollow(spriteBatch, Map.GetPosition());
            Player.Draw(spriteBatch);

            spriteBatch.DrawString(SpriteFont, "X: " + Player.Position.X + " Y: " + Player.Position.Y, new Vector2(649, 29), Color.Black);
            spriteBatch.DrawString(SpriteFont, "X: " + Player.Position.X + " Y: " + Player.Position.Y, new Vector2(651, 29), Color.Black);
            spriteBatch.DrawString(SpriteFont, "X: " + Player.Position.X + " Y: " + Player.Position.Y, new Vector2(651, 31), Color.Black);
            spriteBatch.DrawString(SpriteFont, "X: " + Player.Position.X + " Y: " + Player.Position.Y, new Vector2(649, 31), Color.Black);
            spriteBatch.DrawString(SpriteFont, "X: " + Player.Position.X + " Y: " + Player.Position.Y, new Vector2(650, 30), Color.White);

            FightBar.Draw(graphics, spriteBatch, SpriteFont);
        }
    }
}
