using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blackjack.CardGame
{
    class Hand : List<Card>
    {
        public int HandValue
        {
            get
            {
                int value = 0;
                
                foreach (Card card in this.Where(c => c.Rank != Rank.Ace))
                {
                    switch (card.Rank)
                    {
                        case Rank.Jack:
                        case Rank.Queen:
                        case Rank.King:
                            value += 10;
                            break;
                        default:
                            value += (int)card.Rank;
                            break;
                    }
                }

                // Add Aces Last
                var aces = this.Where(c => c.Rank == Rank.Ace);

                foreach (Card ace in aces)
                {
                    if (value < 11)
                    {
                        value += 11;
                    }
                    else
                    {
                        value += 1;
                    }
                }

                return value;
            }
        }

        public bool CanSplit
        {
            get
            {
                //If there are two cards, and they are the same Rank the player can split
                return (this.Count == 2 && this[0].Rank == this[1].Rank);                
            }
        }

        public bool CanDoubleDown
        {
            get
            {
                return this.Count == 2;
            }
        }
    }
}