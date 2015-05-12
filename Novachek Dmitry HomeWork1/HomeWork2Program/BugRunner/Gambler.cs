using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BugRunner
{
    class Gambler
    {
        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        static int current_number;

        public int Current_number
        {
            get { return current_number; }
            set { current_number = value; }
        }

        int number;

        public int Number
        {
            get { return number; }
            set { number = value; }
        }

        double money;

        public double Money
        {
            get { return money; }
            set { money = value; }
        }
        public Gambler(string Gambler_name, double count_of_money)
        {
            name = Gambler_name;
            number = ++Current_number;
            money = count_of_money;
            last_prize = 0;
        }
        public void GetPay(double prize)
        {
            money += prize;
            last_prize += prize;

        }

        double last_prize;

        public double Last_prize
        {
            get { return last_prize; }
            set { last_prize = value; }
        }

        public Bet MakeBet(int spider_number, double sum)
        {
            Bet bet;
            if (sum > money) throw new MoneySubZeroException("Недостачно денег для ставки. У вас "+money+" руб.");
            else
            {
                money -= sum;
                bet = new Bet(number, spider_number, sum);
            }
            return bet;
            
        }

       public void Clear_State()
        {
            last_prize = 0;
        }

    }
}
