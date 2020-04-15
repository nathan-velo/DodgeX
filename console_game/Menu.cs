using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace console_game {

    class Menu<T> where T : IPageGroup, new() {
        public LRUCache<Page> Cache;
        public Frame_Buffer GameRender;
        public IPageGroup PageCatalogue;
        private Page CurrPage;
        private Stack<String> _menuStack = new Stack<String>();

        public Menu(Frame_Buffer GameRender, int CacheSize = 5) {
            this.GameRender = GameRender;
            Cache = new LRUCache<Page>(CacheSize);
            PageCatalogue = new T();
        }

        public bool StartMenu() {
            string PageToDisplay = "Home";
            CurrPage = GetPage(PageToDisplay);
            _menuStack.Push(PageToDisplay);
            return(MenuLoop());
        }

        public bool MenuLoop() {
            int MenuPos = 0;
            int CursorMax = CurrPage.CursorLocations.Length;
            while (true) {
                CurrPage.AddToDisplay();
                RenderCursor(MenuPos, CurrPage);
                GameRender.PrintFrame();
                ConsoleKeyInfo userInput = Console.ReadKey(true);

                if ( userInput.Key == ConsoleKey.Enter) {
                    return true;
                }

                MenuNav(ref MenuPos, CursorMax, userInput);
            }
        }

        public void ChangePage(string ChangeToPage) {
            CurrPage = GetPage(ChangeToPage);
            _menuStack.Push(ChangeToPage);
        }

        public void RenderCursor(int MenuPos, Page CurrPage) {;
            GameRender.AddToRender(CurrPage.CursorLocations[MenuPos].x, 
                CurrPage.CursorLocations[MenuPos].y, "[-]");
        }

        public Page GoBack() {
            Page Back = GetPage(_menuStack.Pop());
            return Back;
        }

        public Page GetPage(string PageName) {
            Page PageGet = Cache.Get(PageName);
            if (PageGet != null) {
                return PageGet;
            }
            else {
                PageGet = PageCatalogue.Pages[PageName].Invoke();
                Cache.Put(PageName, PageGet);
                return PageGet;
            }
        }

        public void MenuNav(ref int MenuPos, int CursorMax, ConsoleKeyInfo userInput) {
            switch (userInput.Key) {
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                case ConsoleKey.NumPad8:
                    if (MenuPos > 0) {
                        MenuPos -= 1;
                    }
                    else {
                        MenuPos = CursorMax - 1;
                    }
                    break;
                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                case ConsoleKey.NumPad2:
                    if (MenuPos < CursorMax-1) {
                        MenuPos += 1;
                    }
                    else {
                        MenuPos = 0;
                    }
                    break;
                case ConsoleKey.Enter:
                    break;
            }
        }
    }
    public class Page {
        private List<MenuLine> DisplayPage;
        public CursorLocation[] CursorLocations { private set; get; }
        public Page() {
            DisplayPage = new List<MenuLine>();
        }
        public void AddToPage(int x, int y, int YDistancing, string[] Lines) {
            int displayX;
            foreach (string Line in Lines) {
                //Center text in x-axis if x is -1
                if (x == -1) {
                    displayX = (ConsoleGame.WinWidth / 2) - (Line.Length / 2);
                }
                else {
                    displayX = x;
                }
                DisplayPage.Add(new MenuLine(Line, displayX, y));
                y += YDistancing;
            }
        }
        public void AddToDisplay() {
            foreach (MenuLine Line in DisplayPage) {
                ConsoleGame.GameRender.AddToRender(Line.XPos, Line.YPos, Line.Line);
            }
        }

        public void AddCursorLocs(int x, int y, int YDistancing, int numberOfCursors) {
            CursorLocations = new CursorLocation[numberOfCursors];
            for (int i = 0; i < numberOfCursors; i++) {
                CursorLocations[i] = new CursorLocation { x = x,y = y+YDistancing*i};
            }
        }

        public struct CursorLocation {
            public int x;
            public int y;
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
                    if (value >= 0 & value < ConsoleGame.WinWidth - Line.Length) {
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
                    if (value >= 0 & value < ConsoleGame.WinHeight) {
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
    }

    public interface IPageGroup {
        IDictionary<string, Func<Page>> Pages { get; set; }
        void RegisterPages();
    }

    class MenuPages : IPageGroup {
        public IDictionary<string, Func<Page>> Pages { get; set; } = new Dictionary<string, Func<Page>>();

        public MenuPages() {
            RegisterPages();
        }

        public void RegisterPages() {
            Pages.Add("Difficulty", LoadDifficulty);
            Pages.Add("Home", LoadHome);
            Pages.Add("Help", LoadHelp);
        }

        public Page LoadDifficulty() {
            Page LoadPage = new Page();
            LoadPage.AddToPage((int)Centering.middle, 2, 0, new string[] { "No-Named-Game Menu" });
            LoadPage.AddToPage(5, 6, 3, new string[] {
                            " -  Start Game",
                            " -  Change Difficulty",
                            " -  Help",
                            " -  High Scores --- NOT IMPLEMENTED",
                            " -  Exit Game"});
            LoadPage.AddToPage((int)Centering.middle, ConsoleGame.WinHeight - 5, 1, new string[] {
                            "Use arrow keys, WASD or numpad to navigate between options",
                            "Press enter to select an option",
                            "This page is broke in this dev build, hit enter to play game"});

            return LoadPage;
        }

        public Page LoadHome() {
            Page LoadPage = new Page();

            LoadPage.AddToPage(-1, 2, 0, new string[] { "No-Named-Game Menu" });
            LoadPage.AddToPage(5, 6, 3, new string[] {
                " -  Start Game",
                " -  Change Difficulty",
                " -  Help",
                " -  High Scores --- NOT IMPLEMENTED",
                " -  Exit Game"});
            LoadPage.AddToPage(-1, ConsoleGame.WinHeight - 5, 1, new string[] {
                "Use arrow keys, WASD or numpad to navigate between options",
                "Press enter to select an option",
                "This page is broke in this dev build, hit enter to play game"});

            //X, Y, Y Distance between each cursor adding from initial Y, Amount
            LoadPage.AddCursorLocs(5, 6, 3, 5);
            return LoadPage;
        }

        public Page LoadHelp() {
            Page LoadPage = new Page();

            LoadPage.AddToPage(-1, 2, 0, new string[] { "Help Menu" });
            LoadPage.AddToPage(5, 6, 3, new string[] {
                " -  You are the \"@\" symbol",
                " -  Avoid the enemy X symbols",
                " -  If you touch an enemy X, game over",
                " -  Move using arrow keys, WASD or numpad",
                " -  <-- Go Back"});

            return LoadPage;
        }
    }   
}