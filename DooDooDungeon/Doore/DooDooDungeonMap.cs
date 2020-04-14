using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp;
using RLNET;

namespace DooDooDungeon.Core
{
    public class DungeonMap : Map
    {
        public List<Rectangle> Rooms;
        public DungeonMap()
        {
            Rooms = new List<Rectangle>();
        }

        public void Draw(RLConsole mapConsole)
        {
            mapConsole.Clear();
            foreach (Cell cell in GetAllCells())
            {
                SetConsoleSymbolForCell(mapConsole, cell);
            }
        }
        public bool SetActorPosition(DooGomb actor, int x, int y)
        {
            if (GetCell(x, y).IsWalkable)
            {
                SetIsWalkable(actor.X, actor.Y, true);
                actor.X = x;
                actor.Y = y;
                SetIsWalkable(actor.X, actor.Y, false);
                if (actor is Doolayer)
                {
                    UpdatePlayerFieldOfView();
                }
                return true;
            }
            return false;
        }

        public void SetIsWalkable(int x, int y, bool isWalkable)
        {
            Cell cell = GetCell(x, y);
            SetCellProperties(cell.X, cell.Y, cell.IsTransparent, isWalkable, cell.IsExplored);
        }

        private void SetConsoleSymbolForCell(RLConsole console, Cell cell)
        {
            if (!cell.IsExplored)
            {
                return;
            }

            if (IsInFov(cell.X, cell.Y))
            {
                if (cell.IsWalkable)
                {
                    console.Set(cell.X, cell.Y, DooColors.FloorFov, DooColors.FloorBackgroundFov, '.');
                }
                else
                {
                    console.Set(cell.X, cell.Y, DooColors.WallFov, DooColors.WallBackgroundFov, '#');
                }
            }
            else
            {
                if (cell.IsWalkable)
                {
                    console.Set(cell.X, cell.Y, DooColors.Floor, DooColors.FloorBackground, '.');
                }
                else
                {
                    console.Set(cell.X, cell.Y, DooColors.Wall, DooColors.WallBackground, '#');
                }
            }
        }
        public void UpdatePlayerFieldOfView()
        {
            Doolayer player = Game.Player;
            ComputeFov(player.X, player.Y, player.Awareness, true);
            foreach (Cell cell in GetAllCells())
            {
                if (IsInFov(cell.X, cell.Y))
                {
                    SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
                }
            }
        }
        public void AddPlayer(Doolayer player)
        {
            Game.Player = player;
            SetIsWalkable(player.X, player.Y, false);
            UpdatePlayerFieldOfView();
        }
    }

    
   
}
