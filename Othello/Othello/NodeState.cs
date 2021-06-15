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
        public List<double> RewardList { get; set; }
        public Board Board { get; }
        public bool IsTerminal { get; set; }
        public NodeState(int simulationsCount, double simulationsReward, Board b, bool isTerminal)
        {
            this.SimulationsCounter = simulationsCount;
            this.SimulationsReward = simulationsReward;
            this.Board = b;
            IsTerminal = isTerminal;
            RewardList = new List<double>();
        }

        public double CalculateUCTValue(double cp, int nParent)
        {
            double Q = SimulationsReward;
            double N = SimulationsCounter;

            double X = Q / N;
            double C = 2 * cp * Math.Sqrt(2 * Math.Log(nParent) / N);

            return X + C;
        }

        public double CalculateUCTBTuned(int nParent, int t)
        {
            double Q = SimulationsReward;
            double nj = SimulationsCounter;

            double X_mean = Q / nj;
            double vj = 0.0;
            for(int i = 0; i < RewardList.Count; ++i)
            {
                vj += Math.Pow(RewardList[i], 2);
            }
            vj = 0.5 * vj - (Q / nj) + Math.Sqrt(2 * Math.Log(t) / nj);

            double upperConfidenceBound = Math.Sqrt(Math.Log(nParent) * Math.Min(0.25, vj)  / nj);

            return X_mean + upperConfidenceBound;
        }
    }
}
