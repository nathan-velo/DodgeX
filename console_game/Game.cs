using System;
using System.Diagnostics;

namespace console_game
{
    class Game
    {
        public int WinWidth;
        public int WinHeight;
        private string DifficultyText;
        public Game(int winWidth, int winHeight, int Difficulty)
        {
            playerUnit = new PlayerUnit(5, (int)(Console.WindowHeight / 2 - 2), "@");
            Random = new Random();
            stopwatch = new Stopwatch();
            Score = 0;
            WinWidth = winWidth;
            WinHeight = winHeight;
            int enemyNum = 200;
            switch (Difficulty)
            {
                case 1: enemyNum = 125; DifficultyText = "Easy"; break;
                case 2: enemyNum = 200; DifficultyText = "Medium"; break;
                case 3: enemyNum = 300; DifficultyText = "Hard"; break;
                case 4: enemyNum = 500; DifficultyText = "Extreme"; break;
            }
            enemyUnits = new Unit[enemyNum];
        }
        public static Random Random;
        public static int Score;
        private Stopwatch stopwatch;
        private Unit playerUnit;
        private Unit[] enemyUnits;

        public void Run()
        {
            for (int i = 0; i < enemyUnits.Length; i++)
            {
                int row = Random.Next(0, WinHeight - 1);
                enemyUnits[i] = new EnemyUnit(WinWidth - 1, row, "X");
            }

            stopwatch.Start();
            long timeAtPreviousFrame = stopwatch.ElapsedMilliseconds;
            int desiredFPS = 144;
            int desiredFPSTiming = 1000 / desiredFPS;
            int frameTimingMS;
            while (true)
            {
                frameTimingMS = (int)(stopwatch.ElapsedMilliseconds - timeAtPreviousFrame);
                timeAtPreviousFrame = stopwatch.ElapsedMilliseconds;

                playerUnit.Update(frameTimingMS);

                foreach (Unit enemyUnit in enemyUnits)
                {
                    enemyUnit.Update(frameTimingMS);

                    if (playerUnit.IsCollidingWith(enemyUnit))
                    {
                        GameOver();
                        return;
                    }
                }

                Frame_Buffer.AddToRender(playerUnit.X, playerUnit.Y, playerUnit.UnitGraphic);
                foreach (EnemyUnit enemyUnit in enemyUnits)
                {
                    if (enemyUnit.SleepForMS <= 0)
                    {
                        Frame_Buffer.AddToRender(enemyUnit.X, enemyUnit.Y, enemyUnit.UnitGraphic);
                    }
                }
                Frame_Buffer.AddToRender(0, WinHeight - 1, "SCORE: " + Score);
                Frame_Buffer.AddToRender(0, WinHeight - 1, "Difficulty: " + DifficultyText, "right");
                Frame_Buffer.PrintFrame();

                if (desiredFPSTiming - frameTimingMS > 0)
                {
                    System.Threading.Thread.Sleep(desiredFPSTiming - frameTimingMS);
                }
            }
        }
        void GameOver()
        {
            //Menu.Scores.CheckScores(Score,Difficulty);
            Frame_Buffer.AddToRender(0, WinHeight / 2, "Final score " + Score, "middle");
            Frame_Buffer.AddToRender(0, WinHeight / 2 - 1, "Difficulty: " + DifficultyText, "middle");
            Frame_Buffer.AddToRender(0, WinHeight / 2 - 2, "Game Over!", "middle");
            Frame_Buffer.AddToRender(0, WinHeight / 3, "Press Enter to play again", "middle");
            Frame_Buffer.AddToRender(0, WinHeight * 2 / 3 - 2, "Press Escape to exit game", "middle");
            Frame_Buffer.PrintFrame();
            Score = 0;
            Console.ReadKey();
        }
    }
}
