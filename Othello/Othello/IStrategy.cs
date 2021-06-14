using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello
{
    public interface IStrategy
    {
        int GetNextMove(Board board);
        List<(Board, int)> GenerateAllMoves(Board actualBoard);
    }
}
