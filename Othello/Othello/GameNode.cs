using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello
{
    public class GameNode
    {
        private List<(Board, int)> allUntriedActions;
        public List<GameNode> Children { get; }
        public NodeState state;
        public int ActionIndex { get; } //Number of field which was used by parent node to create this node
        public GameNode Parent { get; }

        public GameNode(NodeState state, int actionIndex, GameNode parent)
        {
            Children = new List<GameNode>();
            this.state = state;
            this.ActionIndex = actionIndex;
            allUntriedActions = state.Board.GetAllPossibleMoves();
            Parent = parent;

        }

        public bool AreAnyMovesPossible()
        {
            return allUntriedActions != null && allUntriedActions.Count > 0;
        }

        public bool IsFullyExpanded()
        {
            return allUntriedActions.Count == 0;
        }

        public int GetUntiredActionsCount()
        {
            return allUntriedActions.Count;
        }

        public GameNode ChooseUntriedAction(int actionIndex)
        {
            (Board actionBoard, int actionField) = allUntriedActions.ElementAt(actionIndex);
            GameNode child = new GameNode(new NodeState(0, 0, actionBoard, actionBoard.IsBoardEnd()), actionField, this);
            Children.Add(child);
            allUntriedActions.RemoveAt(actionIndex);
            return child;
        }

        //public GameNode ChooseAction(int actionIndex)
        //{
        //    return Children.ElementAt(actionIndex);
        //}

        public bool IsTerminal() // node is terminal when the state is terminal or there is no possible actions
        {
            return state.IsTerminal || (allUntriedActions.Count == 0 && Children.Count == 0);
        }

        public double GetReward()
        {
            return state.SimulationsReward;
        }

        public void UpdateCounterAndReward(double reward)
        {
            state.SimulationsCounter += 1;
            state.SimulationsReward += reward;
            state.RewardList.Add(reward);
        }

    }
}
