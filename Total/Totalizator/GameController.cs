using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugRacer
{
    public delegate void StartRace(List<Bet> bets);
    public delegate void Finish(Score result);
    public delegate void RaiseEventIfFinished();

    public class GameController
    {
       

        private List<Bug> bugs;
        private List<Gambler> gamers;
        private List<Bet> bets;
        private Totalizator totalizator;
        private RaiseEventIfFinished checkAllBugsHasFinished;
        private Score score;
        private Random r;

        public event Finish finish;

        public int RaceCount { get; private set; }

        public Dictionary<Bug, float> BugsChances
        {
            get
            {
                return totalizator.BugsChances;
            }
        }

        public Dictionary<Bug, int> BugsTotalScores
        {
            get
            {
                return totalizator.BugsTotalScores;
            }
        }
        
        //конструктор
        public GameController(List<Bug> bugs,List<Gambler> gamers)
        {
            this.bugs = new List<Bug>(bugs);
            this.gamers =new List<Gambler>( gamers );
            r = new Random();
            RaceCount = 0;
            score = new Score();
            totalizator = new Totalizator(bugs);

            foreach(var bug in bugs)
            {
                bug.finish += BugFinishedEventHandle;
                checkAllBugsHasFinished += bug.RaiseEventIfFinished;
            }
        }

        //Функция подготавливает данные для нового забега
        public void OnStart(List<Bet> bets)
        {
            this.bets = bets;
            RaceCount++;
            score.BugsPlaces.Clear();
            score.GamblersPrizes.Clear();
            score.WinnerBug = null;

            foreach (var bug in bugs)
            {
                bug.Succsess = r.Next(- bug.Professionality, 5);
                bug.IsFinished = false;
            }
        }

        //двигает жуков
        public void OnTimer(object sender, EventArgs e)
        {

            foreach (var bug in bugs)
            {
                bug.Move(GenerateRandIntFor(bug));
            }

            checkAllBugsHasFinished();
        }

        //Регистрирует участников забега дошедщие до финеша
        //вызываются самими участниками (жуками)
        //точнее обрабатывает событие генерируемое жуками
        public void BugFinishedEventHandle(Bug bug1)
        {
            if (score.WinnerBug==null)
            {
                score.WinnerBug = bug1;
            }
            score.BugsPlaces[bug1] = score.BugsPlaces.Count+1;

            if (score.BugsPlaces.Count == bugs.Count)
            {
                totalizator.RegisterRace(score);
                totalizator.CalculatePrizes(bets);
                totalizator.UpdateChances();


                foreach (var bet in bets)
                {
                    bet.Gambler.TakePrize(score.GamblersPrizes[bet.Gambler]);
                }

                foreach (var bug in bugs)
                {
                    bug.MoveToStart();
                }

                finish(score);
            }
        }
        //генерация шага для движения в зависимости от жука
        private int GenerateRandIntFor(Bug bug)
        {
            return (int)((r.Next(10) + bug.Professionality + bug.Succsess));
        }

       

       
    }
}
