using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BugRunner
{
    class Bet
    {
        int gambler_number;

        public int Gambler_number
        {
            get { return gambler_number; }
            set { gambler_number = value; }
        }

        double count_money;

        public double Count_money
        {
            get { return count_money; }
            set { count_money = value; }
        }



        int bug_number;

        public int Bug_number
        {
            get { return bug_number; }
            set { bug_number = value; }
        }






        public Bet(int gambler_num, int bug_num, double money)
        {
            Gambler_number = gambler_num;
            Bug_number = bug_num;
            Count_money = money;
        }
    }
}
