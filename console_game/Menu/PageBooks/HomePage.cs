using System;
using System.Collections.Generic;
using System.Text;

namespace console_game {
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
            LoadPage.AddToPage(Centering.middle, 2, 0, new string[] { "No-Named-Game Menu" });
            LoadPage.AddToPage(5, 6, 3, new string[] {
                            " -  Start Game",
                            " -  Change Difficulty",
                            " -  Help",
                            " -  High Scores --- NOT IMPLEMENTED",
                            " -  Exit Game"});
            LoadPage.AddToPage(Centering.middle, ConsoleGame.WinHeight - 5, 1, new string[] {
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