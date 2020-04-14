using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace console_game
{
    class EnemyUnit : Unit
    {
        public int Width, Height;
        public EnemyUnit(int x, int y, string unitGraphic) : base(x, y, unitGraphic)
        {
            SleepForMS = Game.Random.Next(0, 10000);
            Width = x;
            Height = y;
        }

        public int TimeBetweenMoves = Game.Random.Next(80, 175);
        private int timeSinceLastMove = 0;

        public int SleepForMS { get; private set; }

        public override void Update(int frameTimingMS)
        {

            SleepForMS -= frameTimingMS;
            if (SleepForMS > 0)
            {
                return;
            }
            timeSinceLastMove += frameTimingMS;

            if (timeSinceLastMove < TimeBetweenMoves)
            {
                return;
            }
            timeSinceLastMove -= TimeBetweenMoves;

            if (X > 0)
            {
                X = X - 1;
            }
            else
            {
                X = Width;
                Y = Game.Random.Next(0, Height);

                SleepForMS = Game.Random.Next(0, 1500);
                if (TimeBetweenMoves > 30)
                {
                    TimeBetweenMoves = (int)(TimeBetweenMoves * 0.9);
                }
                Game.Score += 1;
            }
            base.Update(frameTimingMS);
        }
        [Obsolete("Use the new rendering class")]
        public override void Draw()
        {
            if (SleepForMS > 0)
            {
                return;
            }
            base.Draw();
        }
    }
}
