using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello
{
    public class UTCStrategy : IStrategy
    {
        protected Random random;
        double cp;
        int budget;
        protected int iterationCount;
        bool discreteRewardPolicy;
        int player;
        public UTCStrategy(int player, double cp, int budget, bool discreteRewardPolicy, int randomSeed = 123)
        {
            this.cp = cp;
            this.budget = budget;
            this.iterationCount = 0;
            this.discreteRewardPolicy = discreteRewardPolicy;
            this.player = player;
            random = new Random(randomSeed);

        }
        public List<(Board, int)> GenerateAllMoves(Board actualBoard)
        {
            throw new NotImplementedException();
        }



        public int GetNextMove(Board board)
        {
            return SearchUTC(board);
        }

        private int SearchUTC(Board curentBoard)
        {
            var initialState = new NodeState(0, 0, curentBoard, false);
            var root = new GameNode(initialState, -1, null);
            if (!root.AreAnyMovesPossible())
            {
                return -1;
            }
            this.iterationCount = 0;
            while(this.iterationCount < budget)
            {
                var vl = TreePolicy(root);
                var delta = DefaultPolicy(vl);
                BackPropagation(vl, delta);
                this.iterationCount++;
            }

            return BestChild(root, 0).ActionIndex;
        }

        private GameNode TreePolicy(GameNode v)
        {
            while (v != null && !v.IsTerminal())
            {
                if(!v.IsFullyExpanded())
                {
                    return Expand(v);
                }
                else
                {
                    v = BestChild(v, cp);
                }
            }
            return v;
        }

        private GameNode Expand(GameNode v)
        {
            var randomIndex = random.Next(0, v.GetUntiredActionsCount());
            return v.ChooseUntriedAction(randomIndex);
        }

        protected virtual GameNode BestChild(GameNode v, double cp)
        {
            int argMax = 0;
            double valMax = double.MinValue;
            int nodeN = v.state.SimulationsCounter;
            for (int i = 0; i < v.Children.Count; ++i)
            {
                var c = v.Children[i].state.CalculateUCTValue(cp, nodeN);
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

        double DefaultPolicy(GameNode v)
        {
            Board board = v.state.Board.CopyBoard();
            List<(Board, int)> possibleActions;
            int randomIndex;
            while (!board.IsBoardEnd())
            {
                // my move
                board.player = this.player;
                possibleActions = board.GetAllPossibleMoves();
                if(possibleActions.Count > 0)
                {
                    randomIndex = random.Next(0, possibleActions.Count);
                    board = possibleActions.ElementAt(randomIndex).Item1;
                }
                
                // opponents move
                board.player = this.player * (-1);
                possibleActions = board.GetAllPossibleMoves();
                if (possibleActions.Count > 0)
                {
                    randomIndex = random.Next(0, possibleActions.Count);
                    board = possibleActions.ElementAt(randomIndex).Item1;
                }
            }
            board.player = this.player;
            return this.discreteRewardPolicy ? board.CountRewardDiscrete(): board.CountRewardDifference();
        }

        void BackPropagation(GameNode v, double reward)
        {
            while(v != null)
            {
                v.UpdateCounterAndReward(reward);
                v = v.Parent;
            }
        }

        

       


    }
}
