using System; // We need the standard .NET library for many things

namespace console_game
{
    class ConsoleGame
    {

        public static int WinWidth;
        public static int WinHeight;
        static void Main()
        {
            // Sets window size
            WinWidth = 75;
            WinHeight = 45;
            if (Console.LargestWindowHeight < WinHeight) {
                WinHeight = Console.LargestWindowHeight - 2;
            }
            if (Console.LargestWindowWidth < WinWidth) {
                WinWidth = Console.LargestWindowWidth - 2;
            }
            Console.SetWindowSize(WinWidth, WinHeight);
            Console.CursorVisible = false;

            //Set up environment variables for the frame buffer.
            Frame_Buffer.SetUpBuffer(WinWidth, WinHeight);

            //Run the general game loop
            Menu<MenuPages> GameMenu = new Menu<MenuPages>();
            Game game = new Game(WinWidth, WinHeight, 200);
            while (true) {
                bool playGame = GameMenu.StartMenu();
                if (!playGame) {
                    break;
                }
                game.Run();
                //StartGame(WinWidth, WinHeight, 200);
            }
            Console.Clear();

            // Set cursor to bottom most row
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
        }

        private void StartGame(int WinWidth, int WinHeight, int enemyNum) {
            bool doAgain = true;
                //Game game = new Game(WinWidth, WinHeight, enemyNum);
                //game.Run();
            while (doAgain) {
                ConsoleKeyInfo userInput = Console.ReadKey(true);
                if (userInput.Key == ConsoleKey.Enter) {
                doAgain = true;
                }
                else if (userInput.Key == ConsoleKey.Escape) {
                doAgain = false;
                }
            }
        }

    }
}