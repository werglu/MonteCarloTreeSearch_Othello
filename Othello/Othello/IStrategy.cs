using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello
{
    public interface IStrategy
    {
        void MakeMove(int index);
        List<(Board, int)> GenerateAllMoves(Board actualBoard);
    }
}
