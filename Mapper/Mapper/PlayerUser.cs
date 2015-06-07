using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mapper
{
    public delegate int MapXtoColumn(int x);
    public delegate int MapYtoColumn(int y);
    public delegate void AddRectanglesToDraw(PlayerUser user);
    public delegate Player GetPlayerAtCell(int x,int y);

    public class PlayerUser:Player
    {
        public MapXtoColumn mapX;
        public MapYtoColumn mapY;
        public AddRectanglesToDraw add;
        public GetPlayerAtCell getPlayer;

        public PlayerUser(MapperGame game, string weaponName, int width, int height) :
            base(game, weaponName, width,height)
        {  

            Color o = Color.LightGreen;
            Color w = Color.White;
            Color b = Color.Black;
            Color r = Color.Red;
            BitMap = new Color[] {  o,o,o,o,o,o,o,o,o,o,
                                    o,o,o,b,b,b,b,o,o,o,
                                    o,o,b,w,w,w,w,b,o,o,
                                    o,o,w,b,w,w,b,w,o,o,
                                    o,o,o,w,w,w,w,o,o,o,
                                    o,b,b,b,b,b,b,b,b,o,
                                    o,o,o,o,b,b,b,o,o,o,
                                    o,o,o,b,b,b,b,b,o,o,
                                    o,o,b,b,o,o,o,b,b,o,
                                    o,o,b,b,o,o,o,b,b,o,
                                  };
        
        }


       

        public void MouseClick(object sender, MouseEventArgs args)
        {
            if(args.Clicks > 0)
            {
                int senderX = args.X;
                int senderY = args.Y;

               
                if (args.Button == MouseButtons.Right)
                {

                    NextMoveType = MoveType.Move;
                    NextDirectionX = mapX(senderX) - X;
                    NextDirectionY = mapY(senderY) - Y;


                    NextDirectionX = Math.Sign(NextDirectionX);
                    NextDirectionY =Math.Sign( NextDirectionY);

                    add(this);
                    
                }
                else if (args.Button == MouseButtons.Left)
                {
                    NextMoveType = MoveType.Shot;
                    NextDirectionX = mapX(senderX) - X;
                    NextDirectionY = mapY(senderY) - Y;



                    int len =(int)Math.Sqrt(NextDirectionX * NextDirectionX + NextDirectionY * NextDirectionY);

                    if ( len <= Weapon.Radius &&
                         (Math.Abs(NextDirectionX) == Math.Abs(NextDirectionY)||
                         (NextDirectionX == 0 ^ NextDirectionY==0)) )
                    {
                       
                        add(this);
                    }

                   

                }
            }


        }
    }
}
