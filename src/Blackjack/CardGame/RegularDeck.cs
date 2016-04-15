using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blackjack.CardGame
{
    class RegularDeck : Deck
    {
        public RegularDeck(int numberOfStandardDecks)
            : base(numberOfStandardDecks)
        {
        }
    }
}
