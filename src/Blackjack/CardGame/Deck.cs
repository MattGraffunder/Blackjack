using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blackjack.CardGame
{
    class Deck
    {
        public int NumberOfStandardDecks { get; private set; }

        public Deck(int numberOfStandardDecks)
        {
            NumberOfStandardDecks = numberOfStandardDecks;
        }

        public Card Draw()
        {
            //Return a random card for now
            Random rand = new Random();
            Suit suit = (Suit)rand.Next(1, 5);
            Rank rank = (Rank)rand.Next(1, 14);

            return new Card(suit, rank);
        }
    }
}
