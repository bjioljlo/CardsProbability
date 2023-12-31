using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CardsProbability
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            textBox1.KeyPress += new KeyPressEventHandler(textBox1_KeyPress);
            textBox2.KeyPress += new KeyPressEventHandler(textBox1_KeyPress);
            textBox3.KeyPress += new KeyPressEventHandler(textBox1_KeyPress);
            textBox4.KeyPress += new KeyPressEventHandler(textBox1_KeyPress);
        }

        public void Count()
        {
            int totalCards = int.Parse(textBox1.Text);
            int markedCards = int.Parse(textBox2.Text);
            int drawCount = int.Parse(textBox3.Text);
            int markedCount = int.Parse(textBox4.Text);
            label5.Text = "";
            for (int i = 0; i <= markedCount; i++)
            {
                double probability = GetAtLeastOneMarkedCardProbability(totalCards, markedCards, drawCount, i);

                label5.Text = label5.Text + "抽出 " + drawCount + " 張牌中有" + i + "張記號的機率為：" + probability.ToString("P") + "\n";
            }

            
        }

        static double GetAtLeastOneMarkedCardProbability(int totalCards, int markedCards, int drawCount, int markedCount)
        {
            double probability = 0;

            if (markedCards <= totalCards && markedCount <= drawCount)
            {
                int combinations = GetCombinations(drawCount, markedCount);
                double p = (double)markedCards / totalCards;
                probability = combinations * Math.Pow(p, markedCount) * Math.Pow(1 - p, drawCount - markedCount);
            }

            return probability;
        }

        static int GetCombinations(int n, int k)
        {
            int result = 1;

            for (int i = n; i >= n - k + 1; i--)
            {
                result *= i;
            }

            for (int i = k; i >= 1; i--)
            {
                result /= i;
            }

            return result;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 0x20) e.KeyChar = (char)0;
            if ((e.KeyChar == 0x20) && (((TextBox)sender).Text.Length == 0)) return;
            if (e.KeyChar > 0x20)
            {
                try
                {
                    double.Parse(((TextBox)sender).Text + e.KeyChar.ToString());
                }
                catch
                {
                    e.KeyChar = (char)0;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Count();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
