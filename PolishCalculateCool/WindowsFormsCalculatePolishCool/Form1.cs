using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PolishCalculateCool;

namespace WindowsFormsCalculatePolishCool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            
        }

        string InfixString;
        string PostfixString;
        string[] markers;
        string[] postfixMarkers;

        ConvertPostfix converter = new ConvertPostfix();
        Calculate calculate = new Calculate();

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "")
                {
                    throw new Exception("вы ничего не ввели");
                }

                InfixString = textBox1.Text;
                markers = InfixString.Split(' ');

                PostfixString = converter.Converting(markers);
                resultPolish.Text = PostfixString;

                postfixMarkers = PostfixString.Split(' ');
                resultCalc.Text = calculate.CalculatePolish(postfixMarkers).ToString();

                textBox1.Clear();
            }



            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
