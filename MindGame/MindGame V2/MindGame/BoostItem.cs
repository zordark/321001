using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MindGame
{
    class BoostItem: GameParticipant  
    {

        int value_increase;

        public int Value_increase
        {
            get { return value_increase; }
            set { value_increase = value; }
        }

        public BoostItem(int value_increase_)
            : base("Увеличитель Ходов")
        {
            Value_increase = value_increase_;  // количество ходов, на которое можно увеличить протеином
            Uri uriKind = new Uri("Protein.png", UriKind.Relative);
            BitmapImage bi = new BitmapImage(uriKind);

            ImageBrush ib = new ImageBrush(bi);

            ib.Stretch = Stretch.Fill;
            

            Shape = new Rectangle();
            Shape.Height = 60;
            Shape.Width = 60;

            Shape.Stroke = Brushes.Transparent;
            Shape.Fill = ib;
          
        }


    }
}
