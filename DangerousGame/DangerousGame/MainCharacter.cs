using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace DangerousGame
{
    class MainCharacter : Sprite
    {
        private int Speed = 120;
        private Vector2 Direction = Vector2.Zero;
        private Vector2 Velocity = Vector2.Zero;

        const int MOVE_UP = -1;
        const int MOVE_DOWN = 1;
        const int MOVE_RIGHT = 1;
        const int MOVE_LEFT = -1;

        public enum PlayerStatus
        {
            Walking,
            Standing,
            Attacking
        };

        private PlayerStatus Status;

        private Map Map;

        private List<Keys> PressedKeys = new List<Keys>();
        private Keys LastPressedKey;

        public MainCharacter (Map map)
        {
            this.Map = map;
            Status = PlayerStatus.Standing;
            Position = new Vector2(2000, 500);
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            UpdateMovement(keyboardState, gameTime);


            base.Update(gameTime, Velocity, Direction);
        }
        private void UpdateMovement(KeyboardState currentKeyboardState, GameTime gameTime)
        {
            // Reseting speed and direction
            Velocity = Vector2.Zero;
            Direction = Vector2.Zero;
  
            // Left Arrow Key
            if (currentKeyboardState.IsKeyDown(Keys.Left))
            {
                if (!PressedKeys.Contains(Keys.Left))
                    PressedKeys.Add(Keys.Left);
            }
            else if (PressedKeys.Contains(Keys.Left))
            {
                PressedKeys.Remove(Keys.Left);
            }

            // Right Arrow Key
            if (currentKeyboardState.IsKeyDown(Keys.Right))
            {
                if (!PressedKeys.Contains(Keys.Right))
                    PressedKeys.Add(Keys.Right);
            }
            else if (PressedKeys.Contains(Keys.Right))
            {
                PressedKeys.Remove(Keys.Right);
            }

            // Up Arrow Key
            if (currentKeyboardState.IsKeyDown(Keys.Up))
            {
                if (!PressedKeys.Contains(Keys.Up))
                    PressedKeys.Add(Keys.Up);
            }
            else if (PressedKeys.Contains(Keys.Up))
            {
                PressedKeys.Remove(Keys.Up);
            }

            // Down Arrow Key
            if (currentKeyboardState.IsKeyDown(Keys.Down))
            {
                if (!PressedKeys.Contains(Keys.Down))
                    PressedKeys.Add(Keys.Down);
            }
            else if (PressedKeys.Contains(Keys.Down))
            {
                PressedKeys.Remove(Keys.Down);
            }

            // When there is pressed a key, get the last pressed key. And behave like only
            // that key is pressed
            if (PressedKeys.Count > 0)
                LastPressedKey = PressedKeys[PressedKeys.Count - 1];
            else
                LastPressedKey = 0;

            if (LastPressedKey == Keys.Left)
            {
                Velocity.X = Speed;
                Direction.X = MOVE_LEFT;
                Play("walkLeft");
            }
            else if (LastPressedKey == Keys.Right)
            {
                Velocity.X = Speed;
                Direction.X = MOVE_RIGHT;
                Play("walkRight");
            }
            else if (LastPressedKey == Keys.Up)
            {
                Velocity.Y = Speed;
                Direction.Y = MOVE_UP;
                Play("walkUp");
            }
            else if (LastPressedKey == Keys.Down)
            {
                Velocity.Y = Speed;
                Direction.Y = MOVE_DOWN;
                Play("walkDown");
            }

            // Let the animation stop when the player has no speed.
            if (Velocity.Equals(Vector2.Zero))
                Stop();


            Vector2 newPosition = Position + (Direction * Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);

            List<DrawableMonster> monsters = Pokening.Instance.GetWorldScreen().Monsters;
            Rectangle aBounds = Texture.Bounds;
            Vector2 aPos = newPosition;


            bool monsterInTheWay = false;
            foreach (DrawableMonster monster in monsters)
            {

                Rectangle bBounds = monster.Texture.Bounds;
                Vector2 bPos = monster.Position;

                if (aPos.X + aBounds.Width > bPos.X &&
                    aPos.X < bPos.X + bBounds.Width &&
                    aPos.Y + aBounds.Height > bPos.Y &&
                    aPos.Y < bPos.Y + bBounds.Height)
                {

                    monsterInTheWay = true;
                }

            }

            // Check or the character can walk to his new position. If not, Dont let him walk to there.
            
            if (!Map.MayWalk(newPosition) || monsterInTheWay)
            {
                Velocity = Vector2.Zero;
                Direction = Vector2.Zero;

                Status = PlayerStatus.Standing;
            }
            else
            {
                if (Map.AttackStarted(newPosition))
                {
                    Status = PlayerStatus.Attacking;
                }
                else
                {
                    Map.SetCenterPosition(newPosition);
                    Status = PlayerStatus.Walking;
                }
            }
        }

        public PlayerStatus GetStatus()
        {
            return Status;
        }

        public void setPosition(Vector2 value)
        {
            Position = value;
            Map.SetCenterPosition(value);
        }

    }
}
