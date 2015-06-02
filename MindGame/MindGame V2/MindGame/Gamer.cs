using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MindGame
{
    class Gamer : Character
    {

        int buffer_steps; // оставшееся количество ходов на текущем ходе с учетом подобранных протеинов

        public int Buffer_steps
        {
            get { return buffer_steps; }
            set { buffer_steps = value; }
        }

        public Gamer(string name_, Gun gun):base(name_,gun,1)
        {
            
            Uri uriKind = new Uri("Hero.png", UriKind.Relative);
            BitmapImage bi = new BitmapImage(uriKind);

            ImageBrush ib = new ImageBrush(bi);

            ib.Stretch = Stretch.Fill;
            Shape.Stroke = Brushes.Transparent;
            Shape.Fill = ib;
        
           
            Shape.ToolTip = Name + ": дальность хода " + Max_step + "кл., оружие " + Weapon.Name + ", радиус атаки " + Weapon.Range + " кл.";
            Buffer_steps = Max_step;

        }

        public void MakeStep(Point next_pos)
        {
           
            if (CheckRange(next_pos,buffer_steps))
            {
                int lenght_step = 0;
                int x_step = (int)Math.Abs(next_pos.X - Table_position.X);

                if (x_step != 0) lenght_step = x_step; else lenght_step = (int)Math.Abs(next_pos.Y - Table_position.Y); // рассчитываем длину хода до данной позиции

                if (Buffer_steps < lenght_step) throw new FarStepException("Ходов больше нет");
               
                Buffer_steps -= lenght_step; 
               
                Table_position = next_pos;
            }
            else throw new FarStepException("Ваш максимальный ход " + Max_step.ToString() + " кл.");
        }

        public void Shoot(Point kill_position)
        {
            if (CheckRange(kill_position, Weapon.Range))
            {
                if (Buffer_steps <= 0 ) throw new FarStepException("Ходов для выстрела больше нет");
                Buffer_steps--; 
            }
            else throw new FarStepException("Дальность выстрела " + Weapon.Range.ToString() + " кл.");
        }

        public void RecoveryMoves() // восстанавливаем количество шагов для следующего хода
        {
            Buffer_steps = Max_step;
        }


        public void Eat(BoostItem item) // кушаем протеин и увеличиваем кол-во шагов на текущем ходе
        {
            if (CheckRange(item.Table_position, buffer_steps))
            {

                int lenght_step = 0;
                int x_step = (int)Math.Abs(item.Table_position.X - Table_position.X);
                if (x_step != 0) lenght_step = x_step; else lenght_step = (int)Math.Abs(item.Table_position.Y - Table_position.Y);

                Buffer_steps += item.Value_increase;
            }
            else throw new FarStepException("Ваш максимальный ход " + Max_step.ToString() + " кл.");
        }
    }
}
