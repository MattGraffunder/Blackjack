using Blackjack.CardGame;
using Blackjack.Inputs;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blackjack
{
    class Player
    {
        Input _input;

        Hand _hand;

        public Player(Input input)
        {
            _input = input;
            _hand = new Hand();
        }

        public void Update(GameTime gameTime)
        {

        }

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
