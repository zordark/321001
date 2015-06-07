using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mapper
{
    public enum MoveType
    {
        Move,
        Shot
    }
    public delegate PlayerUser GetPlayerUserInRadius(int x,int y,int radius);
        
    public class Player : GameObject
    {

        public event MapperGame.MoveHandle move;
        public event MapperGame.MoveHandle shoot;

        public GetPlayerUserInRadius getUserInRadius;
        private MapperGame game;
        public MoveType NextMoveType {get;set;}
        public int NextDirectionX{get;set;}
        public int NextDirectionY { get; set; }
        public bool Killed { get; set; }

        public Weapon Weapon { get; set; }


        public Player(MapperGame game, string weaponName,int width,int height)
            :base(width,height)
        {
            this.game  = game;
            Weapon = Weapon.Get(weaponName);
            
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
