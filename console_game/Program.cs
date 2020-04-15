using System; // We need the standard .NET library for many things

namespace console_game
{
    public enum Centering {
        left = 0,
        middle = -1,
    }
    class ConsoleGame
    {

        public static int WinWidth;
        public static int WinHeight;
        public static Frame_Buffer GameRender;
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

            GameRender = new Frame_Buffer(WinWidth, WinHeight);

            //Run the game, game and menu loop
            Menu<MenuPages> GameMenu = new Menu<MenuPages>(GameRender);
            Game game = new Game(WinWidth, WinHeight, 200, GameRender);
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