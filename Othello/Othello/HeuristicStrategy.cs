using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello
{
    public class HeuristicStrategy : IStrategy
    {

        public HeuristicStrategy()
        {

        }

        public void MakeMove(int index)
        {
            
        }

        public List<(Board, int)> GenerateAllMoves(Board actualBoard)
        {
            var listOfBoards = new List<(Board, int)>(); //zwracamy planszę i pole na którym został wykonany ruch

            for (int ind = 1; ind <= 64; ind++)
            {
                var newBoard = new Board();
                newBoard.strategy = actualBoard.strategy;
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
