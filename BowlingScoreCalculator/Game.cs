using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingScoreCalculator
{
    class Game
    {
        private int frame, shot, pinsLeft, score, shotNumber, evalNumber, tempShot, tempFrame;
        private bool bonus = false;
        private int[] shotArray = new int[21];
        private int[] frameScore = new int[10];

        public Game()
        {
            frame = 1;
            shot = 1;
            pinsLeft = 10;
            score = 0;
            shotNumber = 0;
            evalNumber = 0;
            tempShot = 1;
            tempFrame = 1;
        }

        private void AddToScore(int value, int shotNumber)
        {
            if (value > pinsLeft)
            {
                value = pinsLeft;
            }
            else if (value < 0)
            {
                value = 0;
            }
            shotArray[shotNumber] = value;
            pinsLeft -= value;
        }

        public void GetGameStatus()
        {
            Console.WriteLine(String.Format("Frame: {0}\nShot: {1}\nPins Left: {2}\nScore: {3}\n", frame, shot, pinsLeft, score));
        }

        public void EndGame()
        {
            int n = 1;
            Console.WriteLine("---Total score by frame---");
            foreach(var score in frameScore)
            {
                Console.Write("Frame " + n + ": " + score.ToString() + "\n");
                n++;
            }
            Console.WriteLine();
            Console.WriteLine("Final score: " + score);
            Console.ReadLine();
        }

        public void RunGame()
        {
            while (frame < 11)
            {
                NextShot();
            }

            EndGame();
        }

        private void BowlAction()
        {
            GetGameStatus();
            Console.Write("Pins knocked down: ");
            bool isInt = false;
            int val;
            while (!isInt)
            {
                isInt = Int32.TryParse(Console.ReadLine(), out val);
                if (isInt)
                {
                    AddToScore(val, shotNumber);
                }
                else
                {
                    Console.WriteLine("Enter a valid number: ");
                }
            }
            shot++;
            shotNumber++;
            Console.WriteLine();
        }

        private void NextShot()
        {
            
            if (shot < 3 && frame != 10 && pinsLeft > 0)
            {
                BowlAction();
            }
            else if (shot < 4 && frame == 10 && pinsLeft == 0)
            {
                pinsLeft = 10;
                bonus = true;
                BowlAction();
            }
            else if (shot < 3 && frame == 10 && pinsLeft != 0)
            {
                BowlAction();
            }
            else if(shot < 4 && frame == 10 && pinsLeft != 0 && bonus == true)
            {
                BowlAction();
            }
            else
            {
                frame++;
                shot = 1;
                pinsLeft = 10;
                Console.WriteLine();
            }
            EvaluateGame();
        }

        public void EvaluateGame()
        {
            tempShot = shot;
            tempFrame = frame;
            frame = 1;
            shot = 1;
            evalNumber = 0;
            bonus = false;
            score = 0;
            while(evalNumber < shotNumber)
            {
                EvaluateShot(shotArray[evalNumber]);
                evalNumber++;
            }
            shot = tempShot;
            frame = tempFrame;
        }

        private void EvaluateShot(int value)
        {
            if (shot == 1 && value == 10 && bonus == false)
            {
                Strike();
            }
            else if(shot == 2 && (shotArray[evalNumber] + shotArray[evalNumber - 1] == 10) && bonus == false)
            {
                Spare();
            }
            else if(shot == 2 && (shotArray[evalNumber] + shotArray[evalNumber - 1] != 10) && bonus == false)
            {
                Open();
            }
            else if(bonus == true)
            {
                BonusShot();
                shot++;
            }
            else
            {
                shot++;
            }

        }

        private void BonusShot()
        {
            score += shotArray[evalNumber];
            frameScore[frame - 1] = score;
        }

        private void Open()
        {
            score += (shotArray[evalNumber] + shotArray[evalNumber - 1]);
            frameScore[frame - 1] = score;
            frame++;
            shot = 1;
        }

        private void Spare()
        {
            if (frame != 10)
            {
                score += (10 + shotArray[evalNumber + 1]);
                shot = 1;
                frameScore[frame - 1] = score;
                frame++;
            }
            else
            {
                score += 10;
                bonus = true;
            }
        }

        private void Strike()
        {
            if (frame != 10)
            {
                score += (10 + shotArray[evalNumber + 1] + shotArray[evalNumber + 2]);
                shot = 1;
                frameScore[frame - 1] = score;
                frame++;
            }
            else
            {
                score += 10;
                bonus = true;
            }
        }
    }

}
