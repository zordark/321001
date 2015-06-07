using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Mapper
{
    public class GameObject
    {
        private int Height;
        private int Width;

        public GameObject(int width, int height)
        {
            Height = height;
            Width = width;
            
        }

        public int X { get; set; }
        public int Y { get; set; }

        public Color[] BitMap { get; set; }

        public Color this[int x, int y]
        {
            get
            {
                return BitMap[y * Width + x];
            }

            private set
            {
                BitMap[y * Width + x] = value;
            }
        }
    }
}
