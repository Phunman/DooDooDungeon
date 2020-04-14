using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using RogueSharp;
using DooDooDungeon.Core;

namespace DooDooDungeon.Systems
{
    public class Doozgas
    {
        public bool MovePlayer(DooRany direction)
        {
            int x = Game.Player.X;
            int y = Game.Player.Y;
            switch (direction)
            {
       case DooRany.Up:
            {
                y = Game.Player.Y - 1;
                break;
            }
         case DooRany.Down:
            {
                y = Game.Player.Y + 1;
                  break;
            }
          case DooRany.Left:
           {
                x = Game.Player.X - 1;
               break;
            }
          case DooRany.Right:
            {
               x = Game.Player.X + 1;
                break;
            }
          default:
            {
                 return false;
            }
            }
            if (Game.DungeonMap.SetActorPosition(Game.Player, x, y))
            {
                return true;
            }
            return false;
        }
    }
}
