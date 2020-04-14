using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using RogueSharp;
using RogueSharp.Random;
using DooDooDungeon.Core;
using DooDooDungeon.Systems;

namespace DooDooDungeon
{
    public class Game
    {
        private static bool _renderRequired = true;
        public static Doolayer Player { get; set; }
        public static DungeonMap DungeonMap { get; private set; }
        public static Doozgas CommandSystem { get; private set; }
        public static IRandom Random { get; private set; }
        private static RLRootConsole _rootConsole;
        private static RLConsole _mapConsole;
        private static RLConsole _headerConsole;
        private static readonly int _mapHeight = 48;
        private static readonly int _screenHeight = 70;
        private static readonly int _headerHeight = 11;
        private static readonly int _headerWidth = 80;
        private static readonly int _screenWidth = 100;
        private static readonly int _mapWidth = 80;
        public static void Main()
        {
            int seed = (int)DateTime.UtcNow.Ticks;
            Random = new DotNetRandom(seed);
            string fontFileName = "terminal8x8.png";
            string consoleTitle = "Doo Doo Dungeon";
            _rootConsole = new RLRootConsole(fontFileName, _screenWidth, _screenHeight,
              8, 8, 1f, consoleTitle);
            _mapConsole = new RLConsole(_mapWidth, _mapHeight);
            _headerConsole = new RLConsole(_headerWidth, _headerHeight);
            DooGenerator mapGenerator = new DooGenerator(_mapWidth, _mapHeight, 20, 13, 7);
            DungeonMap = mapGenerator.CreateMap();
            DungeonMap.UpdatePlayerFieldOfView();
            CommandSystem = new Doozgas();
            _rootConsole.Update += OnRootConsoleUpdate;
            _rootConsole.Render += OnRootConsoleRender;
            _rootConsole.Run();
        }
        private static void OnRootConsoleUpdate(object sender, UpdateEventArgs e)
        {
            bool didPlayerAct = false;
            RLKeyPress keyPress = _rootConsole.Keyboard.GetKeyPress();

            if (keyPress != null)
            {
          if (keyPress.Key == RLKey.Up)
            {
                didPlayerAct = CommandSystem.MovePlayer(DooRany.Up);
              }
                else if (keyPress.Key == RLKey.Down)
                {
                    didPlayerAct = CommandSystem.MovePlayer(DooRany.Down);
                }
            else if (keyPress.Key == RLKey.Left)
                {
                    didPlayerAct = CommandSystem.MovePlayer(DooRany.Left);
                }
            else if (keyPress.Key == RLKey.Right)
             {
                    didPlayerAct = CommandSystem.MovePlayer(DooRany.Right);
             }
           else if (keyPress.Key == RLKey.Escape)
              {
                 _rootConsole.Close();
                }
            }
            if (didPlayerAct)
            {
                _renderRequired = true;
            }
        }
        private static void OnRootConsoleRender(object sender, UpdateEventArgs e)
        {
            if (_renderRequired)
            {
                DungeonMap.Draw(_mapConsole);
                Player.Draw(_mapConsole, DungeonMap);
                RLConsole.Blit(_mapConsole, 0, 0, _mapWidth, _mapHeight,
              _rootConsole, 0, _headerHeight);
                RLConsole.Blit(_headerConsole, 0, 0, _headerWidth, _headerHeight,
                  _rootConsole, 0, 0);
                Player.Draw(_mapConsole, DungeonMap);
                DungeonMap.Draw(_mapConsole);
                _rootConsole.Draw();

                _renderRequired = false;
            }
            
        }
    }
}
