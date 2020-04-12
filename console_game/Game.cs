using System;
using System.Diagnostics;

namespace ConsoleApp1
{
    class Game
    {
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

        public static int WinWidth;
        public static int WinHeight { private set; get; }
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
            int desiredFPS = 60;
            int desiredFPSTiming = 1000 / desiredFPS;
            while (true)
            {
                int frameTimingMS = (int)(stopwatch.ElapsedMilliseconds - timeAtPreviousFrame);
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

                Menu.GameRender.AddToRender(playerUnit.X, playerUnit.Y, playerUnit.UnitGraphic);
                foreach (EnemyUnit enemyUnit in enemyUnits)
                {
                    if (enemyUnit.SleepForMS <= 0)
                    {
                        Menu.GameRender.AddToRender(enemyUnit.X, enemyUnit.Y, enemyUnit.UnitGraphic);
                    }
                }
                Menu.GameRender.AddToRender(0, WinHeight - 1, "SCORE: " + Score);
                Menu.GameRender.AddToRender(0, WinHeight - 1, "Difficulty: " + DifficultyText, "right");
                Menu.GameRender.PrintFrame();

                if (desiredFPSTiming - frameTimingMS > 0)
                {
                    System.Threading.Thread.Sleep(desiredFPSTiming - frameTimingMS);
                }
            }
        }
        void GameOver()
        {
            //Menu.Scores.CheckScores(Score,Difficulty);
            Menu.GameRender.AddToRender(0, WinHeight / 2, "Final score " + Score, "middle");
            Menu.GameRender.AddToRender(0, WinHeight / 2 - 1, "Difficulty: " + DifficultyText, "middle");
            Menu.GameRender.AddToRender(0, WinHeight / 2 - 2, "Game Over!", "middle");
            Menu.GameRender.AddToRender(0, WinHeight / 3, "Press Enter to play again", "middle");
            Menu.GameRender.AddToRender(0, WinHeight * 2 / 3 - 2, "Press Escape to exit game", "middle");
            Menu.GameRender.PrintFrame();
        }
    }
}
