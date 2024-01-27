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
        TController _Controller;
        public TController Controller
        {
            get { return _Controller; }
            set 
            {
                _Controller = value;
            }
        }
        public Form1()
        {
            InitializeComponent();
            textBox1.KeyPress += new KeyPressEventHandler(TextBox1_KeyPress);
            textBox2.KeyPress += new KeyPressEventHandler(TextBox1_KeyPress);
            textBox3.KeyPress += new KeyPressEventHandler(TextBox1_KeyPress);
            textBox4.KeyPress += new KeyPressEventHandler(TextBox1_KeyPress);
        }
        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Controller.Count(int.Parse(textBox1.Text), int.Parse(textBox2.Text), int.Parse(textBox3.Text), int.Parse(textBox4.Text));
            Controller.ShowText(textBox5);
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            textBox5.Text = "";
            Controller.ClearData();
        }
    }
}
