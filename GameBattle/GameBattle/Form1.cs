using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StepGame
{
    public partial class Form1 : Form
    {

        public delegate void MoveHandle(Player pl);

        private List<Rectangle> shotRadiusRectangles = new List<Rectangle>();
        private List<Rectangle> userMoveRectengles = new List<Rectangle>();
        private List<Rectangle> userShotRectengles = new List<Rectangle>();

        private BufferedGraphicsContext context;
        private BufferedGraphics grafx;
        List<Player> enemies;
        List<Player> allplayers;
        PlayerUser user;
        PaintEventArgs eventArgs;
        private bool gameRunning = false;
       
           

        const int ROW_COUNT = 10;
        const int COL_COUNT = 10;

      
        
        public Form1()
        {
            InitializeComponent();
            
        }

        
        private void InitializePlayers()
        {
            enemies = new List<Player>();
            allplayers = new List<Player>();
            user = new PlayerUser(pictureBox6, "Эльф", Weapon.Random());
            //players.Add( user );
            gameRunning = true;

            tableLayoutPanel1.MouseClick += user.MouseClick;
            user.mapX = MapX2Cell;
            user.mapY = MapY2Cell;
            user.add = CheckCollisionAndAddRectangles;
            user.getPlayer = GetPlayerAtCell;

            allplayers.Add(user);
            enemies.Add( new PlayerAI( pictureBox1,  "Орк",    Weapon.Random() ));
            enemies.Add(new PlayerAI(  pictureBox2,  "Орк враг", Weapon.Random()));
            enemies.Add(new PlayerAI(  pictureBox3,  "Орк маг",   Weapon.Random()));
            enemies.Add(new PlayerAI(  pictureBox4,  "Орк убийца",   Weapon.Random()));
            enemies.Add(new PlayerAI(  pictureBox5,  "Орк кровопийца",     Weapon.Random()));
            allplayers.AddRange(enemies);
          
            foreach (Player pl in enemies)
            {
                pl.move += MovePlayer;
                pl.Picture.MouseClick += user.MouseClick;
                pl.getUserInRadius = GetUserInRadius;
                pl.shoot += Shot;
            }

            tableLayoutPanel1.Paint += DrawElements;
            
            RandomSpawn();
            

            context = BufferedGraphicsManager.Current;
            grafx = context.Allocate(tableLayoutPanel1.CreateGraphics(),
                 new Rectangle(0, 0, tableLayoutPanel1.Width, tableLayoutPanel1.Height));
            eventArgs = new PaintEventArgs(grafx.Graphics, tableLayoutPanel1.Bounds);

           

        }


        PlayerUser GetUserInRadius(int x,int y,int radius)
        {
            x = user.X - x;
            y = user.Y - y;

            if ((Math.Abs(x) == Math.Abs(y) || (x == 0 ^ y == 0) )&& radius >= Math.Sqrt(x * x + y * y))
                return user;
            return null;
        }

        void Shot(Player pl)
        {
            int i;
            for (i = 0; i < enemies.Count; i++)
                if (enemies[i].Killed) {
                    tableLayoutPanel1.Controls.Remove(enemies[i].Picture);
                }

        }

        Player GetPlayerAtCell(int x, int y)
        {
            foreach (var player in enemies)
            {
                if (player.X == x && player.Y == y)
                    return player;
            }
              
            return null;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (gameRunning)
            {
                DrawElements(this, eventArgs);
                grafx.Render();
            }
        }

        Rectangle CellCoords(int row, int col)
        {
            int cellWidth = (int)(((float)tableLayoutPanel1.Width) / ((float)COL_COUNT));
            int cellHeight = (int)(((float)tableLayoutPanel1.Height)/((float)ROW_COUNT));

            return new Rectangle(col * cellWidth, row * cellHeight, cellWidth, cellHeight);
        }

        void DrawElements(object sender, PaintEventArgs e)
        {

            if (gameRunning)
            {
                Graphics g = grafx.Graphics;
                g.Clear(Color.AliceBlue);

                DrawGrid(g);
                Pen p = new Pen(Color.Black, 3);
                foreach (var rectangle in shotRadiusRectangles)
                {
                    g.DrawRectangle(p, rectangle);
                }
                p = new Pen(Color.Green, 3);
                foreach (var rectangle in userMoveRectengles)
                {
                    g.DrawRectangle(p, rectangle);
                }
                p = new Pen(Color.Red, 3);
                foreach (var rectangle in userShotRectengles)
                {
                    g.DrawRectangle(p, rectangle);
                }
            }
        
        }

        private void CheckCollisionAndAddRectangles(PlayerUser user)
        {
            userMoveRectengles.Clear();
            userShotRectengles.Clear();
            

            if (user.NextMoveType == MoveType.Move)
            {
                bool isOnPlayer = false;

                foreach(var player in enemies)
                {
                    if(user.Y + user.NextDirectionY == player.Y &&
                        user.X + user.NextDirectionX == player.X )
                    {
                        isOnPlayer = true;
                        break;
                    }
                }

                if (!isOnPlayer)
                    userMoveRectengles.Add(CellCoords(user.Y + user.NextDirectionY, user.X + user.NextDirectionX));
                else
                {
                    user.NextDirectionY = user.NextDirectionX = 0;
                }

            }
            else
            {
                int dx = Math.Sign(user.NextDirectionX);
                int dy = Math.Sign(user.NextDirectionY);
                
                int count =  Math.Abs(user.NextDirectionX);
                if (count==0)
                    count = Math.Abs(user.NextDirectionY);
                for (int i = 1; i < count+1; i++)
                {
                    userShotRectengles.Add(CellCoords(user.Y + dy * i, user.X + dx * i));
                }
            }

        }

        public void RandomSpawn()
        {
            int x, y;
            Random r = new Random();
            foreach (var p in allplayers)
            {
                do
                {
                    x = r.Next(COL_COUNT);
                    y = r.Next(ROW_COUNT);

                } while (tableLayoutPanel1.GetControlFromPosition(x, y) != null);

                tableLayoutPanel1.Controls.Add(p.Picture, x, y);
                
                p.X = x;
                p.Y = y;
            }

           
        }

        public void MovePlayer(Player plr)
        {
            tableLayoutPanel1.Controls.Add(plr.Picture, plr.X += plr.NextDirectionX, plr.Y += plr.NextDirectionY);
        }
        
        int MapX2Cell(int x)
        {
            return (int)((float)x / (float) tableLayoutPanel1.Width * COL_COUNT); 
        }

        int MapY2Cell(int y)
        {
            return (int)((float)y / (float) tableLayoutPanel1.Height * ROW_COUNT); 
        }

        int i = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Player pl in enemies)
                pl.Move();
            button2.Enabled = true;
            if (user.Killed)
            {
                MessageBox.Show("Вы проиграли!");
                tableLayoutPanel1.Controls.Clear();
                enemies.Clear();
                allplayers.Clear();
                button1.Enabled = false;
                button2.Enabled = false;
                gameRunning = false;
            }
            userMoveRectengles.Clear();
            userShotRectengles.Clear();
            shotRadiusRectangles.Clear();
            grafx.Render();
        }

        private void DrawRadiusIfPlayer(object sender, MouseEventArgs e)
        {
            if (gameRunning)
            {
                shotRadiusRectangles.Clear();
                int senderX = e.X;
                int senderY = e.Y;
                if (sender is PictureBox)
                {
                    senderX = ((PictureBox)sender).Left;
                    senderY = ((PictureBox)sender).Top;
                }
                foreach (var player in allplayers)
                {
                    if (MapX2Cell(senderX) == player.X && MapY2Cell(senderY) == player.Y)
                    {
                        shotRadiusRectangles.Add(CellCoords(player.Y, player.X));
                        for (int i = 1; i <= player.Weapon_.Radius; i++)
                        {
                            if (i + player.X < COL_COUNT)
                                shotRadiusRectangles.Add(CellCoords(player.Y, i + player.X));
                            if (player.X - i < COL_COUNT)
                                shotRadiusRectangles.Add(CellCoords(player.Y, player.X - i));
                            if (i + player.Y < ROW_COUNT)
                                shotRadiusRectangles.Add(CellCoords(player.Y + i, player.X));
                            if (player.Y - i < ROW_COUNT)
                                shotRadiusRectangles.Add(CellCoords(player.Y - i, player.X));
                        }
                        for (int i = 1; i <= player.Weapon_.Radius - 1; i++)
                        {
                            if (i + player.X < COL_COUNT && i + player.Y < ROW_COUNT)
                                shotRadiusRectangles.Add(CellCoords(i + player.Y, i + player.X));
                            if (i + player.X < COL_COUNT && -i + player.Y < ROW_COUNT)
                                shotRadiusRectangles.Add(CellCoords(-i + player.Y, i + player.X));
                            if (-i + player.X < COL_COUNT && i + player.Y < ROW_COUNT)
                                shotRadiusRectangles.Add(CellCoords(i + player.Y, -i + player.X));
                            if (-i + player.X < COL_COUNT && -i + player.Y < ROW_COUNT)
                                shotRadiusRectangles.Add(CellCoords(-i + player.Y, -i + player.X));
                        }
                    }


                }


                DrawElements(tableLayoutPanel1, new PaintEventArgs(grafx.Graphics, tableLayoutPanel1.Bounds));
                grafx.Render();
            }
            
        }

        private void DrawGrid(Graphics gr)
        {
            int cellWidth = (int)(((float)tableLayoutPanel1.Width) / ((float)COL_COUNT));
            int cellHeight = (int)(((float)tableLayoutPanel1.Height) / ((float)ROW_COUNT));

            for(int i=0;i<COL_COUNT+1;i++)
            {
                gr.DrawLine(Pens.RoyalBlue, i * cellWidth, 0, i * cellWidth, tableLayoutPanel1.Height);
            }   
 
            for(int i=0;i<ROW_COUNT+1;i++)
            {
                gr.DrawLine(Pens.RoyalBlue, 0, i * cellHeight, tableLayoutPanel1.Width, i * cellHeight);
            }
        }

        private void OnPanelPaint(object sender, PaintEventArgs e)
        {
            OnPaint(eventArgs);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (user.NextMoveType == MoveType.Move )
            {
                MovePlayer(user);
                user.NextDirectionX = user.NextDirectionY = 0;
            }
            else if (user.NextMoveType == MoveType.Shot)
            {
                Player pl = GetPlayerAtCell(user.X + user.NextDirectionX, user.Y + user.NextDirectionY);
                if (pl != null)
                {
                    tableLayoutPanel1.Controls.Remove(pl.Picture);
                    enemies.Remove(pl);
                    allplayers.Remove(pl);
                }
                if (enemies.Count == 0)
                {
                    MessageBox.Show("Вы выиграли!");
                    tableLayoutPanel1.Controls.Clear();
                    enemies.Clear();
                    allplayers.Clear();
                    gameRunning = false;
                }
                
                
            }
            userMoveRectengles.Clear();
            userShotRectengles.Clear();
            shotRadiusRectangles.Clear();
            grafx.Render();

            button2.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ваш герой эльф! Победите орков!!!");
            tableLayoutPanel1.Controls.Clear();
            InitializePlayers();
            userMoveRectengles.Clear();
            userShotRectengles.Clear();
            shotRadiusRectangles.Clear();
            tableLayoutPanel1.Invalidate();
            button1.Enabled = true;
            button2.Enabled = true;
            foreach (var player in allplayers)
                tableLayoutPanel1.Controls.Add(player.Picture, player.X, player.Y);
            //grafx.Render();
            

           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" ПКМ - ход на одну соседнюю клетку. \n ЛКМ - Атака в зависимости от радиуса оружия \n Наведите на героя, чтобы увидеть радиус атаки. \n Если вы попали под радиус атаки врага, вы проиграли!");
        }
    }
}
