using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello
{
    public class Board
    {
        public int[,] board;  //-1 White, 0 Empty, 1 Black
        public int blackCount;
        public int whiteCount;
        public int player = 1; // Black starts
        public Strategy strategy;
        public Board()
        {
            board = new int[8, 8];

            //setup initial board state
            board[3, 3] = 1;
            board[4, 3] = -1;
            board[3, 4] = -1;
            board[4, 4] = 1;
            blackCount = 2;
            whiteCount = 2;
        }

        public bool IsBoardFill()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j] == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        private bool IsAnyMovePossibleForField(int fieldIndex) //sprawdza czy jest możliwy jakikolwiek ruch dla danego pola
        {
            int buttonIndex = fieldIndex;
            int row = (buttonIndex - 1) / 8;
            int col = buttonIndex - (row * 8) - 1;

            if (row - 1 >= 0 && board[row - 1, col] == (player * (-1))) //up
            {
                int r = row - 1;
                bool canReverse = false;
                while (r >= 0)
                {
                    if (board[r, col] == player)
                    {
                        canReverse = true;
                        break;
                    }
                    else
                    {
                        r--;
                    }
                }
                if (r >= 0 && canReverse)
                {
                    return true;
                }
            }

            if (col - 1 >= 0 && board[row, col - 1] == player * (-1)) //left
            {
                int c = col - 1;
                bool canReverse = false;
                while (c >= 0)
                {
                    if (board[row, c] == player)
                    {
                        canReverse = true;
                        break;
                    }
                    else
                    {
                        c--;
                    }
                }
                if (c >= 0 && canReverse)
                {
                    return true;
                }
            }

            if (row + 1 <= 7 && board[row + 1, col] == (player * (-1))) //down
            {
                int r = row + 1;
                bool canReverse = false;
                while (r <= 7)
                {
                    if (board[r, col] == player)
                    {
                        canReverse = true;
                        break;
                    }
                    else
                    {
                        r++;
                    }
                }
                if (r <= 7 && canReverse)
                {
                    return true;
                }
            }

            if (col + 1 <= 7 && board[row, col + 1] == player * (-1)) //right
            {
                int c = col + 1;
                bool canReverse = false;
                while (c <= 7)
                {
                    if (board[row, c] == player)
                    {
                        canReverse = true;
                        break;
                    }
                    else
                    {
                        c++;
                    }
                }
                if (c <= 7 && canReverse)
                {
                    return true;
                }
            }

            if (row - 1 >= 0 && col - 1 >= 0 && board[row - 1, col - 1] == player * (-1)) //left-up
            {
                int c = col - 1;
                int r = row - 1;
                bool canReverse = false;
                while (c >= 0 && r >= 0)
                {
                    if (board[r, c] == player)
                    {
                        canReverse = true;
                        break;
                    }
                    else
                    {
                        c--;
                        r--;
                    }
                }
                if (c >= 0 && r >= 0 && canReverse)
                {
                    return true;
                }
            }
            if (row + 1 <= 7 && col + 1 <= 7 && board[row + 1, col + 1] == player * (-1)) //right-down
            {
                int c = col + 1;
                int r = row + 1;
                bool canReverse = false;
                while (c <= 7 && r <= 7)
                {
                    if (board[r, c] == player)
                    {
                        canReverse = true;
                        break;
                    }
                    else
                    {
                        c++;
                        r++;
                    }
                }
                if (c <= 7 && r <= 7 && canReverse)
                {
                    return true;
                }
            }

            if (row + 1 <= 7 && col - 1 >= 0 && board[row + 1, col - 1] == player * (-1)) //left-down
            {
                int c = col - 1;
                int r = row + 1;
                bool canReverse = false;
                while (c >= 0 && r <= 7)
                {
                    if (board[r, c] == player)
                    {
                        canReverse = true;
                        break;
                    }
                    else
                    {
                        c--;
                        r++;
                    }
                }
                if (c >= 0 && r <= 7 && canReverse)
                {
                    return true;
                }
            }

            if (row - 1 >= 0 && col + 1 <= 7 && board[row - 1, col + 1] == player * (-1)) //right-up
            {
                int c = col + 1;
                int r = row - 1;
                bool canReverse = false;
                while (r >= 0 && c <= 7)
                {
                    if (board[r, c] == player)
                    {
                        canReverse = true;
                        break;
                    }
                    else
                    {
                        c++;
                        r--;
                    }
                }
                if (r >= 0 && c <= 7 && canReverse)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
