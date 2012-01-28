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
        const int MOVE_UP = -1;
        const int MOVE_DOWN = 1;
        const int MOVE_RIGHT = 1;
        const int MOVE_LEFT = -1;

        Vector2 mDirection = Vector2.Zero;
        Vector2 mSpeed = Vector2.Zero;

        KeyboardState mPreviousKeyboardState;

        public void Update(GameTime gameTime)
        {
            KeyboardState aKeyboardState = Keyboard.GetState();
            UpdateMovement(aKeyboardState);
            mPreviousKeyboardState = aKeyboardState;
            base.Update(gameTime, mSpeed, mDirection);
        }

        private void UpdateMovement(KeyboardState aCurrentKeyboardState)
        {
            mSpeed = Vector2.Zero;
            mDirection = Vector2.Zero;
            if (aCurrentKeyboardState.IsKeyDown(Keys.Add))
            {
                speed += 10;
            } else if (aCurrentKeyboardState.IsKeyDown(Keys.Subtract)) {
                speed -= 10;
            }

            if (aCurrentKeyboardState.IsKeyDown(Keys.Left))
            {
                mSpeed.X = speed;
                mDirection.X = MOVE_LEFT;
            } else if (aCurrentKeyboardState.IsKeyDown(Keys.Right))
            {
                mSpeed.X = speed;
                mDirection.X = MOVE_RIGHT;
            }
            
            if (aCurrentKeyboardState.IsKeyDown(Keys.Up))
            {
                mSpeed.Y = speed;
                mDirection.Y = MOVE_UP;
            } else if(aCurrentKeyboardState.IsKeyDown(Keys.Down)) {
                mSpeed.Y = speed;
                mDirection.Y = MOVE_DOWN;
            }
            
        }

    }
}
