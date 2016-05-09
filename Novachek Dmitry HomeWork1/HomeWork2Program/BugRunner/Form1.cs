using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BugRunner
{
    public partial class Form1 : Form
    {
        Game_Controll game;

       

        internal Game_Controll Game
        {
            get { return game; }
            set { game = value; }
        }
        Gambler chosen_Gambler;

        internal Gambler Chosen_Gambler
        {
            get { return chosen_Gambler; }
            set { chosen_Gambler = value; }
        }

        Bug chosen_Spider;

        internal Bug Chosen_Spider
        {
            get { return chosen_Spider; }
            set { chosen_Spider = value; }
        }


        public Form1()
        {
            InitializeComponent();
            game = new Game_Controll(SpiderPicture1,SpiderPicture2,SpiderPicture3,SpiderPicture4);
            Chosen_Gambler = game.Gamblers[0];
            Chosen_Spider = game.Spiders[0];
        }


     


        private void GamblerButton1_CheckedChanged(object sender, EventArgs e)
        {
            Chosen_Gambler = game.Gamblers[0];
        }

        private void GamblerButton2_CheckedChanged(object sender, EventArgs e)
        {
            Chosen_Gambler = game.Gamblers[1];
        }

        private void GamblerButton3_CheckedChanged(object sender, EventArgs e)
        {
            Chosen_Gambler = game.Gamblers[2];
        }

        private void SpiderButton1_CheckedChanged(object sender, EventArgs e)
        {
            Chosen_Spider = game.Spiders[0];
        }

        private void SpiderButton2_CheckedChanged(object sender, EventArgs e)
        {
            Chosen_Spider = game.Spiders[1];
        }

        private void SpiderButton3_CheckedChanged(object sender, EventArgs e)
        {
            Chosen_Spider = game.Spiders[2];
        }

        private void SpiderButton4_CheckedChanged(object sender, EventArgs e)
        {
            Chosen_Spider = game.Spiders[3];
        }


        private void UpdateBetTable()
        {
            if (game.Bets.Count > 0)
            {
                string info="";
                foreach (Bet bet in game.Bets)
                {
                    info += game.Gamblers.Find(x => x.Number == bet.Gambler_number).Name + ": " + bet.Count_money + " руб на №" + bet.Bug_number + "\n"; 
                }
                CurrentBetsBox.Text = info;
            }
        }



        private void ButtonBet_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "")
                {
                    double sum = Convert.ToDouble(textBox1.Text);
                    if (sum <= 0) MessageBox.Show("Введите положительное число");
                    else
                    {
                        game.AddBet(chosen_Gambler.MakeBet(chosen_Spider.Number, sum));
                        UpdateBetTable();
                    }
                }
            }
            catch (MoneySubZeroException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex1)
            {
                MessageBox.Show("Введите число, а не что-то другое.");
            }
            
        }

        private void ButtonGo_Click(object sender, EventArgs e)
        {   
            if (game.Bets.Count > 0)
            {
                game.ReturnToStart();
                game.StartGame();
                ResultBox.Text = game.GenerateResult();
                game.EndCurrentGame();
                
            }
            else MessageBox.Show("Ставок нет - нет смысла играть.");

        }
    }
}
