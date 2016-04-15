using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blackjack.Inputs
{
    class KeyboardInput:Input
    {
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        public override void Update()
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            Exit = currentKeyboardState.IsKeyDown(Keys.Escape);

            Deal = WasKeyPressed(Keys.Space);

            Hit = WasKeyPressed(Keys.Z);

            Split = WasKeyPressed(Keys.X);

            DoubleDown = WasKeyPressed(Keys.C);

            Stay = WasKeyPressed(Keys.V);
        }

        private bool WasKeyPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyUp(key);
        }
    }
}