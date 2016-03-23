using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blackjack.Inputs
{
    abstract class Input
    {
        //Exit Game
        public bool Exit { get; set; }
        
        public abstract void Update(); 
    }
}