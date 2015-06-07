using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BugRacer
{
    public delegate void HandleFinishedBug(Bug bug);
    //Участник забега
    public class Bug 
    {
        private int trackLength;
        private int startPosition;

        public int Number { get; set; }
        public string Name { get; set; }
        public PictureBox Picture { get; private set; }

        //профессионализм дает участнику дополнительную скорость
        public int Professionality { get; private set; }

        //удача дает преимущество если >0 и ухудшает результат при <0
        public int Succsess { get; set; }

        //Участник пришедший раньше остальных ждет их
        public bool IsFinished{get;set;}


        public event HandleFinishedBug finish;


        public Bug(string name,int number, int trackLength, PictureBox picture,int professionality)
        {
            Number = number;
            Name = name;
            IsFinished = false ;
            this.trackLength = trackLength;
            Picture = picture;
            startPosition = picture.Left;
            Professionality = professionality;
        }

        //движение к финишу,
        //при дохождении учатника к финишу, он ждет остальных
        public void Move(int stepSize)
        {
            if(!IsFinished)
                Picture.Left += stepSize;
        }

      
        //возвращаеься в начальное положение
        public void MoveToStart()
        {
            Picture.Left = startPosition;
            IsFinished = false;
        }

        //генерация события дохождения до финиша
        public void RaiseEventIfFinished()
        {
            if (!IsFinished && Picture.Right >= trackLength)
            {
                finish(this);
                IsFinished = true;
            }
        }

        public override bool Equals(object obj)
        {
            return object.ReferenceEquals(this, obj);
        }

        public override int GetHashCode()
        {
            return Number;
        }

      
    }
}
