using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BugRacer
{
    public partial class BugRacerForm : Form
    {

        private const int GAME_SPEED = 50;
        Gambler currentGambler;
        Bug currentBug;
        private GameController controller;
        List<Gambler> gamblers;
        List<Bug> bugs;

        Dictionary<Gambler, Bet> bets;
        Label[] bugsTotalScoresLabels;
        Label[] bugsChanceLabels;
        Label[] gamblerMoneysLabels;
        

        public BugRacerForm()
        {
            InitializeComponent();

            bugsTotalScoresLabels = new Label[] { label11, label14, label17, label20 };
            bugsChanceLabels = new Label[] { label10, label13, label16, label19 };
            gamblerMoneysLabels = new Label[] { label23,label6,label25};

            bugs = new List<Bug>();
            bugs.Add(new Bug("Таракан",1, 520, pictureBox1,8));
            bugs.Add(new Bug("Паук",2, 520, pictureBox2,6));
            bugs.Add(new Bug("Жук 'Носорог'",3, 520, pictureBox3,7));
            bugs.Add(new Bug("Скорпион",4, 520, pictureBox4,5));

            gamblers = new List<Gambler>();
            gamblers.Add(new Gambler("Игрок 1", 1, 500));
            gamblers.Add(new Gambler("Игрок 2", 2, 500));
            gamblers.Add(new Gambler("Игрок 3", 3, 500));

            currentGambler = gamblers[0];
            currentBug = bugs[0];
            
            bets = new Dictionary<Gambler, Bet>();

            controller = new GameController(bugs, gamblers);

            controller.finish += HandleFinishEvent;
            start += controller.OnStart;

            timer1.Tick += controller.OnTimer;
            timer1.Enabled = false;
            timer1.Interval = GAME_SPEED;

            UpdateInfo();

        }
        
        //обработка события конца забега
        public void HandleFinishEvent(Score score)
        {
            timer1.Enabled = false;
            bets.Clear();
            UpdateInfo();
            UpdateScoreInfo(score);
            groupBox1.Enabled = true ;
        }

       

        //обновление информаии о результате зебега
        private void UpdateScoreInfo(Score score)
        {
            listBox2.Items.Add(string.Format("{0} выиграл забег номер {1}", score.WinnerBug.Name, controller.RaceCount));
            foreach (var bug in score.BugsPlaces.Keys)
            {
                listBox2.Items.Add(string.Format("{0} прибыл {1}-м", bug.Name, score.BugsPlaces[bug]));
            }
            foreach (var gambler in score.GamblersPrizes.Keys)
            {
                if (score.GamblersPrizes[gambler] > 0)
                    listBox2.Items.Add(string.Format("{0} выиграл {1} р.", gambler.Name, score.GamblersPrizes[gambler]));
                else
                    listBox2.Items.Add(string.Format("{0} проиграл", gambler.Name));
            }
        }
        //Обновление информации обучастника и игроках
        private void UpdateInfo()
        {

            listBox1.Items.Clear();
            var chances = controller.BugsChances;
            var scores = controller.BugsTotalScores;


            foreach(var bug in chances.Keys)
            {
                bugsChanceLabels[bug.Number - 1].Text = string.Format("{0:0.00}",chances[bug]);
            }

            foreach (var bug in scores.Keys)
            {
                bugsTotalScoresLabels[bug.Number - 1].Text = string.Format("{0:0.00}", scores[bug]-1);
            }

            foreach (var gambler in gamblers)
            {
                gamblerMoneysLabels[gambler.Number - 1].Text = string.Format("{0:0.00}", gambler.Money);
            }

            foreach (var gambler in bets.Keys)
            {
                listBox1.Items.Add(string.Format("{0} поставил {1:0.00} на {2}", gambler.Name, bets[gambler].Amount, bets[gambler].Bug.Name));
            }
        }

      
        private void StartRace(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            start(bets.Values.ToList());
            groupBox1.Enabled = false;
        }


        
        
        private void CurrentGamblerChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                currentGambler = gamblers[0];
            else if (radioButton2.Checked)
                currentGambler = gamblers[1];
            else if (radioButton3.Checked)
                currentGambler = gamblers[2];
        }

       

        private void CurrentBugChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
                currentBug = bugs[0];
            else if (radioButton5.Checked)
                currentBug = bugs[1];
            else if (radioButton6.Checked)
                currentBug = bugs[2];
            else if (radioButton7.Checked)
                currentBug = bugs[3];
        }

        private void MakeBet(object sender, EventArgs e)
        {
            int amount;
            
            if (int.TryParse(textBox1.Text, out amount))
                if(amount == 0)
                    MessageBox.Show("Нельзя поставит 0 рублей");
                else if (currentGambler.HasEnoughMoney(amount))
                {
                    currentGambler.MakeBet(amount);
                    bets[currentGambler] = new Bet(currentGambler, currentBug, amount);
                }
                else
                    MessageBox.Show("Недостаточно денег для ставки");
            else 
                MessageBox.Show("Неправильно введена сумма ставки");

            UpdateInfo();
            
        }

      
    }
}
