using System; // We need the standard .NET library for many things

namespace ConsoleApp1
{
    class ConsoleApp1
    {

        static void Main()
        {
            // Sets window size
            int WinWidth = (int)(75);
            int WinHeight = (int)(45);
            if (Console.LargestWindowHeight < WinHeight)
            {
                WinHeight = Console.LargestWindowHeight - 2;
            }
            if (Console.LargestWindowWidth < WinWidth)
            {
                WinWidth = Console.LargestWindowWidth - 2;
            }
            Console.SetWindowSize(WinWidth, WinHeight);

            // Hides cursor
            Console.CursorVisible = false;

            //Run the game
            Menu GameMenu;
            GameMenu = new Menu();
            bool doAgain;
            do
            {
                doAgain = GameMenu.DisplayMenu(WinWidth, WinHeight);
            } while (doAgain == true);
            Console.Clear();

            // Set cursor to bottom most row
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
        }
    }
}