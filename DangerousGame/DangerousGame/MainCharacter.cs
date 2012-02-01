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

        public MainCharacter (Map Map)
        {
            this.Map = Map;
        }

        public void Update(GameTime GameTime)
        {
            KeyboardState KeyboardState = Keyboard.GetState();
            UpdateMovement(KeyboardState, GameTime);
            PreviousKeyboardState = KeyboardState;
            base.Update(GameTime, Velocity, Direction);
        }
        private void UpdateMovement(KeyboardState CurrentKeyboardState, GameTime GameTime)
        {
            // Reseting speed and direction
            Velocity = Vector2.Zero;
            Direction = Vector2.Zero;
  
            // Left and Right arrow keys
            if (CurrentKeyboardState.IsKeyDown(Keys.Left))
            {
                Velocity.X = Speed;
                Direction.X = MOVE_LEFT;
                Play("walkLeft");
            }
            else if (CurrentKeyboardState.IsKeyDown(Keys.Right))
            {
                Velocity.X = Speed;
                Direction.X = MOVE_RIGHT;
                Play("walkRight");
            }
            
            if
                (CurrentKeyboardState.IsKeyDown(Keys.Up))
            {
                Velocity.Y = Speed;
                Direction.Y = MOVE_UP;
                Play("walkUp");
            }
            else if (CurrentKeyboardState.IsKeyDown(Keys.Down))
            {
                Velocity.Y = Speed;
                Direction.Y = MOVE_DOWN;
                Play("walkDown");
            }

            if (Velocity.Equals(Vector2.Zero))
                Stop();
            

            Vector2 NewPosition = Position + (Direction * Velocity * (float)GameTime.ElapsedGameTime.TotalSeconds);
            if (!Map.MayWalk(NewPosition))
            {
                Velocity = Vector2.Zero;
                Direction = Vector2.Zero;
            }
            else
            {
                Map.SetCenterPosition(NewPosition);
            }
                

        }

    }
}
