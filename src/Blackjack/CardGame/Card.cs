using Blackjack;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Blackjack.CardGame
{
    [DebuggerDisplay("{Rank} of {Suit}")]
    struct Card
    {
        public Suit Suit;
        public Rank Rank;

        //public Vector2 Position { get; set; }

        public Card(Suit suit, Rank rank)
        {
            Suit = suit;
            Rank = rank;
        }

        //public void Draw()
        //{

        //}
    }
}