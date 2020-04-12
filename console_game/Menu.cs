using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Menu
    {
        public static Frame_Buffer GameRender;
        //public static High_Scores Scores = new High_Scores();
        public int WinWidth;
        public int WinHeight;
        private int difficulty = 2;
        public bool DisplayMenu(int winWidth, int winHeight)
        {
            bool doAgain = true;
            this.WinWidth = winWidth;
            this.WinHeight = winHeight;
            GameRender = new Frame_Buffer(WinWidth, WinHeight);
            int MenuPos = MenuDisplay(PrintMenuMain);
            switch (MenuPos)
            {
                case 1:
                    StartGame(WinWidth, WinHeight, difficulty);
                    break;
                case 2:
                    MenuPos = MenuDisplay(PrintMenuDifficulty);
                    switch (MenuPos)
                    {
                        case 1: difficulty = 1; break; //125
                        case 2: difficulty = 2; break; //200
                        case 3: difficulty = 3; break; //300
                        case 4: difficulty = 4; break; //500
                    }
                    break;
                case 3:
                    MenuDisplay(PrintMenuHelp);
                    break;
                case 4:
                    MenuDisplay(PrintMenuHighScores);
                    break;
                case 5:
                    doAgain = false;
                    break;
            }
            return doAgain;
        }

        private void StartGame(int WinWidth, int WinHeight, int enemyNum)
        {
            bool doAgain = true;
            do
            {
                Game game = new Game(WinWidth, WinHeight, enemyNum);
                game.Run();
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

        private int MenuDisplay(Func<int, int, int> MenuToDisplay)
        {
            bool NoOption = true;
            int MenuPos = 1;
            int MenuCursorLocation = 6;
            MenuToDisplay(MenuPos, MenuCursorLocation);
            while (NoOption)
            {
                if (Console.KeyAvailable)
                {
                    MenuPos = MenuNavigation(ref MenuPos, ref NoOption);
                    MenuToDisplay(MenuPos, MenuCursorLocation);
                }

            }
            return MenuPos;
        }

        private void MenuPosDisplay(int MenuPos, int MenuCursorLocation)
        {
            switch (MenuPos)
            {
                case 1:
                    MenuCursorLocation = 6;
                    break;
                case 2:
                    MenuCursorLocation = 9;
                    break;
                case 3:
                    MenuCursorLocation = 12;
                    break;
                case 4:
                    MenuCursorLocation = 15;
                    break;
                case 5:
                    MenuCursorLocation = 18;
                    break;
            }
            GameRender.AddToRender(10, MenuCursorLocation, "[-]");
        }

        private int MenuNavigation(ref int MenuPos, ref bool NoOption)
        {
            ConsoleKeyInfo userInput = Console.ReadKey(true);
            switch (userInput.Key)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                case ConsoleKey.NumPad8:
                    if (MenuPos > 1)
                    {
                        MenuPos -= 1;
                    }
                    break;
                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                case ConsoleKey.NumPad2:
                    if (MenuPos < 5)
                    {
                        MenuPos += 1;
                    }
                    break;
                case ConsoleKey.Enter:
                    NoOption = false;
                    break;
            }
            return MenuPos;
        }

        private int PrintMenuMain(int MenuPos, int MenuCursorLocation)
        {
            GameRender.AddToRender(0, 2, "No-Named-Game Menu", "middle");
            GameRender.AddToRender(10, 6, " -  Start Game");
            GameRender.AddToRender(10, 9, " -  Change Difficulty");
            GameRender.AddToRender(10, 12, " -  Help");
            GameRender.AddToRender(10, 15, " -  High Scores --- DOESN'T WORK");
            GameRender.AddToRender(10, 18, " -  Exit Game");
            GameRender.AddToRender(10, WinHeight - 5, "Use arrow keys, WASD or numpad to navigate between options", "middle");
            GameRender.AddToRender(10, WinHeight - 4, "Press enter to select an option", "middle");
            MenuPosDisplay(MenuPos, MenuCursorLocation);
            GameRender.PrintFrame();
            return MenuPos;
        }

        private int PrintMenuDifficulty(int MenuPos, int MenuCursorLocation)
        {
            GameRender.AddToRender(0, 2, "Difficulty Menu", "middle");
            GameRender.AddToRender(10, 6, " -  Easy Difficulty");
            GameRender.AddToRender(10, 9, " -  Medium Difficulty");
            GameRender.AddToRender(10, 12, " -  Hard Difficulty");
            GameRender.AddToRender(10, 15, " -  Extreme Difficulty");
            GameRender.AddToRender(10, 18, " -  <-- Go Back");
            string DifficultyText = "Easy";
            switch (difficulty)
            {
                case 1: DifficultyText = "Easy"; break;
                case 2: DifficultyText = "Medium"; break;
                case 3: DifficultyText = "Hard"; break;
                case 4: DifficultyText = "Extreme"; break;
            }
            GameRender.AddToRender(10, 21, "Current Difficulty: " + DifficultyText);
            MenuPosDisplay(MenuPos, MenuCursorLocation);
            GameRender.PrintFrame();
            return MenuPos;
        }

        private int PrintMenuHighScores(int MenuPos, int MenuCursorLocation)
        {
            GameRender.AddToRender(0, 2, "High Score Menu", "middle");
            GameRender.AddToRender(10, 6, " -  Easy Difficulty Records");
            GameRender.AddToRender(10, 9, " -  Medium Difficulty Records");
            GameRender.AddToRender(10, 12, " -  Hard Difficulty Records");
            GameRender.AddToRender(10, 15, " -  Extreme Difficulty Records");
            GameRender.AddToRender(10, 18, " -  <-- Go Back");
            MenuPosDisplay(MenuPos, MenuCursorLocation);
            GameRender.PrintFrame();
            return MenuPos;
        }

        private int PrintMenuHelp(int MenuPos, int MenuCursorLocation)
        {
            GameRender.AddToRender(0, 2, "Help Menu", "middle");
            GameRender.AddToRender(10, 6, " -  You are the \"@\" symbol");
            GameRender.AddToRender(10, 9, " -  Avoid the enemy X symbols");
            GameRender.AddToRender(10, 12, " -  If you touch an enemy X, game over");
            GameRender.AddToRender(10, 15, " -  Move using arrow keys, WASD or numpad");
            GameRender.AddToRender(10, 18, " -  <-- Go Back");
            MenuPosDisplay(MenuPos, MenuCursorLocation);
            GameRender.PrintFrame();
            return MenuPos;

        }
    }
}
