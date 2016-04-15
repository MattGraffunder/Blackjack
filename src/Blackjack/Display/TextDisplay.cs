using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blackjack
{
    class TextDisplay
    {
        SpriteFont _font;

        public void Initialize(SpriteFont font)
        {
            _font = font;
        }

        public void DrawString(SpriteBatch spriteBatch, string text, Vector2 position)
        {
            DrawString(spriteBatch, text, position, Color.Black);
        }

        public void DrawString(SpriteBatch spriteBatch, string text, Vector2 position, Color color)
        {
            spriteBatch.DrawString(_font, text, position, color);
        }
    }
}