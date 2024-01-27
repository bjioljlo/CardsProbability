using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardsProbability
{
    interface IController
    {
        Form1 GetForm();
        void Count(int totalCards, int markedCards, int drawCount, int markedCount, int alevel = 0);
        void ShowText(TextBox textBox);
        void ClearData();
    }
    public class TController:IController
    {
        readonly Form1 Form;
        readonly TModel Model;
        readonly List<SaveData> CountDatas;
        public TController(Form1 form)
        {
            Form = form;
            CountDatas = new List<SaveData>();
            Form.Controller = this;
            Model = new TModel();
        }
        class SaveData
        {
            public int totalCards, markedCards, drawCount, markedCount, level;
            public double persent;
        }
        public void Count(int totalCards, int markedCards, int drawCount, int markedCount, int alevel = 0)
        {
            alevel++;
            for (int i = 0; i <= markedCount; i++)
            {
                int _totalCards = totalCards;
                int _markedCards = markedCards;
                if ((totalCards < drawCount) && (i != markedCards))
                {
                    continue;
                }
                double probability = GetAtLeastOneMarkedCardProbability(totalCards, markedCards, drawCount, i);
                SaveData temp = new SaveData
                {
                    totalCards = totalCards,
                    markedCards = markedCards,
                    drawCount = drawCount,
                    markedCount = i,
                    persent = probability,
                    level = alevel
                };
                CountDatas.Add(temp);
                _totalCards -= drawCount;
                if (_totalCards <= 0)
                {
                    //"抽完牌了"
                    continue;
                }
                _markedCards -= i;
                if (_markedCards <= 0)
                {
                    //"抽完指定牌了"
                    continue;
                }
                Count(_totalCards, _markedCards, drawCount, markedCount, alevel);
            }
            return;
        }
        public void ShowText(TextBox textBox)
        {
            foreach (SaveData child in CountDatas)
            {
                textBox.Text = textBox.Text + "總共:" + child.totalCards + "張 Level:" + child.level + 
                              " 目標剩下:" + child.markedCards + "張 抽出: " + child.drawCount + 
                              "張牌中有" + child.markedCount + "張記號的機率為：" + child.persent.ToString("P") + Environment.NewLine;
            }
        }
        private double GetAtLeastOneMarkedCardProbability(int totalCards, int markedCards, int drawCount, int markedCount)
        {
            double probability = 0;

            if ((drawCount >= totalCards) && (markedCount == markedCards))
            {
                return 1;
            }
            if ((markedCards - markedCount) >= (totalCards - drawCount))
            {
                return probability;
            }
            if (markedCards <= totalCards && markedCount <= drawCount)
            {
                int combinations = GetCombinations(drawCount, markedCount);
                double p = (double)markedCards / totalCards;
                probability = combinations * Math.Pow(p, markedCount) * Math.Pow(1 - p, drawCount - markedCount);
            }
            return probability;
        }
        private int GetCombinations(int n, int k)
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
        public void ClearData()
        {
            CountDatas.Clear();
        }
        public Form1 GetForm()
        {
            return Form;
        }
    }
}
