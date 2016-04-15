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
        public bool Exit { get; protected set; }

        public bool Deal { get; protected set; }

        public bool Hit { get; protected set; }

        public bool Split { get; protected set; }

        public bool DoubleDown { get; protected set; }

        public bool Stay { get; protected set; }

        public abstract void Update();
    }
}