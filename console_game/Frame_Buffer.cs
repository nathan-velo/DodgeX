using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace console_game
{
    static class Frame_Buffer
    {
        public static int WinWidth { set; get; }
        public static int WinHeight { set; get; }
        private static char[,] Frame;
        static string NewFrame;

        public static void SetUpBuffer(int WinWidth, int WinHeight) {
            Frame_Buffer.WinWidth = WinWidth;
            Frame_Buffer.WinHeight = WinHeight;
            Frame = new char[WinWidth,WinHeight];
        }

        public static void AddToRender(int x, int y, string textForFrame, string Centering = "none")
        {
            //If string 1 char long put char at x,y
            if (textForFrame.Length == 1)
            {
                Frame[x, y] = Convert.ToChar(textForFrame);
            }
            //If string longer then 1 char convert to array and
            //add to frame array one char at a time
            else if (textForFrame.Length > 1)
            {
                char[] charForFrame = textForFrame.ToCharArray();
                if (Centering == "left")
                {
                    x = 0;
                }
                else if (Centering == "middle")
                {
                    x = (WinWidth / 2) - (textForFrame.Length / 2);
                }
                else if (Centering == "right")
                {
                    x = WinWidth - textForFrame.Length;
                }
                foreach (var item in charForFrame)
                {
                    Frame[x, y] = item;
                    x++;
                }
            }
            else { Debug.WriteLine("String of size 0 passed to frame buffer"); }
        }

        public static void PrintFrame()
        {
            NewFrame = string.Empty;
            //Loop through frame array and create string from it.
            for (int y = 0; y < WinHeight; y++)
            {
                for (int x = 0; x < WinWidth; x++)
                {
                    NewFrame += Frame[x, y];
                }
                // Add new row as long as it doesn't go below height of the console
                if (y < WinHeight - 1)
                {
                    NewFrame += Environment.NewLine;
                }
            }
            Console.CursorVisible = false;

            //Set console cursor to 0 position on screen and print string
            Console.SetCursorPosition(0, 0);
            Console.Write(NewFrame);

            //Clear Frame array.
            Array.Clear(Frame, 0, Frame.Length);
        }
    }
}
