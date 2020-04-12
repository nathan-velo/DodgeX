using System;
using System.Diagnostics;

namespace ConsoleApp1
{
    abstract class Unit
    {
        public Unit(int x, int y, string unitGraphic)
        {
            this.X = x;
            this.Y = y;
            this.UnitGraphic = unitGraphic;
        }

        public int X
        {
            get
            {
                return _x;
            }
            set
            {
                if (value < 0 || value >= Console.WindowWidth)
                {
                    throw new Exception("Invalid X coordinates passed");
                }
                //Undraw();
                _x = value;

            }
        }
        private int _x;

        public int Y
        {
            get
            {
                return _y;
            }
            set
            {
                if (value < 0 || value >= Console.WindowHeight)
                {
                    Debug.WriteLine("Coordinates at time of exception X: {0} Y: {1}", X, Y);
                    throw new Exception("Invalid Y coordinates passed");
                }
                //Undraw();
                _y = value;

            }
        }
        private int _y;

        public string UnitGraphic;

        virtual public void Update(int frameTimingMS)
        {
        }

        [Obsolete("Use the new rendering class")]
        virtual public void Draw()
        {
        }

        [Obsolete("Use the new rendering class")]
        public void Undraw()
        {
            Console.SetCursorPosition(this.X, this.Y);
            Console.Write(' ');
        }

        public bool IsCollidingWith(Unit otherUnit)
        {
            if (this.X == otherUnit.X && this.Y == otherUnit.Y)
            {
                return true;
            }
            return false;
        }

    }
}
