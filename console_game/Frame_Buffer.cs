using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace console_game
{
    class Frame_Buffer
    {
        public int WinWidth { private set; get; }
        public int WinHeight { private set; get; }
        private char[,] Frame;
        string NewFrame;
        public Frame_Buffer(int Width, int Height)
        {
            WinWidth = Width;
            WinHeight = Height;
            Frame = new char[WinWidth, WinHeight];
        }

        public void AddToRender(int x, int y, string textForFrame, string Centering = "none")
        {
            if (textForFrame.Length == 1)
            {
                Frame[x, y] = Convert.ToChar(textForFrame);
            }
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

        public void PrintFrame()
        {
            NewFrame = string.Empty;
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
            Console.SetCursorPosition(0, 0);
            Console.Write(NewFrame);
            Array.Clear(Frame, 0, Frame.Length);
        }
    }
}
