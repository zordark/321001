using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mapper
{
    
    class PlayerAI:Player
    {
        MapperGame game;

        public PlayerAI(MapperGame game, string weaponName, int width, int height) :
            base(game, weaponName, width, height)
        {
            Color o = Color.LightGreen;
            Color w = Color.White;
            Color b = Color.Black;
            Color r = Color.Red;
            BitMap = new Color[] {  o,o,o,o,o,o,o,o,o,o,
                                    o,o,o,b,b,b,b,o,o,o,
                                    o,o,b,w,w,w,w,b,o,o,
                                    o,o,w,r,w,w,r,w,o,o,
                                    o,o,o,w,w,w,w,o,o,o,
                                    o,r,r,r,r,r,r,r,r,o,
                                    o,o,o,o,r,r,r,o,o,o,
                                    o,o,o,r,r,r,r,r,o,o,
                                    o,o,r,r,o,o,o,r,r,o,
                                    o,o,r,r,o,o,o,r,r,o,
                                  };

            this.game = game;
        }

        

        public override void Move()
        {
            PlayerUser user = getUserInRadius(X,Y,Weapon.Radius);

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
                    NextDirectionX = NextDirectionX != 0 ? NextDirectionX / Math.Abs(NextDirectionX) : 0;
                }
                while ((X + NextDirectionX) < 0 || (X + NextDirectionX) >= 20);

                do
                {
                    NextDirectionY = r.Next(-10, 10);
                    NextDirectionY = NextDirectionY != 0 ? NextDirectionY / Math.Abs(NextDirectionY) : 0;
                }
                while (((Y + NextDirectionY) < 0 || (Y + NextDirectionY) >= 20) 
                    || game[(short)(X + NextDirectionX), (short)(Y + NextDirectionY)] != null);



                NextMoveType = MoveType.Move;

                
            }
            base.Move();
        }
    }
}
