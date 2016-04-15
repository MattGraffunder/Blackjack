using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blackjack
{
    class ChipDisplay
    {
        const int CHIP_HEIGHT = 132;
        const int CHIP_WIDTH = 132;

        const int CHIP_START_X = 302;
        const int CHIP_START_Y = 274;

        const float SCALE = 1f; // Multiplier: 0 to 1 

        private Texture2D _chipSpriteSheet;

        private TextDisplay _textDisplay;

        public void Initialize(Texture2D chipSpriteSheet, TextDisplay textDisplay)
        {
            _textDisplay = textDisplay;
            _chipSpriteSheet = chipSpriteSheet;                        
        }

        public void DrawChip(SpriteBatch spriteBatch, int xPosition, int yPosition)
        {
            //Figure out how big the cards are, and how much to scale them
            spriteBatch.Draw(_chipSpriteSheet,
                new Rectangle(xPosition, yPosition, (int)(CHIP_WIDTH * SCALE), (int)(CHIP_HEIGHT * SCALE)),
                new Rectangle(CHIP_START_X, CHIP_START_Y, CHIP_WIDTH, CHIP_HEIGHT), Color.White);
        }

        public void DrawChip(SpriteBatch spriteBatch, int xPosition, int yPosition, int amount)
        {
            DrawChip(spriteBatch, xPosition, yPosition);

            _textDisplay.DrawString(spriteBatch, amount.ToString(), new Vector2(xPosition + 30, yPosition + 47), Color.White);
        }
    }
}