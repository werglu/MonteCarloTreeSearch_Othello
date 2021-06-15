using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace Othello
{
    public partial class Form1 : Form
    {
        public Board othelloBoard = new Board();
        public bool NoMoves = false;
        public double cp;
        public int iter;
        IStrategy solver;
        public Strategy gameStrategy;

        public Form1()
        {
            InitializeComponent();
  
            foreach (var button in this.Controls[0].Controls[0].Controls.OfType<Button>())
            {
                SetUpButton(button, Color.Green, true);
                button.Click += button2_Click;
            }

            SetUpButton(button28, Color.Black);
            SetUpButton(button37, Color.Black);
            SetUpButton(button29, Color.White);
            SetUpButton(button36, Color.White);

            
        }


        public void CreateStrategy(Strategy strategy)
        {
            gameStrategy = strategy;
            if (gameStrategy == Strategy.Heuristic)
            {
                solver = new HeuristicStrategy();
            }
            else if (gameStrategy == Strategy.BasicUCT)
            {
                solver = new UTCStrategy(-1, cp, iter, true);
            }
            else if (gameStrategy == Strategy.UCB1_Tuned)
            {
                solver = new TunedStrategy(-1, cp, iter, true);
            }
            else if (gameStrategy == Strategy.DiffereceReward_BasicUCT)
            {
                solver = new UTCStrategy(-1, cp, iter, false);
            }
            else if (gameStrategy == Strategy.DiffereceReward_UCB1_Tuned)
            {
                solver = new TunedStrategy(-1, cp, iter, false);
            }
        }

        private void SetUpButton(Button button, Color c, bool enable = false)
        {
            button.Enabled = enable;
            button.TabStop = false;
            button.Dock = DockStyle.Fill;
            button.Text = "";
            button.BackColor = c;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            GraphicsPath p = new GraphicsPath();
            p.AddEllipse(button.Width / 2 - 30, button.Height / 2 - 30, 60, 60);
            button.Region = new Region(p);
        }

        private bool MoveIfIsMoveValid(Button button, int index = 0) //sprawdza czy ruch jest ok, i jeśli tak to wykonuje go 
        {
            bool valid = false;
            int buttonIndex = index;
            if (button != null)
            { 
                var buttonName = button.Name.Replace("button", "");
                buttonIndex = int.Parse(buttonName);
            }

            int row = (buttonIndex-1) / 8;
            int col = buttonIndex - (row * 8) - 1;

            if(othelloBoard.board[row, col] != 0) //pole jest już zajęte
            {
                return false;
            }

            if (row-1 >=0 && othelloBoard.board[row-1, col] == (othelloBoard.player * (-1))) //up
            {
                int r = row - 1;
                bool canReverse = false;
                while (r>=0)
                {
                    if (othelloBoard.board[r, col] == othelloBoard.player)
                    {
                        canReverse = true;
                        break;
                    }
                    else if (othelloBoard.board[r, col] == 0) //empty
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
                        foreach (var btn in this.Controls[0].Controls[0].Controls.OfType<Button>())
                        {
                            var ind = int.Parse(btn.Name.Replace("button", ""));
                            if (ind == ((r + 1) * 8 + col + 1))
                            {
                                btn.Enabled = true;
                                btn.BackColor = othelloBoard.player == 1 ? Color.Black : Color.White;
                                btn.Enabled = false;

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

            if (col-1>=0 && othelloBoard.board[row, col-1]== othelloBoard.player * (-1)) //left
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
                    else if(othelloBoard.board[row, c] == 0) //empty
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
                    foreach (var btn in this.Controls[0].Controls[0].Controls.OfType<Button>())
                    {
                        var ind = int.Parse(btn.Name.Replace("button", ""));
                        if (ind >= (row * 8 + c + 2) && ind <= (row * 8 + col - 1 + 1))
                        {
                            btn.Enabled = true;
                            btn.BackColor = othelloBoard.player == 1 ? Color.Black : Color.White;
                            btn.Enabled = false;

                            othelloBoard.board[row, ind - ((ind-1)/8 * 8)-1] = othelloBoard.player;
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
                    else if (othelloBoard.board[r, col] == 0) //empty
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
                        foreach (var btn in this.Controls[0].Controls[0].Controls.OfType<Button>())
                        {
                            var ind = int.Parse(btn.Name.Replace("button", ""));
                            if (ind == ((r - 1) * 8 + col + 1))
                            {
                                btn.Enabled = true;
                                btn.BackColor = othelloBoard.player == 1 ? Color.Black : Color.White;
                                btn.Enabled = false;

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
                        foreach (var btn in this.Controls[0].Controls[0].Controls.OfType<Button>())
                        {
                            var ind = int.Parse(btn.Name.Replace("button", ""));
                            if (ind == (row * 8 + c - 1 + 1))
                            {
                                btn.Enabled = true;
                                btn.BackColor = othelloBoard.player == 1 ? Color.Black : Color.White;
                                btn.Enabled = false;

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

            if (row-1>=0 && col-1 >= 0 && othelloBoard.board[row-1, col-1] == othelloBoard.player * (-1)) //left-up
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
                    else if (othelloBoard.board[r, c] == 0) //empty
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
                        foreach (var btn in this.Controls[0].Controls[0].Controls.OfType<Button>())
                        {
                            var ind = int.Parse(btn.Name.Replace("button", ""));
                            if (ind == ((r + 1) * 8 + c + 1 + 1))
                            {
                                btn.Enabled = true;
                                btn.BackColor = othelloBoard.player == 1 ? Color.Black : Color.White;
                                btn.Enabled = false;

                                othelloBoard.board[(ind-1) / 8, ind - ((ind-1) / 8 * 8) - 1] = othelloBoard.player;
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
                    else if (othelloBoard.board[r, c] == 0) //empty
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
                        foreach (var btn in this.Controls[0].Controls[0].Controls.OfType<Button>())
                        {
                            var ind = int.Parse(btn.Name.Replace("button", ""));
                            if (ind == ((r - 1) * 8 + c - 1 + 1))
                            {
                                btn.Enabled = true;
                                btn.BackColor = othelloBoard.player == 1 ? Color.Black : Color.White;
                                btn.Enabled = false;

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
                    else if (othelloBoard.board[r, c] == 0) //empty
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
                        foreach (var btn in this.Controls[0].Controls[0].Controls.OfType<Button>())
                        {
                            var ind = int.Parse(btn.Name.Replace("button", ""));
                            if (ind == ((r - 1) * 8 + c + 1 + 1))
                            {
                                btn.Enabled = true;
                                btn.BackColor = othelloBoard.player == 1 ? Color.Black : Color.White;
                                btn.Enabled = false;

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
                    else if (othelloBoard.board[r, c] == 0) //empty
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
                        foreach (var btn in this.Controls[0].Controls[0].Controls.OfType<Button>())
                        {
                            var ind = int.Parse(btn.Name.Replace("button", ""));
                            if (ind == ((r + 1) * 8 + c - 1 + 1))
                            {
                                btn.Enabled = true;
                                btn.BackColor = othelloBoard.player == 1 ? Color.Black : Color.White;
                                btn.Enabled = false;

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

        private bool IsAnyMovePossibleForButton(Button button) //sprawdza czy jest możliwy jakikolwiek ruch dla danego przycisku
        {
            var buttonName = button.Name.Replace("button", "");
            int buttonIndex = int.Parse(buttonName);
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
                    else if (othelloBoard.board[r, col] == 0) //empty
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
                    return true;
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
                    else if (othelloBoard.board[r, col] == 0) //empty
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
                    return true;
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
                    else if (othelloBoard.board[r, c] == 0) //empty
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
                    else if (othelloBoard.board[r, c] == 0) //empty
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
                    else if (othelloBoard.board[r, c] == 0) //empty
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
                    else if (othelloBoard.board[r, c] == 0) //empty
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

        private bool IsAnyMovePossible()
        {
            foreach (var button in this.Controls[0].Controls[0].Controls.OfType<Button>())
            {
                if (IsAnyMovePossibleForButton(button))
                {
                    return true;
                }               
            }
            return false;
        }

        private void NextMoveByComputer()
        {
            int nextMove = -1;
            if (gameStrategy == Strategy.Heuristic)
            {
                //jeśli jest możliwe położenie piona w rogu to wykonaj ten ruch

                //generujemy wszytskie możliwe ruchy i wybieramy najlepszy
                var listOfAllMoves = solver.GenerateAllMoves(othelloBoard);
                if (listOfAllMoves.Count() == 0) //komputer nie ma ruchu
                {
                    nextMove = -1;
                }
                else
                {
                    var bestWhiteCount = listOfAllMoves.Max(x => x.Item1.whiteCount);
                    var best = listOfAllMoves.Where(x => x.Item1.whiteCount == bestWhiteCount); //lista najlepszych ruchów
                    Random rnd = new Random();
                    int bestIndex = rnd.Next(0, best.Count()); // losujemy ruch sposród najlepszych ruchów
                    var bestMove = best.ElementAt(bestIndex);
                    nextMove = bestMove.Item2;
                }
            }
            else
            {
                nextMove = solver.GetNextMove(othelloBoard);
            }

            if(nextMove >= 0)
            {
                System.Threading.Thread.Sleep(1000); // opóźnienie 
                MoveIfIsMoveValid(null, nextMove); //uaktualniamy stan planszy
                FindButtonById(nextMove, othelloBoard.player); //dodajemy element na wybranym polu i uaktualniamy black i white counter
            }
            else
            {
                label1.Text = "Brak ruchu. " + (othelloBoard.player == 1 ? "Gracz Czarny traci kolejke" : "Gracz Biały traci kolejkę");
            }

            othelloBoard.player = othelloBoard.player * (-1);
        }

        //komputer gra jako Player = -1, użytkownik jako player = 1; użytkownik zaczyna grę
        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            if (!IsAnyMovePossible())
            {
                if (NoMoves)
                {
                    MessageBox.Show("Brak ruchów. Koniec gry.", "Koniec gry");
                }
                else
                {
                    NoMoves = true;
                    label1.Text = "Brak ruchu. " + (othelloBoard.player == 1 ? "Gracz Czarny traci kolejke" : "Gracz Biały traci kolejkę");
                    othelloBoard.player = othelloBoard.player * (-1);
                    NextMoveByComputer();
                }
            }
            else
            {   
                NoMoves = false;
                if (MoveIfIsMoveValid((sender as Button)))
                {
                    if (othelloBoard.player == 1)
                    {
                        othelloBoard.blackCount++;
                    }
                    else
                    {
                        othelloBoard.whiteCount++;
                    }

                    (sender as Button).BackColor = othelloBoard.player == 1 ? Color.Black : Color.White;
                    (sender as Button).Enabled = false;
                    othelloBoard.player = othelloBoard.player * (-1);
                    label2.Text = "Black:" + othelloBoard.blackCount.ToString();
                    label3.Text = "White:" + othelloBoard.whiteCount.ToString();

                    if (othelloBoard.player == -1) //komputer
                    {
                        NextMoveByComputer();
                    }
                }
                else
                {
                    label1.Text = "Ruch niewykonalny, spróbuj jeszcze raz";
                }
            }

            label2.Text = "Black:" + othelloBoard.blackCount.ToString();
            label3.Text = "White:" + othelloBoard.whiteCount.ToString();

            if (othelloBoard.whiteCount == 0)
            {
                MessageBox.Show("Zwyciężył gracz czarny!", "Zwycięstwo!");
            }
            else if (othelloBoard.blackCount == 0)
            {
                MessageBox.Show("Zwyciężył gracz biały!", "Zwycięstwo!");
            }
            else if (othelloBoard.whiteCount + othelloBoard.blackCount == 64 || othelloBoard.IsBoardFill())
            {
                if (othelloBoard.whiteCount > othelloBoard.blackCount)
                {
                    MessageBox.Show("Zwyciężył gracz biały!", "Zwycięstwo!");
                }
                else if (othelloBoard.blackCount > othelloBoard.whiteCount)
                {
                    MessageBox.Show("Zwyciężył gracz czarny!", "Zwycięstwo!");
                }
                else
                {
                    MessageBox.Show("Remis!", "Remis!");
                }
            }
        }

        public void FindButtonById(int id, int player)
        {
            foreach (var btn in this.Controls[0].Controls[0].Controls.OfType<Button>())
            {
                var ind = int.Parse(btn.Name.Replace("button", ""));
                if (ind == id)
                {
                    btn.Enabled = true;
                    btn.BackColor = player == 1 ? Color.Black : Color.White;
                    btn.Enabled = false;
                }
            }
            if (player == 1)
            {
                othelloBoard.blackCount++;
            }
            else
            {
                othelloBoard.whiteCount++;
            }
        }
    }
}
