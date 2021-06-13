using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello
{
    class NodeState
    {
        public int SimulationsCounter { get; set; }
        public double SimulationsReward { get; set; }
        public Board Board { get; }
        public bool IsTerminal { get; set; }
        public NodeState(int simulationsCount, double simulationsReward, Board b, bool isTerminal)
        {
            this.SimulationsCounter = simulationsCount;
            this.SimulationsReward = simulationsReward;
            this.Board = b;
            IsTerminal = isTerminal;
        }

        public double CalculateUCTValue(double cp, int nParent)
        {
            double Q = SimulationsCounter;
            double N = SimulationsReward;

            double X = Q / N;
            double C = 2 * cp * Math.Sqrt(2 * Math.Log(nParent) / N);

            return X + C;
        }
    }
}
