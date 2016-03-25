using Blackjack.CardGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blackjack
{
    class Dealer
    {
        Hand _hand = new Hand();
                
        public void TakeCard(Card card)
        {
            _hand.Add(card);
        }

        public void ClearHand()
        {
            _hand.Clear();
        }
    }
}