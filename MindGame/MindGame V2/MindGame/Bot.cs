using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;


namespace MindGame
{


    class Bot:Gamer
    {

        static int index = 0;

        public static void ResetIndex()
        {
            index =0;
        }

        public Bot() : base("Враг "+ (++index), new Gun("cтандартное", 1)) 
        {
           
            
            Uri uriKind = new Uri("Bot.png", UriKind.Relative);
            BitmapImage bi = new BitmapImage(uriKind);

            ImageBrush ib = new ImageBrush(bi);

            ib.Stretch = Stretch.Fill;
            Shape.Stroke = Brushes.Transparent;
            Shape.Fill = ib;
        }

       public Point MakeAction(List<GameParticipant> part_of_table) // принятие решения для действия бота (в радиусе действия хода бота)
        {
            Random rand = new Random();

            part_of_table = part_of_table.FindAll(x => x.GetType() != typeof(BoostItem)); // исключаем из возможных действий принятие протеина

            GameParticipant gamer = part_of_table.Find(x => x.GetType() == typeof(Gamer));
            List<GameParticipant> bots = part_of_table.FindAll(x => x.GetType() == typeof(Bot));
            if (gamer != null) // если бот видит игрока, то сразу убивает
            {
                return gamer.Table_position;
            }
           /* else if (bots.Count > 0) // для активации режима боты против ботов и против игрока
            {
                rand = new Random();
                int index = rand.Next(0, bots.Count - 1);
                return bots[index].Table_position;
            }*/
            else // иначе просто делает ход
            {
                rand = new Random();
                int index = rand.Next(0, part_of_table.Count - 1);
                return part_of_table[index].Table_position;
            }

        }
    }

    
    
}
