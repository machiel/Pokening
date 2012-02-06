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

        private Map Map;

        private List<Keys> pressedKeys = new List<Keys>();
        private Keys lastPressedKey;

        public MainCharacter (Map map)
        {
            this.Map = map;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState KeyboardState = Keyboard.GetState();
            UpdateMovement(KeyboardState, gameTime);
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
                if (!pressedKeys.Contains(Keys.Left))
                    pressedKeys.Add(Keys.Left);
            }
            else if (pressedKeys.Contains(Keys.Left))
            {
                pressedKeys.Remove(Keys.Left);
            }

            // Right Arrow Key
            if (currentKeyboardState.IsKeyDown(Keys.Right))
            {
                if (!pressedKeys.Contains(Keys.Right))
                    pressedKeys.Add(Keys.Right);
            }
            else if (pressedKeys.Contains(Keys.Right))
            {
                pressedKeys.Remove(Keys.Right);
            }

            // Up Arrow Key
            if (currentKeyboardState.IsKeyDown(Keys.Up))
            {
                if (!pressedKeys.Contains(Keys.Up))
                    pressedKeys.Add(Keys.Up);
            }
            else if (pressedKeys.Contains(Keys.Up))
            {
                pressedKeys.Remove(Keys.Up);
            }

            // Down Arrow Key
            if (currentKeyboardState.IsKeyDown(Keys.Down))
            {
                if (!pressedKeys.Contains(Keys.Down))
                    pressedKeys.Add(Keys.Down);
            }
            else if (pressedKeys.Contains(Keys.Down))
            {
                pressedKeys.Remove(Keys.Down);
            }

            if (pressedKeys.Count > 0)
                lastPressedKey = pressedKeys[pressedKeys.Count - 1];
            else
                lastPressedKey = 0;

            //foreach (Keys key in pressedKeys)
            //    Console.Out.WriteLine(key);

            Console.Out.WriteLine(pressedKeys.Count);

            if (lastPressedKey == Keys.Left)
            {
                Velocity.X = Speed;
                Direction.X = MOVE_LEFT;
                Play("walkLeft");
            }
            else if (lastPressedKey == Keys.Right)
            {
                Velocity.X = Speed;
                Direction.X = MOVE_RIGHT;
                Play("walkRight");
            }
            else if (lastPressedKey == Keys.Up)
            {
                Velocity.Y = Speed;
                Direction.Y = MOVE_UP;
                Play("walkUp");
            }
            else if (lastPressedKey == Keys.Down)
            {
                Velocity.Y = Speed;
                Direction.Y = MOVE_DOWN;
                Play("walkDown");
            }

            // Let the animation stop when the player has no speed.
            if (Velocity.Equals(Vector2.Zero))
                Stop();

            // Check or the character can walk to his new position. If not, Dont let him walk to there.
            Vector2 newPosition = Position + (Direction * Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
            if (!Map.MayWalk(newPosition))
            {
                Velocity = Vector2.Zero;
                Direction = Vector2.Zero;
            }
            else
                Map.SetCenterPosition(newPosition);
        }

        public void setPosition(Vector2 value)
        {
            Position = value;
            Map.SetCenterPosition(value);
        }

    }
}
