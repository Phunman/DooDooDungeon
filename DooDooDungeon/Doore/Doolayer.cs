using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DooDooDungeon.Core
{
    public class Doolayer : DooGomb
    {
        public Doolayer()
        {
            Awareness = 15;
            Name = "TheDoo";
            Color = DooColors.Player;
            Symbol = '@';
            X = 10;
            Y = 10;
        }
    }
}
