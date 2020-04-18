using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace console_game {

    class Menu<T> where T : IPageGroup, new() {
        public LRUCache<Page> Cache;
        public IPageGroup PageCatalogue;
        private Page CurrPage;
        private Stack<String> _menuStack = new Stack<String>();

        public Menu(int CacheSize = 5) {
            Cache = new LRUCache<Page>(CacheSize);
            PageCatalogue = new T();
        }

        public bool StartMenu() {
            string PageToDisplay = "Home";
            CurrPage = GetPage(PageToDisplay);
            return(MenuLoop());
        }

        public bool MenuLoop() {
            int MenuPos = 0;
            int CursorMax = CurrPage.CursorLocations.Length;
            while (true) {
                CurrPage.AddToDisplay();
                RenderCursor(MenuPos, CurrPage);
                Frame_Buffer.PrintFrame();
                ConsoleKeyInfo userInput = Console.ReadKey(true);

                if (userInput.Key == ConsoleKey.Enter) {
                    return true;
                }

                MenuNav(ref MenuPos, CursorMax, userInput);
            }
        }

        public void ChangePage(string ChangeToPage) {
            CurrPage = GetPage(ChangeToPage);
            _menuStack.Push(ChangeToPage);
        }

        public void RenderCursor(int MenuPos, Page CurrPage) {
            ;
            Frame_Buffer.AddToRender(CurrPage.CursorLocations[MenuPos].x,
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
                    if (MenuPos < CursorMax - 1) {
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

    public static class Centering {
        public const int left = 0;
        public const int middle = -1;
    }
}