using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugRacer
{
    class Totalizator
    {
        private const float COEFF = 0.275f;

        // коэффициента выигрыша у игроков
        public Dictionary<Bug, float> BugsChances { get; private set;}

        //количество побед у участников
        public Dictionary<Bug, int> BugsTotalScores { get; private set; }

        Score score;

        public Totalizator(List<Bug> bugs)
        {
            BugsTotalScores = new Dictionary<Bug, int>();
            BugsChances = new Dictionary<Bug, float>();

            foreach(var bug in bugs)
            {
                BugsChances.Add(bug, 2.0f);
                BugsTotalScores.Add(bug, 1);
            }
        }

        public void RegisterRace(Score score)
        {
            this.score = score;
        }

        //Обновление коэффициента выигрыша у  участников
        public void UpdateChances()
        {
            BugsTotalScores[score.WinnerBug] += 1;
            float allRaces=0;

            foreach(var wins in BugsTotalScores.Values)
            {
                allRaces += wins;
            }

            foreach(var bug in BugsChances.Keys.ToList())
            {
                BugsChances[bug] = allRaces / (float)BugsTotalScores[bug] * COEFF;
                BugsChances[bug] = BugsChances[bug] < 1.1f ? 1.1f : BugsChances[bug];
            }
        }

        //вычисление выигрыша
        public Score CalculatePrizes(List<Bet> bets)
        {
            foreach(var bet in bets)
            {
                if(IsWin(bet))
                {
                    score.GamblersPrizes[bet.Gambler] = (int)(((float)bet.Amount) * BugsChances[score.WinnerBug]);
                }
                else
                {
                    score.GamblersPrizes[bet.Gambler] = 0;
                }
            }

            return score;
        }

        private bool IsWin(Bet bet)
        {
            return score.WinnerBug == bet.Bug;
        }



    }
}
