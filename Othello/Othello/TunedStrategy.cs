using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello
{
    class TunedStrategy : UTCStrategy
    {
        public TunedStrategy(int player, double cp, int budget, bool discreteRewardPolicy, int randomSeed = 123): base(player, cp, budget, discreteRewardPolicy, randomSeed)
        {

        }

        protected override GameNode BestChild(GameNode v, double cp)
        {
            if (v.Children.Count == 0) return v;
            int argMax = 0;
            double valMax = double.MinValue;
            int nodeN = v.state.SimulationsCounter;
            for (int i = 0; i < v.Children.Count; ++i)
            {
                var c = v.Children[i].state.CalculateUCTBTuned(nodeN, this.iterationCount);
                if (valMax < c)
                {
                    argMax = i;
                    valMax = c;
                }
                else if (valMax == c && random.NextDouble() > 0.5)
                {
                    argMax = i;
                    valMax = c;
                }
            }
            return v.Children.ElementAt(argMax);
        }

    }
}
