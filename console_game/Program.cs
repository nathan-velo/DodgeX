using System; // We need the standard .NET library for many things

namespace console_game
{
    class console_game
    {
        public static Frame_Buffer GameRender;
        static void Main()
        {
            // Sets window size
            int WinWidth = (int)(75);
            int WinHeight = (int)(45);
            if (Console.LargestWindowHeight < WinHeight) {
                WinHeight = Console.LargestWindowHeight - 2;
            }
            if (Console.LargestWindowWidth < WinWidth) {
                WinWidth = Console.LargestWindowWidth - 2;
            }
            Console.SetWindowSize(WinWidth, WinHeight);
            Console.CursorVisible = false;

            GameRender = new Frame_Buffer(WinWidth, WinHeight);

            bool exitGame = true;

            //Run the game, game and menu loop
            Menu GameMenu;
            GameMenu = new Menu(WinWidth, WinHeight, GameRender);
            while (true) {
                GameMenu.DisplayMenu();
                if (exitGame != true) {
                    break;
                }
                Game game = new Game(WinWidth, WinHeight, 200, GameRender);
                game.Run();
                //StartGame(WinWidth, WinHeight, 200);
            }
            Console.Clear();

            // Set cursor to bottom most row
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
        }

        private void StartGame(int WinWidth, int WinHeight, int enemyNum)
        {
            bool doAgain = true;
            do
            {
                //Game game = new Game(WinWidth, WinHeight, enemyNum);
                //game.Run();
                while (true)
                {
                    ConsoleKeyInfo userInput = Console.ReadKey(true);
                    if (userInput.Key == ConsoleKey.Enter)
                    {
                        doAgain = true;
                        break;
                    }
                    else if (userInput.Key == ConsoleKey.Escape)
                    {
                        doAgain = false;
                        break;
                    }
                }
            } while (doAgain == true);
        }

    }


}