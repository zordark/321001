using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugRacer
{  //Игрок
    public class Gambler
    {

        public int Number { get; private set; }
        public string Name { get; private set; }
        public int Money { get; private set; }

        public Gambler(string name,int number,int money)
        {
            Number = number;
            Name = name;
            Money = money;
        }

        //Сделать ставку
        public int MakeBet(int amount)
        {
            
            Money -= amount;

            return amount;
        }

       //получить приз
        public void TakePrize(int amount)
        {
            Money += amount;
        }

        public bool HasEnoughMoney(int amount)
        {
            return Money >= amount;
        }


        public override bool Equals(object obj)
        {
            return object.ReferenceEquals(obj,this);
        }


      
        public override int GetHashCode()
        {
            return Number;
        }
    }

    
}
