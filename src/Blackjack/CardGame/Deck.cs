using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blackjack.CardGame
{
    abstract class Deck
    {
        protected const int CARDS_IN_DECK = 52;
        protected const int SUITS_IN_DECK = 4;
        protected const int RANKS_IN_DECK = 13;

        Queue<Card> _deck;
        Random _rng;

        public int NumberOfStandardDecks { get; private set; }

        public int CardsRemaining
        {
            get
            {
                return _deck.Count;
            }
        }

        public Deck(int numberOfStandardDecks)
        {
            _deck = new Queue<Card>();
            _rng = new Random();

            NumberOfStandardDecks = numberOfStandardDecks;
        }

        public void ShuffleDeck()
        {
            //Rebuild the Deck
            Card[] deckBuild = new Card[NumberOfStandardDecks * CARDS_IN_DECK];

            int arrayPosition = 0;

            //Build all the cards
            for (int i = 0; i < NumberOfStandardDecks; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 13; k++)
                    {
                        // Deck Number Multiplied by Number of Cards
                        // Plus Suit Number Multiplied by Number of Ranks
                        arrayPosition = (i * CARDS_IN_DECK) + (j * RANKS_IN_DECK) + k;
                        deckBuild[arrayPosition] = new Card((Suit)(j + 1), (Rank)(k + 1)); // Suits are numbered 1-4 and Ranks 1-13                         
                    }
                }
            }

            //Shuffle Deck
            for (int i = 0; i < deckBuild.Length; i++)
            {
                // Get a random position in the array
                int position = i + _rng.Next(deckBuild.Length - i);
                //int position = _rng.Next(deckBuild.Length);

                //Swap Elements
                Card swap = deckBuild[i];
                deckBuild[i] = deckBuild[position];
                deckBuild[position] = swap;
            }

            //Build Queue from shuffled Array
            _deck = new Queue<Card>(deckBuild);
        }

        public virtual Card Draw()
        {
            if (_deck.Count == 0)
            {
                throw new Exception("No Cards Remaining");
            }

            return _deck.Dequeue();
        }
    }
}