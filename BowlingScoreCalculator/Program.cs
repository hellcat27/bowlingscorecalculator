using System;

namespace BowlingScoreCalculator
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("---Bowling Calculator---");
            Game Game = new Game();
            Game.RunGame();
        }
    }
}
