using Blackjack.CardGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blackjack
{
    class CardDisplay
    {
        const int CARD_HEIGHT = 780;
        const int CARD_WIDTH = 560;

        const float LARGE_SCALE = .25f; // Multiplier: 0 to 1 
        const float SMALL_SCALE = .15f; // Multiplier: 0 to 1 

        public Texture2D _cardSpriteSheet { get; private set; }
        public Texture2D _cardBack { get; private set; }

        public void Initialize(Texture2D cardSpriteSheet, Texture2D cardBack)
        {
            _cardSpriteSheet = cardSpriteSheet;
            _cardBack = cardBack;
        }

        public void DrawCard(SpriteBatch spriteBatch, Card card, int xPosition, int yPosition)
        {
            DrawCard(spriteBatch, card, xPosition, yPosition, LARGE_SCALE);
        }

        public void DrawCardMiniature(SpriteBatch spriteBatch, Card card, int xPosition, int yPosition)
        {
            DrawCard(spriteBatch, card, xPosition, yPosition, SMALL_SCALE);
        }

        public void DrawCard(SpriteBatch spriteBatch, Card card, int xPosition, int yPosition, float scale)
        {
            //Figure out how big the cards are, and how much to scale them
            spriteBatch.Draw(_cardSpriteSheet,
                new Rectangle(xPosition, yPosition, (int)(CARD_WIDTH * scale), (int)(CARD_HEIGHT * scale)),
                new Rectangle(CardStartWidth(card), CardStartHeight(card), CARD_WIDTH, CARD_HEIGHT),
                Color.White);
        }

        public void DrawCardBack(SpriteBatch spriteBatch, int xPosition, int yPosition)
        {
            spriteBatch.Draw(_cardBack,
                new Rectangle(xPosition, yPosition - 2, (int)(CARD_WIDTH * LARGE_SCALE), (int)(CARD_HEIGHT * LARGE_SCALE) + 2),
                new Rectangle(0, 2, CARD_WIDTH, CARD_HEIGHT),
                Color.White);
        }

        private int CardStartHeight(Card card)
        {
            //Cards are ordered: Club -> Diamond -> Heart -> Spade -> on the sprite sheet
            //Card height is 780
            switch (card.Suit)
            {
                case Suit.Club:
                    return 0 * CARD_HEIGHT;
                case Suit.Diamond:
                    return 1 * CARD_HEIGHT;
                case Suit.Heart:
                    return 2 * CARD_HEIGHT;
                case Suit.Spade:
                    return 3 * CARD_HEIGHT;
                default:
                    throw new Exception(String.Format("Suit: {0} is invalid", card.Suit));
            }
        }

        private int CardStartWidth(Card card)
        {
            //Card Ranks Start at 1 and go through 13.
            //Card width is 560 on the sprite sheet
            if ((int)card.Rank < 1 || (int)card.Rank > 13)
            {
                throw new Exception(string.Format("Rank: {0} is invalid", card.Rank));
            }

            return ((int)card.Rank - 1) * CARD_WIDTH;
        }
    }
}
