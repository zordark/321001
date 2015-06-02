using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media;

namespace MindGame
{
    class Character: GameParticipant
    {

       private Gun weapon;

        public virtual Gun Weapon
        {
            get { return weapon; }
            set { weapon = value; }
        }

       private int max_step;

        public virtual int Max_step
        {
            get { return max_step; }
            set { max_step = value; }
        }

       public Character(string name_,Gun wp, int steps):base(name_)
        {
           Name = name_;
            Weapon = wp;
            Max_step = steps;

            Shape = new Rectangle();
            Shape.Height = 60;
            Shape.Width = 60;
            Shape.Fill = Brushes.White;
            Shape.Stroke = Brushes.White;
        }


        public bool CheckRange(Point point_new,int range) // проверяем, можно за текущее количество ходов дойти до точки
        {
            bool result = false;
            int x_step = (int)Math.Abs(point_new.X - Table_position.X);
            int y_step = (int)Math.Abs(point_new.Y - Table_position.Y);

            if (x_step == 0 && y_step <= range) result = true;
            else if (y_step == 0 && x_step <= range) result = true;
            else if (y_step == x_step && y_step <= range) result = true;
            else result = false;

            return result;

        }

    }
}
