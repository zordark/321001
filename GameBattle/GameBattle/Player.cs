using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StepGame
{
    public enum MoveType
    {
        Move,
        Shot
    }
    public delegate PlayerUser GetPlayerUserInRadius(int x,int y,int radius);
        
    public class Player
    {

        public event StepGame.Form1.MoveHandle move;
        public event StepGame.Form1.MoveHandle shoot;
        public PictureBox Picture{get;private set;}
        public GetPlayerUserInRadius getUserInRadius;

               public int X { get; set; }
        public int Y {get;set;}

        public MoveType NextMoveType {get;set;}
        public int NextDirectionX{get;set;}
        public int NextDirectionY { get; set; }
        public bool Killed { get; set; }

        public Weapon Weapon_ { get; set; }


        public Player(PictureBox picture, 
                      string name, string weaponName)
        {
            this.Picture = picture;
            Weapon_ = Weapon.Get(weaponName);
            
            r = new Random(System.DateTime.Now.Millisecond);
            System.Threading.Thread.Sleep(10);
        }
        protected Random r ;
        
        public virtual void Move()
        {
            switch (NextMoveType)
            {
                case MoveType.Move:
                    move(this);
                    break;
                case MoveType.Shot:
                    shoot(this);
                    break;
            }
            
        }

        



    }
}
