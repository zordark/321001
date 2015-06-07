using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StepGame
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

        public PlayerUser(PictureBox pucture,
                          string name, string weaponName):
            base(pucture, name, weaponName)
        {  }


       

        public void MouseClick(object sender, MouseEventArgs args)
        {
            if(args.Clicks > 0)
            {
                int senderX = args.X;
                int senderY = args.Y;

                if (sender is PictureBox)
                {
                    senderX = ((PictureBox)sender).Left;
                    senderY = ((PictureBox)sender).Top;

                   
                }

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

                    if ( len <= Weapon_.Radius &&
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
