using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media;

namespace MindGame
{
   public class GameParticipant
    {
        private string name;

        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }


        private  Rectangle shape;

        public virtual Rectangle Shape
        {
            get { return shape; }
            set { shape = value; }
        }
        private  Point table_position;

        public virtual Point Table_position
        {
            get { return table_position; }
            set
            {
                table_position = value;
            }
        }

        public GameParticipant(string name_)
        {
            Name = name_;
           
        }
       
    }
}
