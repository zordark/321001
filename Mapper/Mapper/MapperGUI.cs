using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mapper
{
    public partial class MapperGUI : Form
    {
        const int WIDTH = 200;
        const int HEIGHT = 200;

        const int ROW_COUNT = 20;
        const int COL_COUNT = 20;

        private BufferedGraphics grafx;
        private BufferedGraphicsContext gContext;
        private PaintEventArgs eventArgs;
        private MapperGame game;

        private List<Rectangle> shotRadiusRectangles = new List<Rectangle>();
        private List<Rectangle> userMoveRectengles = new List<Rectangle>();
        private List<Rectangle> userShotRectengles = new List<Rectangle>();

        private int pixelWidth;
        private int pixelHeight;

        BackgroundWorker gameWorker;
        

        public MapperGUI()
        {
            InitializeComponent();

            gContext = BufferedGraphicsManager.Current;
            grafx = gContext.Allocate(mainPanel.CreateGraphics(),
                 new Rectangle(0, 0, mainPanel.Width, mainPanel.Height));
            eventArgs = new PaintEventArgs(grafx.Graphics, mainPanel.Bounds);

            pixelHeight = (int)Math.Ceiling((float)mainPanel.Height / (float)HEIGHT);
            pixelWidth = (int)Math.Ceiling((float)mainPanel.Width / (float)WIDTH);

            button1.Enabled = false;
            button2.Enabled = false;




        }


        private void InitializeGame()
        {
            game = new MapperGame();
            
            game.Enemies = new List<Player>();
            game.Allplayers = new List<Player>();
            game.User = new PlayerUser(game, Weapon.Random(), game.CELL_WIDTH, game.CELL_HEIGHT);
            
            game.GameOver = false;
            mainPanel.MouseMove += UpdateGUI;
            mainPanel.MouseClick += game.User.MouseClick;
            game.User.mapX = MapX2Cell;
            game.User.mapY = MapY2Cell;
            game.User.shoot += game.Shot;
            game.User.move += game.MovePlayer;
            game.User.add = CheckCollisionAndAddRectangles;
            game.User.getPlayer = (int x,int y) => game[(short)x,(short)y] as Player;

            game.Allplayers.Add(game.User);
            game.Enemies.Add(new PlayerAI(game, Weapon.Random(), game.CELL_WIDTH, game.CELL_HEIGHT));
            game.Enemies.Add(new PlayerAI(game, Weapon.Random(), game.CELL_WIDTH, game.CELL_HEIGHT));
            game.Enemies.Add(new PlayerAI(game, Weapon.Random(), game.CELL_WIDTH, game.CELL_HEIGHT));
            game.Enemies.Add(new PlayerAI(game, Weapon.Random(), game.CELL_WIDTH, game.CELL_HEIGHT));
            game.Enemies.Add(new PlayerAI(game, Weapon.Random(), game.CELL_WIDTH, game.CELL_HEIGHT));
            game.Allplayers.AddRange( game.Enemies);

            foreach (Player pl in  game.Enemies)
            {
                pl.getUserInRadius = game.GetUserInRadius;
                pl.move += game.MovePlayer;
                pl.shoot += game.Shot;

            }

            

            RandomSpawn();
            DrawElements(this, eventArgs);
            RenderScene();
            grafx.Render();

        }
        public void RandomSpawn()
        {
            int x, y;
            Random r = new Random();
            foreach (var p in game.Allplayers)
            {
                do
                {
                    x = r.Next(COL_COUNT);
                    y = r.Next(ROW_COUNT);

                } while (game[(short)x, (short)y] != null);

                game[(short)x, (short)y] = p;

                p.X = x;
                p.Y = y;
            }


        }

        private void UpdateGUI(object sender, MouseEventArgs e)
        {
            if (!game.GameOver)
            {
                shotRadiusRectangles.Clear();
                int senderX = e.X;
                int senderY = e.Y;
                if (sender is PictureBox)
                {
                    senderX = ((PictureBox)sender).Left;
                    senderY = ((PictureBox)sender).Top;
                }
                foreach (var player in game.Allplayers)
                {
                    if (MapX2Cell(senderX) == player.X && MapY2Cell(senderY) == player.Y)
                    {
                        shotRadiusRectangles.Add(CellCoords(player.Y, player.X));
                        for (int i = 1; i <= player.Weapon.Radius; i++)
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
                        for (int i = 1; i <= player.Weapon.Radius - 1; i++)
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


                DrawElements(mainPanel, eventArgs);
                RenderScene();
                grafx.Render();
            }



        }

        public void RenderScene()
        {
            if (game == null) return;
            SolidBrush brush = new SolidBrush(Color.Black);

            for (int i = 0; i < 200; i++)
                for (int j = 0; j < 200; j++)
                {
                    brush.Color = game[i, j];
                    FillPixel(i, j, brush);
                }
        }



        private void DrawGrid(Graphics gr)
        {
            int cellWidth = (int)(((float)mainPanel.Width) / ((float)COL_COUNT));
            int cellHeight = (int)(((float)mainPanel.Height) / ((float)ROW_COUNT));

            for (int i = 0; i < COL_COUNT + 1; i++)
            {
                gr.DrawLine(Pens.Bisque, i * cellWidth, 0, i * cellWidth, mainPanel.Height);
            }

            for (int i = 0; i < ROW_COUNT + 1; i++)
            {
                gr.DrawLine(Pens.Bisque, 0, i * cellHeight, mainPanel.Width, i * cellHeight);
            }
        }

        private void DrawElements(object sender, PaintEventArgs e)
        {

            if (!game.GameOver)
            {
                Graphics g = grafx.Graphics;
                g.Clear(Color.LawnGreen);

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

                foreach (var player in game.Enemies)
                {
                    if (user.Y + user.NextDirectionY == player.Y &&
                        user.X + user.NextDirectionX == player.X)
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

                int count = Math.Abs(user.NextDirectionX);
                if (count == 0)
                    count = Math.Abs(user.NextDirectionY);
                for (int i = 1; i < count + 1; i++)
                {
                    userShotRectengles.Add(CellCoords(user.Y + dy * i, user.X + dx * i));
                }
            }

        }

       


      

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(eventArgs);
            grafx.Render();
        }

    

        private void MoveAction(object sender, EventArgs e)
        {
            foreach (Player pl in game.Enemies)
                pl.Move();
            button2.Enabled = true;
            if (game.User.Killed)
            {
                MessageBox.Show("Вы проиграли!");
                
                button1.Enabled = false;
                button2.Enabled = false;
                game.ClearAll();
                game.GameOver = true;
                
            }
            userMoveRectengles.Clear();
            userShotRectengles.Clear();
            shotRadiusRectangles.Clear();
            DrawElements(this, eventArgs);
            RenderScene();
            
            grafx.Render();
        }

        private void MakeUserMoveAction(object sender, EventArgs e)
        {
            if (game.User.NextMoveType == MoveType.Move)
            {
                game.MovePlayer(game.User);
                game.User.NextDirectionX = game.User.NextDirectionY = 0;
            }
            else if (game.User.NextMoveType == MoveType.Shot)
            {
                Player pl =(Player) game[(short)(game.User.X + game.User.NextDirectionX), (short)(game.User.Y + game.User.NextDirectionY)];
                if (pl != null)
                {
                    game.RemovePlayer(pl);
                    game.Enemies.Remove(pl);
                    game.Allplayers.Remove(pl);
                }
                if (game.Enemies.Count == 0)
                {
                    MessageBox.Show("Вы выиграли!");

                    
                    game.ClearAll();
                    game.Enemies.Clear();
                    game.Allplayers.Clear();
                    game.GameOver = true;

                    button1.Enabled = false;
                    button2.Enabled = false;
                }


            }
            userMoveRectengles.Clear();
            userShotRectengles.Clear();
            shotRadiusRectangles.Clear();
            DrawElements(this, eventArgs);
            RenderScene();
           
            grafx.Render();

            button2.Enabled = false;
        }
        private void NewGameAction(object sender, EventArgs e)
        {
           
            InitializeGame();
            userMoveRectengles.Clear();
            userShotRectengles.Clear();
            shotRadiusRectangles.Clear();
            
            button1.Enabled = true;
            button2.Enabled = true;
            
            grafx.Render();



        }

        public void FillPixel(int x, int y, Brush brush)
        {
            grafx.Graphics.FillRectangle(brush, x * pixelWidth, y * pixelHeight, pixelWidth, pixelHeight);
        }




        Rectangle CellCoords(int row, int col)
        {
            int cellWidth = (int)(((float)mainPanel.Width) / ((float)COL_COUNT));
            int cellHeight = (int)(((float)mainPanel.Height) / ((float)ROW_COUNT));

            return new Rectangle(col * cellWidth, row * cellHeight, cellWidth, cellHeight);
        }


        int MapX2Cell(int x)
        {
            return (int)((float)x / (float)mainPanel.Width * COL_COUNT);
        }

        int MapY2Cell(int y)
        {
            return (int)((float)y / (float)mainPanel.Height * ROW_COUNT);
        }

    }
}
