using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using RogueSharp;
using DooDooDungeon.Interfaces;

namespace DooDooDungeon.Core
{
    public class DooGomb : IActor, IDrawable
    {
        public string Name { get; set; }
        public int Awareness { get; set; }
        public RLColor Color { get; set; }
        public char Symbol { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public void Draw(RLConsole console, IMap map)
        {
            if (!map.GetCell(X, Y).IsExplored)
            {
                return;
            }
            if (map.IsInFov(X, Y))
            {
                console.Set(X, Y, Color, DooColors.FloorBackgroundFov, Symbol);
            }
            else
            {
                console.Set(X, Y, DooColors.Floor, DooColors.FloorBackground, '.');
            }
        }
    }
}
