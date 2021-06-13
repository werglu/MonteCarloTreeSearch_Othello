using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello
{
    class UTCStrategy : IStrategy
    {
        Random random;
        double cp;
        int budget;
        public UTCStrategy(double cp, int budget, int randomSeed = 123)
        {
            this.cp = cp;
            this.budget = budget;
            random = new Random(randomSeed);

        }
        public List<(Board, int)> GenerateAllMoves(Board actualBoard)
        {
            throw new NotImplementedException();
        }

        public void MakeMove(int index)
        {


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
            int budgetLeft = budget;
            while(budgetLeft > 0)
            {
                var vl = TreePolicy(root);
                var delta = DefaultPolicy(vl);
                BackPropagation(vl, delta);
                budgetLeft--;
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

        private GameNode BestChild(GameNode v, double cp)
        {
            if (v.Children.Count == 0) return v;
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
            Board board = v.state.Board;
            List<(Board, int)> possibleActions;
            int randomIndex;
            while (!board.IsBoardEnd())
            {
                // my move
                board.player = -1;
                possibleActions = board.GetAllPossibleMoves();
                if(possibleActions.Count > 0)
                {
                    randomIndex = random.Next(0, possibleActions.Count);
                    board = possibleActions.ElementAt(randomIndex).Item1;
                }
                
                // opponents move
                board.player = 1;
                possibleActions = board.GetAllPossibleMoves();
                if (possibleActions.Count > 0)
                {
                    randomIndex = random.Next(0, possibleActions.Count);
                    board = possibleActions.ElementAt(randomIndex).Item1;
                }
            }
            return board.CountRewardDiscrete();
        }

        void BackPropagation(GameNode v, double reward)
        {
            while(v != null)
            {
                v.UpdateCounter();
                v.UpdateReward(reward);
                v = v.Parent;
            }
        }

        

       


    }
}
