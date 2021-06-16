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
        //public Strategy strategy;
        //public IStrategy solver;
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

            player = 1; // Black starts
        }

        public Board CopyBoard()
        {
            Board copyBoard = new Board
            {
                blackCount = this.blackCount,
                whiteCount = this.whiteCount,
                player = this.player
            };
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    copyBoard.board[i, j] = board[i, j];
                }
            }
            return copyBoard;
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

        public bool IsBoardEnd()
        {
            if (whiteCount + blackCount == 64)
                return true;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j] == 0 && IsAnyMovePossibleForField(i * 8 + j + 1)) //TODO 
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        public bool IsAnyMovePossibleForField(int fieldIndex) //sprawdza czy jest możliwy jakikolwiek ruch dla danego pola
        {
            int buttonIndex = fieldIndex;
            int row = (buttonIndex - 1) / 8;
            int col = buttonIndex - (row * 8) - 1;
            if (board[row, col] != 0) //pole jest już zajęte
            {
                return false;
            }
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
                    else if (board[r, col] == 0) //empty
                    {
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
                    else if (board[row, c] == 0) //empty
                    {
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
                    else if (board[r, col] == 0) //empty
                    {
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
                    else if (board[row, c] == 0) //empty
                    {
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
                    else if (board[r, c] == 0) //empty 
                    {
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
                    else if (board[r, c] == 0) //empty
                    {
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
                    else if (board[r, c] == 0) //empty
                    {
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
                    else if (board[r, c] == 0) //empty
                    {
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

        public bool MakeMoveIfValid(Board othelloBoard, int filedIndex) //sprawdza czy ruch jest ok, i jeśli tak to wykonuje go 
        {
            bool valid = false;
            int buttonIndex = filedIndex;
            int row = (buttonIndex - 1) / 8;
            int col = buttonIndex - (row * 8) - 1;
            if (othelloBoard.board[row, col] != 0) //pole jest już zajęte
            {
                return false;
            }

            if (row - 1 >= 0 && othelloBoard.board[row - 1, col] == (othelloBoard.player * (-1))) //up
            {
                int r = row - 1;
                bool canReverse = false;
                while (r >= 0)
                {
                    if (othelloBoard.board[r, col] == othelloBoard.player)
                    {
                        canReverse = true;
                        break;
                    }
                    else if (board[r, col] == 0) //empty
                    {
                        break;
                    }
                    else
                    {
                        r--;
                    }
                }
                if (r >= 0 && canReverse)
                {
                    while (r + 1 <= row - 1)
                    {
                        for (int ind=1; ind<=64;ind++)
                        {
                            if (ind == ((r + 1) * 8 + col + 1))
                            {
                                //btn.BackColor = othelloBoard.player == 1 ? Color.Black : Color.White;

                                othelloBoard.board[(ind - 1) / 8, col] = othelloBoard.player;
                                if (othelloBoard.player == 1)
                                {
                                    othelloBoard.blackCount++;
                                    othelloBoard.whiteCount--;
                                }
                                else
                                {
                                    othelloBoard.whiteCount++;
                                    othelloBoard.blackCount--;
                                }
                            }

                        }

                        r++;
                    }
                    valid = true;
                }
            }

            if (col - 1 >= 0 && othelloBoard.board[row, col - 1] == othelloBoard.player * (-1)) //left
            {
                int c = col - 1;
                bool canReverse = false;
                while (c >= 0)
                {
                    if (othelloBoard.board[row, c] == othelloBoard.player)
                    {
                        canReverse = true;
                        break;
                    }
                    else if (othelloBoard.board[row, c] == 0) //empty
                    {
                        break;
                    }
                    else
                    {
                        c--;
                    }
                }
                if (c >= 0 && canReverse)
                {
                    for (int ind = 1; ind <= 64; ind++)
                    {
                        if (ind >= (row * 8 + c + 2) && ind <= (row * 8 + col - 1 + 1))
                        {
                           // btn.BackColor = othelloBoard.player == 1 ? Color.Black : Color.White;

                            othelloBoard.board[row, ind - ((ind - 1) / 8 * 8) - 1] = othelloBoard.player;
                            if (othelloBoard.player == 1)
                            {
                                othelloBoard.blackCount++;
                                othelloBoard.whiteCount--;
                            }
                            else
                            {
                                othelloBoard.whiteCount++;
                                othelloBoard.blackCount--;
                            }
                        }

                    }
                    valid = true;
                }
            }

            if (row + 1 <= 7 && othelloBoard.board[row + 1, col] == (othelloBoard.player * (-1))) //down
            {
                int r = row + 1;
                bool canReverse = false;
                while (r <= 7)
                {
                    if (othelloBoard.board[r, col] == othelloBoard.player)
                    {
                        canReverse = true;
                        break;
                    }
                    else if (board[r, col] == 0) //empty
                    {
                        break;
                    }
                    else
                    {
                        r++;
                    }
                }
                if (r <= 7 && canReverse)
                {
                    while (r - 1 >= row + 1)
                    {
                        for (int ind = 1; ind <= 64; ind++)
                        {
                            if (ind == ((r - 1) * 8 + col + 1))
                            {
                               // btn.BackColor = othelloBoard.player == 1 ? Color.Black : Color.White;

                                othelloBoard.board[(ind - 1) / 8, col] = othelloBoard.player;
                                if (othelloBoard.player == 1)
                                {
                                    othelloBoard.blackCount++;
                                    othelloBoard.whiteCount--;
                                }
                                else
                                {
                                    othelloBoard.whiteCount++;
                                    othelloBoard.blackCount--;
                                }
                            }

                        }
                        r--;
                    }
                    valid = true;
                }
            }

            if (col + 1 <= 7 && othelloBoard.board[row, col + 1] == othelloBoard.player * (-1)) //right
            {
                int c = col + 1;
                bool canReverse = false;
                while (c <= 7)
                {
                    if (othelloBoard.board[row, c] == othelloBoard.player)
                    {
                        canReverse = true;
                        break;
                    }
                    else if (othelloBoard.board[row, c] == 0) //empty
                    {
                        break;
                    }
                    else
                    {
                        c++;
                    }
                }
                if (c <= 7 && canReverse)
                {
                    while (c - 1 >= col + 1)
                    {
                        for (int ind = 1; ind <= 64; ind++)
                        {
                            if (ind == (row * 8 + c - 1 + 1))
                            {
                                //btn.BackColor = othelloBoard.player == 1 ? Color.Black : Color.White;

                                othelloBoard.board[row, ind - ((ind - 1) / 8 * 8) - 1] = othelloBoard.player;
                                if (othelloBoard.player == 1)
                                {
                                    othelloBoard.blackCount++;
                                    othelloBoard.whiteCount--;
                                }
                                else
                                {
                                    othelloBoard.whiteCount++;
                                    othelloBoard.blackCount--;
                                }
                            }

                        }
                        c--;
                    }
                    valid = true;
                }
            }

            if (row - 1 >= 0 && col - 1 >= 0 && othelloBoard.board[row - 1, col - 1] == othelloBoard.player * (-1)) //left-up
            {
                int c = col - 1;
                int r = row - 1;
                bool canReverse = false;
                while (c >= 0 && r >= 0)
                {
                    if (othelloBoard.board[r, c] == othelloBoard.player)
                    {
                        canReverse = true;
                        break;
                    }
                    else if (board[r, c] == 0) //empty
                    {
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
                    while (c + 1 <= col - 1 && r + 1 <= row - 1)
                    {
                        for (int ind = 1; ind <= 64; ind++)
                        {
                            if (ind == ((r + 1) * 8 + c + 1 + 1))
                            {
                                //btn.BackColor = othelloBoard.player == 1 ? Color.Black : Color.White;

                                othelloBoard.board[(ind - 1) / 8, ind - ((ind - 1) / 8 * 8) - 1] = othelloBoard.player;
                                if (othelloBoard.player == 1)
                                {
                                    othelloBoard.blackCount++;
                                    othelloBoard.whiteCount--;
                                }
                                else
                                {
                                    othelloBoard.whiteCount++;
                                    othelloBoard.blackCount--;
                                }
                            }
                        }
                        c++;
                        r++;
                    }
                    valid = true;
                }
            }
            if (row + 1 <= 7 && col + 1 <= 7 && othelloBoard.board[row + 1, col + 1] == othelloBoard.player * (-1)) //right-down
            {
                int c = col + 1;
                int r = row + 1;
                bool canReverse = false;
                while (c <= 7 && r <= 7)
                {
                    if (othelloBoard.board[r, c] == othelloBoard.player)
                    {
                        canReverse = true;
                        break;
                    }
                    else if (board[r, c] == 0) //empty
                    {
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
                    while (c - 1 >= col + 1 && r - 1 >= row + 1)
                    {
                        for (int ind = 1; ind <= 64; ind++)
                        {
                            if (ind == ((r - 1) * 8 + c - 1 + 1))
                            {
                                //btn.BackColor = othelloBoard.player == 1 ? Color.Black : Color.White;

                                othelloBoard.board[(ind - 1) / 8, ind - ((ind - 1) / 8 * 8) - 1] = othelloBoard.player;
                                if (othelloBoard.player == 1)
                                {
                                    othelloBoard.blackCount++;
                                    othelloBoard.whiteCount--;
                                }
                                else
                                {
                                    othelloBoard.whiteCount++;
                                    othelloBoard.blackCount--;
                                }
                            }
                        }
                        c--;
                        r--;
                    }
                    valid = true;
                }
            }

            if (row + 1 <= 7 && col - 1 >= 0 && othelloBoard.board[row + 1, col - 1] == othelloBoard.player * (-1)) //left-down
            {
                int c = col - 1;
                int r = row + 1;
                bool canReverse = false;
                while (c >= 0 && r <= 7)
                {
                    if (othelloBoard.board[r, c] == othelloBoard.player)
                    {
                        canReverse = true;
                        break;
                    }
                    else if (board[r, c] == 0) //empty
                    {
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
                    while (c + 1 <= col - 1 && r - 1 >= row + 1)
                    {
                        for (int ind = 1; ind <= 64; ind++)
                        {
                            if (ind == ((r - 1) * 8 + c + 1 + 1))
                            {
                               // btn.BackColor = othelloBoard.player == 1 ? Color.Black : Color.White;

                                othelloBoard.board[(ind - 1) / 8, ind - ((ind - 1) / 8 * 8) - 1] = othelloBoard.player;
                                if (othelloBoard.player == 1)
                                {
                                    othelloBoard.blackCount++;
                                    othelloBoard.whiteCount--;
                                }
                                else
                                {
                                    othelloBoard.whiteCount++;
                                    othelloBoard.blackCount--;
                                }
                            }
                        }
                        c++;
                        r--;
                    }
                    valid = true;
                }
            }

            if (row - 1 >= 0 && col + 1 <= 7 && othelloBoard.board[row - 1, col + 1] == othelloBoard.player * (-1)) //right-up
            {
                int c = col + 1;
                int r = row - 1;
                bool canReverse = false;
                while (r >= 0 && c <= 7)
                {
                    if (othelloBoard.board[r, c] == othelloBoard.player)
                    {
                        canReverse = true;
                        break;
                    }
                    else if (board[r, c] == 0) //empty
                    {
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
                    while (c - 1 >= col + 1 && r + 1 <= row - 1)
                    {
                        for (int ind = 1; ind <= 64; ind++)
                        {
                            if (ind == ((r + 1) * 8 + c - 1 + 1))
                            {
                                //btn.BackColor = othelloBoard.player == 1 ? Color.Black : Color.White;

                                othelloBoard.board[(ind - 1) / 8, ind - ((ind - 1) / 8 * 8) - 1] = othelloBoard.player;
                                if (othelloBoard.player == 1)
                                {
                                    othelloBoard.blackCount++;
                                    othelloBoard.whiteCount--;
                                }
                                else
                                {
                                    othelloBoard.whiteCount++;
                                    othelloBoard.blackCount--;
                                }
                            }
                        }
                        c--;
                        r++;
                    }
                    valid = true;
                }
            }

            if (valid)
            {
                othelloBoard.board[row, col] = othelloBoard.player;
            }

            return valid;
        }


        public List<(Board, int)> GetAllPossibleMoves()
        {
            var listOfBoards = new List<(Board, int)>(); //zwracamy planszę i pole na którym został wykonany ruch

            for (int ind = 1; ind <= 64; ind++)
            {
                var newBoard = new Board();
                //newBoard.strategy = this.strategy;
                newBoard.blackCount = this.blackCount;
                newBoard.whiteCount = this.whiteCount;
                newBoard.player = this.player;
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        newBoard.board[i, j] = this.board[i, j];
                    }
                }

                if (newBoard.MakeMoveIfValid(newBoard, ind))
                {
                    listOfBoards.Add((newBoard, ind));
                }
            }

            return listOfBoards;
        }



        public List<(Board, int)> GetAllPossibleMoves2()
        {
            var listOfBoards = new List<(Board, int)>(); //zwracamy planszę i pole na którym został wykonany ruch

            for (int ind = 1; ind <= 64; ind++)
            {
                var newBoard = new Board();
                //newBoard.strategy = this.strategy;
                newBoard.blackCount = this.blackCount;
                newBoard.whiteCount = this.whiteCount;
                newBoard.player = this.player;
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        newBoard.board[i, j] = this.board[i, j];
                    }
                }

                if (newBoard.MakeMoveIfValid(newBoard, ind))
                {
                    listOfBoards.Add((newBoard, ind));
                }
            }

            return listOfBoards;
        }

        public double CountRewardDiscrete()
        {
            if(player == 1) // black player
            {
                if (this.blackCount > this.whiteCount)
                    return 1;
                else if (this.blackCount == this.whiteCount)
                    return 0;
                else return -1;
            }
            else
            {
                if (this.blackCount < this.whiteCount)
                    return 1;
                else if (this.blackCount == this.whiteCount)
                    return 0;
                else return -1;
            }
        }

        public double CountRewardDifference()
        {
            if (player == 1) // black player
            {
                return this.blackCount - this.whiteCount;
            }
            else return this.whiteCount - this.blackCount; 
        }


    }
}
