using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello
{
    public class HeuristicStrategy : IStrategy
    {
        Random rnd;
        public HeuristicStrategy()
        {
            rnd = new Random();
        }

        public int GetNextMove(Board board)
        {
            //jeśli jest możliwe położenie piona w rogu to wykonaj ten ruch

            //generujemy wszytskie możliwe ruchy i wybieramy najlepszy
            var listOfAllMoves = GenerateAllMoves(board);
            if (listOfAllMoves.Count() == 0) //komputer nie ma ruchu
            {
                return -1;
            }
            else
            {
                var bestWhiteCount = listOfAllMoves.Max(x => x.Item1.whiteCount);
                var best = listOfAllMoves.Where(x => x.Item1.whiteCount == bestWhiteCount); //lista najlepszych ruchów
                int bestIndex = rnd.Next(0, best.Count()); // losujemy ruch sposród najlepszych ruchów
                var bestMove = best.ElementAt(bestIndex);
                return bestMove.Item2;
            }
        }

        public List<(Board, int)> GenerateAllMoves(Board actualBoard)
        {
            var listOfBoards = new List<(Board, int)>(); //zwracamy planszę i pole na którym został wykonany ruch

            for (int ind = 1; ind <= 64; ind++)
            {
                var newBoard = new Board();
                //newBoard.strategy = actualBoard.strategy;
                newBoard.blackCount = actualBoard.blackCount;
                newBoard.whiteCount = actualBoard.whiteCount;
                newBoard.player = actualBoard.player;
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        newBoard.board[i, j] = actualBoard.board[i, j];
                    }
                }

               if (newBoard.MakeMoveIfValid(newBoard, ind))
               {
                    listOfBoards.Add((newBoard, ind));
               }
            }

            return listOfBoards;
        }
    }
}
