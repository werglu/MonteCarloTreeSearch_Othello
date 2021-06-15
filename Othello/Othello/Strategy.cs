using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello
{
    public enum Strategy
    {
        Heuristic,
        BasicUCT, 
        UCB1_Tuned,
        DiffereceReward_BasicUCT,
        DiffereceReward_UCB1_Tuned
    }
}
