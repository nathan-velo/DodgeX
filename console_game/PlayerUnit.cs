using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace console_game
{
    class PlayerUnit : Unit
    {
        public PlayerUnit(int x, int y, string unitGraphic) : base(x, y, unitGraphic)
        {
        }


        override public void Update(int frameTimingMS)
        {
            if (Console.KeyAvailable == true)
            {
                ConsoleKeyInfo userInput = Console.ReadKey(true);

                switch (userInput.Key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                    case ConsoleKey.NumPad8:
                        if (Y > 0)
                        {
                            Y = Y - 1;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                    case ConsoleKey.NumPad2:
                        if (Y < Game.WinHeight - 2)
                        {
                            Y = Y + 1;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.A:
                    case ConsoleKey.NumPad4:
                        if (X > 0)
                        {
                            X = X - 1;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.D:
                    case ConsoleKey.NumPad6:
                        if (X < Game.WinWidth - 1)
                        {
                            X = X + 1;
                        }
                        break;
                }
            }
            base.Update(frameTimingMS);
        }
    }
}
