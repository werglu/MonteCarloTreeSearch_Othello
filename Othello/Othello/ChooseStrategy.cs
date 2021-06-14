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
        public double cp =  Math.Sqrt(2);
        public int iter = 200;
        public ChooseStrategy()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                strategy = Strategy.Heuristic;
                label2.Hide();
                label3.Hide();
                textBox1.Hide();
                textBox2.Hide();
            }
            else if (radioButton1.Checked && !checkBox1.Checked)
            {
                strategy = Strategy.BasicUCT;
                label2.Show();
                label3.Show();
                textBox1.Show();
                textBox2.Show();
                cp = double.Parse(textBox1.Text);
                iter = int.Parse(textBox2.Text);
            }
            else if(radioButton1.Checked && checkBox1.Checked)
            {
                strategy = Strategy.DiffereceReward_BasicUCT;
                label2.Show();
                label3.Show();
                textBox1.Show();
                textBox2.Show();
                cp = double.Parse(textBox1.Text);
                iter = int.Parse(textBox2.Text);
            }
            else if(radioButton3.Checked && !checkBox1.Checked)
            {
                strategy = Strategy.UCB1_Tuned;
                label2.Show();
                label3.Show();
                textBox1.Show();
                textBox2.Show();
                cp = double.Parse(textBox1.Text);
                iter = int.Parse(textBox2.Text);
            }
            else if(radioButton3.Checked && checkBox1.Checked)
            {
                strategy = Strategy.DiffereceReward_UCB1_Tuned;
                label2.Show();
                label3.Show();
                textBox1.Show();
                textBox2.Show();
                cp = double.Parse(textBox1.Text);
                iter = int.Parse(textBox2.Text);
            }

            this.Hide();
            var form = new Form1();
            form.cp = cp;
            form.iter = iter;
            form.gameStrategy = strategy;
            form.Show();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                strategy = Strategy.Heuristic;
                label2.Hide();
                label3.Hide();
                textBox1.Hide();
                textBox2.Hide();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                strategy = Strategy.BasicUCT;
                label2.Show();
                label3.Show();
                textBox1.Text = cp.ToString("N4");
                textBox2.Text = iter.ToString();
                textBox1.Show();
                textBox2.Show();
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                strategy = Strategy.UCB1_Tuned;
                label2.Show();
                label3.Show();
                textBox1.Text = cp.ToString("N4");
                textBox2.Text = iter.ToString();
                textBox1.Show();
                textBox2.Show();
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
