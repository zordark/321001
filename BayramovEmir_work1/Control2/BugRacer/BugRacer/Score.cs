using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugRacer
{
    public class Score
    {
        public Bug WinnerBug { get; set; }
        public Dictionary<Bug, int> BugsPlaces { get; private set; }
        public Dictionary<Gambler, int> GamblersPrizes { get; private set; }

        public Score()
        {
            BugsPlaces = new Dictionary<Bug, int>();
            GamblersPrizes = new Dictionary<Gambler, int>();
        }
    }
}
