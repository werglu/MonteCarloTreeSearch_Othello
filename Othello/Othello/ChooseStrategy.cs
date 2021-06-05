using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Othello
{
    public partial class ChooseStrategy : Form
    {
        public Strategy strategy;
        public ChooseStrategy()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                strategy = Strategy.BasicMCTS;
            }
            else if (radioButton2.Checked)
            {
                strategy = Strategy.Heuristic;
            }

            this.Hide();
        }
    }
}
