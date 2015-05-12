using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;



namespace BugRunner
{
    class Game_Controll
    {
        List<Gambler> gamblers;

        internal List<Gambler> Gamblers
        {
            get { return gamblers; }
            set { gamblers = value; }
        }
        List<Bug> spiders;

        internal List<Bug> Spiders
        {
            get { return spiders; }
            set { spiders = value; }
        }
        List<Bet> bets;

        internal List<Bet> Bets
        {
            get { return bets; }
            set { bets = value; }
        }

        int length_of_race;

        public int Length_of_race
        {
            get { return length_of_race; }
            set { length_of_race = value; }
        }

        Bug winner;

        internal Bug Winner
        {
            get { return winner; }
            set { winner = value; }
        }




        public Game_Controll(PictureBox pic1, PictureBox pic2, PictureBox pic3, PictureBox pic4)
        {
            gamblers = new List<Gambler>();
            gamblers.Add(new Gambler("Генадий Капрушин", 1000));
            gamblers.Add(new Gambler("Аркадий Гарбочкин", 800));
            gamblers.Add(new Gambler("Игорь Кукурузов", 1200));

            spiders = new List<Bug>();
            spiders.Add(new Bug(pic1, 13));
            spiders.Add(new Bug(pic2, 113));
            spiders.Add(new Bug(pic3, 209));
            spiders.Add(new Bug(pic4, 303));

            bets = new List<Bet>();

            Length_of_race = 600;

            



        }


        public int ThrowCube()
        {
            Random rand = new Random();
            return rand.Next(1, 6);
        }

        public void StartGame()
        {
            List<Bug> winners = new List<Bug>();

            while (winners.Count == 0)
            {
                foreach (Bug spider in spiders)
                {
                    int steps = ThrowCube();
                    spider.ToMove(steps);
                    if (spider.Current_position.X - spider.Start_position.X >= Length_of_race)
                    {
                        winners.Add(spider);
                    }
                    Thread.Sleep(200);
                }
            }
            if (winners.Count > 0) GivePrizes(winners);
            

        }


        public void AddBet(Bet bet)
        {
            bets.Add(bet);
        }


        public void ReturnToStart()
        {
            foreach (Bug spider in spiders)
            {
                spider.ReturnToStart();
            }
        }

        void RecalculateBetIfMoreWinners(List<Bug> winners)
        {
            int winner_number = winners[0].Number; //главным победителем делаем первого паука из списка победителей
            //А так же в списках ставок изменяем все ставки в которых было поставлено на пауков победителей которые не явлются главным победителем
            foreach (Bug win in winners)
            {
                foreach (Bet bet in bets)
                {
                    if (bet.Bug_number == win.Number) bet.Bug_number = winner_number;
                }
            }
            //то есть те кто ставил на разных пауков победителей, как-будто поставили на одного - первого победителя-паука (если победителей больше одного) 
        }

        public void GivePrizes(List<Bug> winners)
        {
           
            if(winners.Count > 1) RecalculateBetIfMoreWinners(winners); //если выйграло несколько пауков, то пересчитываем ставки по алгоритму описанном выше 
            winner = winners[0];

            double prizeFond = bets.Sum(x=>x.Count_money);//подсчет общей суммы ставок
            double betSumOnWinner = bets.FindAll(x=>x.Bug_number == winner.Number).Sum(x=>x.Count_money);//подсчет общей суммы ставок поставленных на победителя
          
            foreach (Bet bet in bets)
            {
                if (bet.Bug_number == winner.Number)
                {
                    double current_prize = (double)(bet.Count_money / betSumOnWinner) * prizeFond;
                    gamblers.Find(x => x.Number == bet.Gambler_number).GetPay(current_prize);
                    
                }
            }

        }

        public string GenerateResult()
        {
            string result ="";
            if (winner != null && bets.Count > 0)
            {
                foreach(Gambler gam in gamblers)
                {
                    if (bets.Exists(x => x.Gambler_number == gam.Number))//проверяем делал ли ставки игрок
                    {
                        result += gam.Name + ": ";
                        if (gam.Last_prize > 0) result += "Выиграл " + Math.Round(gam.Last_prize,2) + " руб\n"; //если сумма приза полученого при текущей игре положительна значит игрок что-то выйграл
                        else result += "Проиграл\n";
                    }
                }
            }
            return result;
        }

       public void EndCurrentGame()
        {
            winner = null;
           

            foreach (Gambler gam in gamblers)
            {
                gam.Clear_State();
            }

            bets.Clear();

        }


    }
}
