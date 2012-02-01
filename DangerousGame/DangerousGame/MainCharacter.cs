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

        KeyboardState PreviousKeyboardState;

        public MainCharacter (Map map)
        {
            this.Map = map;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState KeyboardState = Keyboard.GetState();
            UpdateMovement(KeyboardState, gameTime);
            PreviousKeyboardState = KeyboardState;
            base.Update(gameTime, Velocity, Direction);
        }
        private void UpdateMovement(KeyboardState currentKeyboardState, GameTime gameTime)
        {
            // Reseting speed and direction
            Velocity = Vector2.Zero;
            Direction = Vector2.Zero;
  
            // Left and Right arrow keys
            if (currentKeyboardState.IsKeyDown(Keys.Left))
            {
                Velocity.X = Speed;
                Direction.X = MOVE_LEFT;
                Play("walkLeft");
            }
            else if (currentKeyboardState.IsKeyDown(Keys.Right))
            {
                Velocity.X = Speed;
                Direction.X = MOVE_RIGHT;
                Play("walkRight");
            }
            
            if
                (currentKeyboardState.IsKeyDown(Keys.Up))
            {
                Velocity.Y = Speed;
                Direction.Y = MOVE_UP;
                Play("walkUp");
            }
            else if (currentKeyboardState.IsKeyDown(Keys.Down))
            {
                Velocity.Y = Speed;
                Direction.Y = MOVE_DOWN;
                Play("walkDown");
            }

            if (Velocity.Equals(Vector2.Zero))
                Stop();
            

            Vector2 newPosition = Position + (Direction * Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
            if (!Map.MayWalk(newPosition))
            {
                Velocity = Vector2.Zero;
                Direction = Vector2.Zero;
            }
            else
            {
                Map.SetCenterPosition(newPosition);
            }
                

        }

    }
}
