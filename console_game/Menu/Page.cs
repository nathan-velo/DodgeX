using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace console_game {
    public class Page {
        private List<MenuLine> DisplayPage;
        public CursorLocation[] CursorLocations { private set; get; }
        public Page() {
            DisplayPage = new List<MenuLine>();
        }

        //Add text to page, handles actions such as text centering
        public void AddToPage(int x, int y, int YDistancing, string[] Lines) {
            int displayX;
            foreach (string Line in Lines) {
                //Center text in x-axis if x is Centering-middle(-1)
                if (x == Centering.middle) {
                    displayX = (ConsoleGame.WinWidth / 2) - (Line.Length / 2);
                }
                //Else start text from whatever x was set to in args
                else {
                    displayX = x;
                }
                //Add line to the page's list of lines
                DisplayPage.Add(new MenuLine(Line, displayX, y));
                y += YDistancing;
            }
        }
        public void AddToDisplay() {
            //Add every line in page's display list to renderer
            foreach (MenuLine Line in DisplayPage) {
                Frame_Buffer.AddToRender(Line.XPos, Line.YPos, Line.Line);
            }
        }

        //Add locations to screen of where the cursor can travel.
        public void AddCursorLocs(int x, int y, int YDistancing, int numberOfCursors) {
            CursorLocations = new CursorLocation[numberOfCursors];
            for (int i = 0; i < numberOfCursors; i++) {
                CursorLocations[i] = new CursorLocation { x = x, y = y + YDistancing * i };
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
                    //Ensure x value doesn't go off screen
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
                    //Ensure y value doesn't go off screen
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
}
