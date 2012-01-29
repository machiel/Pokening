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

        private int speed = 120;
        private Vector2 Direction = Vector2.Zero;
        private Vector2 Speed = Vector2.Zero;

        const int MOVE_UP = -1;
        const int MOVE_DOWN = 1;
        const int MOVE_RIGHT = 1;
        const int MOVE_LEFT = -1;

        KeyboardState PreviousKeyboardState;

        public void Update(GameTime gameTime)
        {
            KeyboardState KeyboardState = Keyboard.GetState();
            UpdateMovement(KeyboardState);
            PreviousKeyboardState = KeyboardState;
            base.Update(gameTime, Speed, Direction);
        }

        private void UpdateMovement(KeyboardState aCurrentKeyboardState)
        {
            // Reseting speed and direction
            Speed = Vector2.Zero;
            Direction = Vector2.Zero;

            // Left and Right arrow keys
            if (aCurrentKeyboardState.IsKeyDown(Keys.Left))
            {
                Speed.X = speed;
                Direction.X = MOVE_LEFT;
            }
            else if (aCurrentKeyboardState.IsKeyDown(Keys.Right))
            {
                Speed.X = speed;
                Direction.X = MOVE_RIGHT;
            }
            
            // Up and Down arrow keys
            if (aCurrentKeyboardState.IsKeyDown(Keys.Up))
            {
                Speed.Y = speed;
                Direction.Y = MOVE_UP;
            }
            else if(aCurrentKeyboardState.IsKeyDown(Keys.Down))
            {
                Speed.Y = speed;
                Direction.Y = MOVE_DOWN;
            }
            
        }

    }
}
