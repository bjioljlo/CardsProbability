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
        List<SaveData> Datas;

        public class SaveData
        {
            public int totalCards, markedCards, drawCount, markedCount, level;
            public double persent;
        }

        public Form1()
        {
            InitializeComponent();
            textBox1.KeyPress += new KeyPressEventHandler(textBox1_KeyPress);
            textBox2.KeyPress += new KeyPressEventHandler(textBox1_KeyPress);
            textBox3.KeyPress += new KeyPressEventHandler(textBox1_KeyPress);
            textBox4.KeyPress += new KeyPressEventHandler(textBox1_KeyPress);
        }
        
        public void Count(int totalCards, int markedCards, int drawCount, int markedCount, int alevel = 0)
        {
            alevel++;
            for (int i = 0; i <= markedCount; i++)
            {
                int _totalCards = totalCards;
                int _markedCards = markedCards;

                double probability = GetAtLeastOneMarkedCardProbability(totalCards, markedCards, drawCount, i);

                textBox5.Text = textBox5.Text + "總共:" + totalCards + "張 Level:"+ alevel + " 目標剩下:" + markedCards + "張 抽出: " + drawCount + "張牌中有" + i + "張記號的機率為：" + probability.ToString("P") + Environment.NewLine;

                SaveData temp = new SaveData
                {
                    totalCards = totalCards,
                    markedCards = markedCards,
                    drawCount = drawCount,
                    markedCount = i,
                    persent = probability,
                    level = alevel
                };
                Datas.Add(temp);

                _totalCards -= drawCount;
                if (_totalCards <= 0)
                {
                    textBox5.Text = textBox5.Text + "抽完牌了" + Environment.NewLine;
                    return;
                }
                _markedCards -= i;
                if (_markedCards <= 0)
                {
                    textBox5.Text = textBox5.Text + "抽完指定牌了" + Environment.NewLine;
                    return;
                }
                Count(_totalCards, _markedCards, drawCount, markedCount, alevel);
            }


        }

        static double GetAtLeastOneMarkedCardProbability(int totalCards, int markedCards, int drawCount, int markedCount)
        {
            double probability = 0;

            if (drawCount >= totalCards)
            {
                if (markedCount == markedCards) return 100;
                else return probability;
            }

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
            textBox5.Text = "";
            Datas = new List<SaveData>();
            Count(int.Parse(textBox1.Text), int.Parse(textBox2.Text), int.Parse(textBox3.Text), int.Parse(textBox4.Text));
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            textBox5.ScrollBars = ScrollBars.Vertical;
        }
    }
}
