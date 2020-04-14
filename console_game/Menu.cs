using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace console_game
{
    class Menu
    {
        public static int WinWidth;
        public static int WinHeight;
        public Frame_Buffer GameRender;
        private Stack<Page> _menuStack = new Stack<Page>();
        Dictionary<string,Page> Pages;

        public Menu(int WinWidth, int WinHeight, Frame_Buffer GameRender) {
            this.GameRender = GameRender;
            Menu.WinWidth = WinWidth;
            Menu.WinHeight = WinHeight;
            LoadPages();
        }

        public void DisplayMenu() {
            Pages["Home"].AddToDisplay(GameRender);
            GameRender.PrintFrame();
            while (true) {
                ConsoleKeyInfo userInput = Console.ReadKey(true);
                if (userInput.Key == ConsoleKey.Enter) {
                    break;
                }
            }
        }

            
        //_menuStack.Push()

        public class Page {
            protected List<MenuLine> DisplayPage;
            public Page() {
                DisplayPage = new List<MenuLine>();
            }
            public void AddToPage(int x, int y, int YDistancing, string[] Lines) {
                int displayX;
                foreach (string Line in Lines) {
                    //Center text in x-axis if x is -1
                    if (x == -1) {
                        displayX = (WinWidth / 2) - (Line.Length / 2);
                    }
                    else {
                        displayX = x;
                    }
                    DisplayPage.Add(new MenuLine(Line, displayX, y));
                    y += YDistancing;
                }
            }
            public void AddToDisplay(Frame_Buffer GameRender) {
                foreach (MenuLine Line in DisplayPage) {
                    GameRender.AddToRender(Line.XPos,Line.YPos,Line.Line);
                }
            }
            public struct CursorLocation {
                int x;
                int y;

                //Call this func if cursor is on Location and user hits enter.
                //Func could call to a new page and add it to the stack or 
                //The Func could start the game.
                Delegate FuncToCall;
            }
        }
        public class MenuLine {
            public string Line;
            private int _yPos;
            private int _xPos;
            public int XPos {
                get {
                    return _xPos;
                }
                set {
                    if (value >= 0 & value < WinWidth-Line.Length) {
                        _xPos = value;
                    }
                    else {
                        _xPos = 0;
                        Debug.WriteLine("Invalid X co-ordinate passed: {0}", value);
                    }
                }
            }
            public int YPos {
                get { return _yPos; }
                set {
                    if (value >= 0 & value < WinHeight) {
                        _yPos = value;
                    }
                    else {
                        _yPos = 0;
                        Debug.WriteLine("Invalid Y co-ordinate passed: {0}", value);
                    }
                }
            }

            public MenuLine(string Line, int XPos, int YPos) {
                this.Line = Line;
                this.XPos = XPos;
                this.YPos = YPos;
            }
        }

        public void LoadPages() {
            Pages = new Dictionary<string, Page>();

            //Create pages and add to dict. Potential change in future to only load pages into dict as needed.
            //Perhaps use a simple LRU cache to store some pages? This is not needed but would be good learning exercise.
            Page PageToAdd = new Page();
            
            PageToAdd.AddToPage(-1, 2, 0, new string[] { "No-Named-Game Menu" });
            PageToAdd.AddToPage(5, 6, 3, new string[] {
                " -  Start Game",
                " -  Change Difficulty",
                " -  Help",
                " -  High Scores --- NOT IMPLEMENTED",
                " -  Exit Game"});
            PageToAdd.AddToPage(-1, WinHeight - 5, 1, new string[] {
                "Use arrow keys, WASD or numpad to navigate between options",
                "Press enter to select an option",
                "This page is broke in this dev build, hit enter to play game"});
            Pages["Home"] = PageToAdd;

            PageToAdd = new Page();
            PageToAdd.AddToPage(-1, 2, 0, new string[] { "Difficulty Menu" });
            PageToAdd.AddToPage(5, 6, 3, new string[] {
                " -  Extreme Difficulty",
                " -  Hard Difficulty",
                " -  Medium Difficulty",
                " -  Easy Difficulty",
                " -  <-- Go Back"});
            Pages["Difficulty"] = PageToAdd;

            PageToAdd = new Page();
            PageToAdd.AddToPage(-1, 2, 0, new string[] { "Help Menu" });
            PageToAdd.AddToPage(5, 6, 3, new string[] {
                " -  You are the \"@\" symbol",
                " -  Avoid the enemy X symbols",
                " -  If you touch an enemy X, game over",
                " -  Move using arrow keys, WASD or numpad",
                " -  <-- Go Back"});
            Pages["Help"] = PageToAdd;

        }
    }
}