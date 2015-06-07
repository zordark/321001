using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Mapper
{
    

    //главный класс игры
    public class MapperGame
    {
        public int WIDTH { get; private set; }
        public int HEIGHT { get; private set; }

        public int COL_COUNT { get; private set; }
        public int ROW_COUNT { get; private set; }

        public int CELL_WIDTH { get; set; }
        public int CELL_HEIGHT { get; set; }

        public delegate void MoveHandle(Player plr);

        //матрица Bitmap
        //используется для отрисовки
        public Color[] Matrix { get; private set; }
        //игровое поле
        public GameObject[] Map { get; private set; }


        public List<Player> Enemies{get;set;}
        public List<Player> Allplayers{get;set;}
        public PlayerUser User{get;set;}
        public bool GameOver { get; set; }

        public MapperGame()
        {
            WIDTH = 200;
            HEIGHT = 200;

            COL_COUNT = 20;
            ROW_COUNT = 20;

            CELL_WIDTH = WIDTH / COL_COUNT;
            CELL_HEIGHT = HEIGHT / ROW_COUNT;
            
            Matrix = new Color[ WIDTH * HEIGHT ];
            Map = new GameObject[ COL_COUNT * ROW_COUNT ];

        }


        public PlayerUser GetUserInRadius(int x, int y, int radius)
        {
            x = User.X - x;
            y = User.Y - y;

            if ((Math.Abs(x) == Math.Abs(y) || (x == 0 ^ y == 0)) && radius >= Math.Sqrt(x * x + y * y))
                return User;
            return null;
        }

        public void Shot(Player pl)
        {
            int i;
            for (i = 0; i < Enemies.Count; i++)
                if (Enemies[i].Killed)
                {
                    RemovePlayer(pl);
                }

        }

        public void MovePlayer(Player plr)
        {
            MoveObjectTo(plr, plr.X + plr.NextDirectionX, plr.Y + plr.NextDirectionY);
        }

        public Color this[ int x, int y ]
        {
            get
            {
                return Matrix[y * WIDTH + x];
            }
            
            private set
            {
                Matrix[ y * WIDTH + x ] = value;
            }

        }

        public GameObject this[ short x, short y ]
        {
            get
            {
                return Map[y * COL_COUNT + x];
            }

            set
            {
                Map[y * COL_COUNT + x] = value;
                if (value != null)
                {
                    

                    for (int i = 0; i < CELL_WIDTH; i++)
                        for (int j = 0; j < CELL_HEIGHT; j++)
                            this[x * CELL_WIDTH + i, y * CELL_HEIGHT + j] = value[i, j];
                }
                else
                    ClearCell((short)x, (short)y);
            }
        }

        public void RemovePlayer(Player pl)
        {
            ClearCell((short)pl.X, (short)pl.Y);

            Allplayers.Remove(pl);
            if( Enemies.Contains(pl) )
                Enemies.Remove(pl);
        }

        public void ClearCell(short x, short y)
        {
            for (int i = 0; i < CELL_WIDTH; i++)
                for(int j = 0 ; j < CELL_HEIGHT; j++)
                {
                    this[x * CELL_WIDTH + i, y * CELL_HEIGHT+j] = Color.Transparent;
                }
        }

        public void ClearAll()
        {
            for (int i = 0; i < WIDTH; i++)
                for (int j = 0; j < HEIGHT; j++)
                    this[i, j] = Color.Transparent;
        }

        public void MoveObjectTo(GameObject obj, int x, int y)
        {
            if (obj.X != x || obj.Y != y)
            {
                this[(short)x, (short)y] = obj;
                this[(short)obj.X, (short)obj.Y] = null;

                //  ClearCell((short)obj.X, (short)obj.Y);

                obj.X = x;
                obj.Y = y;
            }

        }

        

        

       

      




        

        
    }
}
