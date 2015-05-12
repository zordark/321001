using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace BugRunner
{
    class Bug
    {



        static int current_number;

        public int Current_number
        {
            get { return current_number; }
            set { current_number = value; }
        }



        int number;

        public int Number
        {
            get { return number; }
            set { number = value; }
        }

        PictureBox image;

        public PictureBox Image
        {
            get { return image; }
            set { image = value; }
        }

        Point start_position;

        public Point Start_position
        {
            get { return start_position; }
            set { start_position = value; }
        }

        Point current_position;

        public Point Current_position
        {
            get { return current_position; }
            set { current_position = value; }
        }

        public void ReturnToStart()
        {
            current_position = start_position;
            image.Location = current_position;
        }
        public void ToMove(int steps)
        {
            current_position.X += steps * 20;
            image.Location = current_position;
        }

        public Bug(PictureBox image_,int y_position)
        {
            image = image_;
            start_position = new Point(12, y_position);
            current_position = start_position;
            number=++Current_number;
        }


    }
}
