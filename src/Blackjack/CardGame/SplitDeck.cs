using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blackjack.CardGame
{
    class SplitDeck:Deck
    {
        int suitCount;

        internal SplitDeck(int numberOfRegularDecks) : base(numberOfRegularDecks) { }

        public override Card Draw()
        {
            Suit suit = (Suit)((++suitCount % 4) + 1);

            return new Card(suit, Rank.Eight);
        }
    }
}
