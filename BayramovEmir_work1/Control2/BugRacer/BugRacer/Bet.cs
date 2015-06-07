using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugRacer
{
    //Ставка
    public class Bet
    {
        public Gambler Gambler { get; private set; }
        public Bug Bug { get; private set; }
        public int Amount { get; private set; }

        public Bet(Gambler gambler, Bug bug, int amount)
        {
            Gambler = gambler;
            Bug = bug;
            Amount = amount;
        }
    }
}
