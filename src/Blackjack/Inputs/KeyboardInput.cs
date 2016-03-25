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

            Deal = currentKeyboardState.IsKeyDown(Keys.Space) && previousKeyboardState.IsKeyUp(Keys.Space);
        }
    }
}