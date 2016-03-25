using Blackjack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blackjack.CardGame
{
    struct Card
    {
        public Suit Suit;
        public Rank Rank;

        public Card(Suit suit, Rank rank)
        {
            Suit = suit;
            Rank = rank;
        }
    }
}