using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MindGame
{
    class Gun
    {
        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        int range;

        public int Range
        {
            get { return range; }
            set { range = value; }
        }
        public Gun(string name_, int range_)
        {
            name = name_;
            range = range_;
        }
    }
}
