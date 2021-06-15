using System;
using System.Collections.Generic;
using System.Text;
using Othello;

namespace OthelloGames
{
    class Game
    {
        IStrategy playerOne;
        IStrategy playerTwo;
        Strategy strategyOne;
        Strategy strategyTwo;
        Random random;
        public Game(Strategy strategyOne, Strategy strategyTwo, int budget, double cp, int randomSeed)
        {
            random = new Random(randomSeed);
            this.strategyOne = strategyOne;
            this.strategyTwo = strategyTwo;

            switch(strategyOne) // player one is black
            {
                case Strategy.BasicUCT:
                    playerOne = new UTCStrategy(1, cp, budget, true, randomSeed);
                    break;
                case Strategy.DiffereceReward_BasicUCT:
                    playerOne = new UTCStrategy(1, cp, budget, false, randomSeed);
                    break;
                case Strategy.UCB1_Tuned:
                    playerOne = new TunedStrategy(1, cp, budget, true, randomSeed);
                    break;
                case Strategy.DiffereceReward_UCB1_Tuned:
                    playerOne = new TunedStrategy(1, cp, budget, false, randomSeed);
                    break;
                case Strategy.Heuristic:
                    playerOne = new HeuristicStrategy();
                    break;
            }

            switch (strategyTwo) // player two is white
            {
                case Strategy.BasicUCT:
                    playerTwo = new UTCStrategy(-1, cp, budget, true, randomSeed);
                    break;
                case Strategy.DiffereceReward_BasicUCT:
                    playerTwo = new UTCStrategy(-1, cp, budget, false, randomSeed);
                    break;
                case Strategy.UCB1_Tuned:
                    playerTwo = new TunedStrategy(-1, cp, budget, true, randomSeed);
                    break;
                case Strategy.DiffereceReward_UCB1_Tuned:
                    playerTwo = new TunedStrategy(-1, cp, budget, false, randomSeed);
                    break;
                case Strategy.Heuristic:
                    playerTwo = new HeuristicStrategy();
                    break;
            }

        }

        public (int scorePlayerOne, int scorePlayerTwo) PlayGame(bool showPlay = false)
        {
            Board othelloBoard = new Board();
            bool stopGame = false;
            while(!stopGame)
            {
                // player one move
                int moveOfPlayerOne = playerOne.GetNextMove(othelloBoard);
                if(moveOfPlayerOne >= 0)
                {
                    othelloBoard.MakeMoveIfValid(othelloBoard, moveOfPlayerOne);
                    othelloBoard.blackCount++;
                }
                othelloBoard.player *= (-1);

                // player two move
                int moveOfPlayerTwo = playerTwo.GetNextMove(othelloBoard);
                if (moveOfPlayerTwo >= 0)
                {
                    othelloBoard.MakeMoveIfValid(othelloBoard, moveOfPlayerTwo);
                    othelloBoard.whiteCount++;
                }
                othelloBoard.player *= (-1);

                if(showPlay)
                {
                    ShowBoard(othelloBoard);
                }
                stopGame = othelloBoard.IsBoardEnd() || (moveOfPlayerOne < 0 && moveOfPlayerTwo < 0);
            }

            return (othelloBoard.blackCount, othelloBoard.whiteCount);
        }

        private void ShowBoard(Board board)
        {
            Console.WriteLine("Board {0} - {1} ", board.blackCount, board.whiteCount);
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board.board[i, j] == 0)
                        Console.Write("  ");
                    else if(board.board[i, j] == 1)
                        Console.Write("B ");
                    else
                        Console.Write("W ");
                }
                Console.WriteLine();
            }
        }

        public void PlayGameWithSpeaker(bool showBoard = false)
        {
            (int scorePlayerOne, int scorePlayerTwo) = PlayGame(showBoard);
            
            if (scorePlayerOne > scorePlayerTwo)
            {
                Console.WriteLine("The winner is {0}", strategyOne.ToString());
            }
            else if(scorePlayerOne < scorePlayerTwo)
            {
                Console.WriteLine("The winner is {0}", strategyTwo.ToString());
            }
            else
            {
                Console.WriteLine("Draw");
            }
            Console.WriteLine("Points:");
            Console.WriteLine("{0} - {1}", scorePlayerOne, strategyOne.ToString());
            Console.WriteLine("{0} - {1}", scorePlayerTwo, strategyTwo.ToString());

        }

     

    }
}
