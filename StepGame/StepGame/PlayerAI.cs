using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StepGame
{
    
    class PlayerAI:Player
    {

        
        public PlayerAI(PictureBox picture,
                     string name, string weaponName):
            base(picture, name, weaponName)
        { 
        }

        

        public override void Move()
        {
            PlayerUser user = getUserInRadius(X,Y,Weapon_.Radius);

            if (user != null)
            {
                NextMoveType = MoveType.Shot;
                user.Killed = true;
            }
            else
            {
                do
                {
                    NextDirectionX = r.Next(-10, 10);
                    NextDirectionX = NextDirectionX != 0 ? NextDirectionX / Math.Abs(NextDirectionX) :0;
                }
                while ((X + NextDirectionX) < 0 || (X + NextDirectionX) > 20);

                do
                {
                    NextDirectionY = r.Next(-10, 10);
                    NextDirectionY = NextDirectionY != 0 ? NextDirectionY / Math.Abs(NextDirectionY) : 0;
                }
                while ((Y + NextDirectionY) < 0 || (Y + NextDirectionY) > 20);



                NextMoveType = MoveType.Move;

                
            }
            base.Move();
        }
    }
}
