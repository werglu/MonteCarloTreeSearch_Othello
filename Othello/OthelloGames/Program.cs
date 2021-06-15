using System;
using Othello;

namespace OthelloGames
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello to Othello Games!");
            Game game = new Game(Strategy.BasicUCT, Strategy.UCB1_Tuned, 100, Math.Sqrt(2), 123);
            game.PlayGameWithSpeaker(false);

            game = new Game(Strategy.BasicUCT, Strategy.DiffereceReward_BasicUCT, 100, Math.Sqrt(2), 123);
            game.PlayGameWithSpeaker(false);

            game = new Game(Strategy.BasicUCT, Strategy.Heuristic, 100, Math.Sqrt(2), 123);
            game.PlayGameWithSpeaker(false);

            game = new Game(Strategy.BasicUCT, Strategy.DiffereceReward_UCB1_Tuned, 100, Math.Sqrt(2), 123);
            game.PlayGameWithSpeaker(false);
        }
    }
}
