using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Threading;
using System.ComponentModel;

namespace MindGame
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            rand = new Random();
            BlockUI();
          
            
           
        }

        double FieldWidth, FieldHeight;

        GameParticipant[,] gameField; 
        Gamer player;
        List<Gamer> bots;
        List<BoostItem> items;
        Gun current_gun;
        Random rand;

      
        int CountOfBot = 5;
        int CountOfItems = 3;

        BackgroundWorker[] bots_thread;


        Mutex mutex = new Mutex(false, "");
      
        void CreateGame() // создаем игру и заполняем игровую матрицу
        {


            GameField.Children.Clear();


            //////////////////////////////
            CreateGridOfHint();
            FieldHeight = GameField.Height;
            FieldWidth = GameField.Width;
            gameField = new GameParticipant[10, 10];

            for (int i = 0; i < gameField.GetLength(0); i++)
            {
                for (int j = 0; j < gameField.GetLength(1); j++)
                {
                    gameField[i, j] = new EmptyCell(new Point(i, j));
                }
            }


            //////////////////////////////////
            current_gun = new Gun("пистолет", 1); // создаем игрока
            player = new Gamer("Игрок", current_gun);

            RandomObjectPosition(player);
            player.Max_step = 1;
            AddToGame(player);
            ///////////////////////////////////

            items = new List<BoostItem>();



            for (int i = 0; i < CountOfItems; i++) // создаем протеин
            {
                var item = new BoostItem(2);

                bool succes = false; // показывает успешно ли был добавлен обьект (идет расчет добавления обьектов так, чтобы они не стояли в одном шагу от друг другу)
                while (!succes)
                {

                    RandomObjectPosition(item);
                    succes = AddToGame(item);
                }
                items.Add(item);
            }
           

            ////////////////////////////////
            bots_thread = new BackgroundWorker[CountOfBot]; // создаем поток для каждого бота
            bots = new List<Gamer>();
            Bot.ResetIndex();
            for (int i = 0; i < CountOfBot; i++)
            {
               var bot = new Bot();
                bool succes = false;
                bots_thread[i] = new BackgroundWorker();
                bots_thread[i].DoWork+=new DoWorkEventHandler(MainWindow_DoWork);


                while(!succes)  
                {
                  
                    RandomObjectPosition(bot);
                    succes = AddToGame(bot);
                }
                bots.Add(bot);
            }




        }

        bool CheckAround(Point table_pos) // проверка есть ли вокруг заданной ячейки занятые
        {
            bool result = false;
            if (table_pos.X != 0 && gameField[(int)table_pos.X - 1, (int)table_pos.Y].GetType() != typeof(EmptyCell)) result = true;
            if (table_pos.X != 9 && gameField[(int)table_pos.X + 1, (int)table_pos.Y].GetType() != typeof(EmptyCell)) result = true;
            if (table_pos.Y != 0 && gameField[(int)table_pos.X, (int)table_pos.Y - 1].GetType() != typeof(EmptyCell)) result = true;
            if (table_pos.Y != 9 && gameField[(int)table_pos.X, (int)table_pos.Y + 1].GetType() != typeof(EmptyCell)) result = true;

            if (table_pos.X != 0 && table_pos.Y != 0 && gameField[(int)table_pos.X - 1, (int)table_pos.Y - 1].GetType() != typeof(EmptyCell)) result = true;
            if (table_pos.X != 9 && table_pos.Y != 9 && gameField[(int)table_pos.X + 1, (int)table_pos.Y + 1].GetType() != typeof(EmptyCell)) result = true;
            if (table_pos.X != 9 && table_pos.Y != 0 && gameField[(int)table_pos.X + 1, (int)table_pos.Y - 1].GetType() != typeof(EmptyCell)) result = true;
            if (table_pos.X != 0 && table_pos.Y != 9 && gameField[(int)table_pos.X - 1, (int)table_pos.Y + 1].GetType() != typeof(EmptyCell)) result = true;

            return result;
        }

        List<GameParticipant> CellAroundPosition(Point table_pos,int range) // возвращаем списком ячейки вокруг позиции с заданным радиусом 
        {
            List<GameParticipant> cells = new List<GameParticipant>();
           
            for (int i = (int)table_pos.X - range; i <= (int)table_pos.X + range; i++)
            {
                if (i <= 9 && i >= 0 && i != (int)table_pos.X ) cells.Add(gameField[i, (int)table_pos.Y]);
            }

           
            for (int i = (int)table_pos.Y - range; i <= (int)table_pos.Y + range; i++)
            {
                if (i <= 9 && i >= 0 && i != (int)table_pos.Y ) cells.Add(gameField[(int)table_pos.X,i]);
            }


            for (int i = (int)table_pos.X - range; i <= (int)table_pos.X + range; i++)
            {
                for (int j = (int)table_pos.Y - range; j <= (int)table_pos.Y + range; j++)
                {
                    if (i <= 9 && i >= 0 && i != (int)table_pos.X && j <= 9 && j >= 0 && j != (int)table_pos.Y && Math.Abs(j - (int)table_pos.Y) == Math.Abs(i - (int)table_pos.X)) cells.Add(gameField[i, j]);                   
                }
                
            }



         
            return cells;
        }

        bool AddToGame(GameParticipant obj) // пытаемся добавить обьекты в игру 
        {
            bool succes = false;
            if (gameField[(int)obj.Table_position.X, (int)obj.Table_position.Y] .GetType() == typeof(EmptyCell) && !CheckAround(obj.Table_position)) // мы можем его поставить если ячейка пустая и рядом никого нет
            {
                gameField[(int)obj.Table_position.X, (int)obj.Table_position.Y] = obj;
                
                Canvas canvas = GameField;
                Point pos = ScreenPosition(obj.Table_position);
                Canvas.SetTop(obj.Shape, 0);
                Canvas.SetLeft(obj.Shape, 0);

                obj.Shape.Margin = new Thickness(pos.X, pos.Y, 0, 0);

                if (canvas.Children.Contains(obj.Shape))canvas.Children.Remove(obj.Shape);
                
                canvas.Children.Add(obj.Shape); 

               succes = true;
            }
            return succes;


        }



        void RandomObjectPosition(GameParticipant obj)
        {
            obj.Table_position = new Point(rand.Next(0, 10), rand.Next(0, 10));
        }

       




        Point IndexOfPosition(Point mouse_Position) // переводим координаты экрана в координаты на поле
        {
            Point indexOfPosition = new Point();
            indexOfPosition.X = Math.Floor(mouse_Position.X / (FieldWidth / 10));
            indexOfPosition.Y = Math.Floor(mouse_Position.Y / (FieldHeight / 10));
            return indexOfPosition;
        }

        Point ScreenPosition(Point indexPosition) // переводим координаты на поле в координаты на экране
        {
            Point position = new Point();
            position.X = indexPosition.X * (FieldWidth / 10);
            position.Y = indexPosition.Y * (FieldHeight / 10);
            return position;
        }


        void AnimationMove(GameParticipant obj, Point new_Index_Position) // воспроизводим анимацию передвижения
        {
            Dispatcher.BeginInvoke(new Action(delegate // принужденно вызываем исполнение потока
            {

                Point new_Position = ScreenPosition(new_Index_Position);
                var animation = new ThicknessAnimation();
                animation.From = new Thickness(obj.Shape.Margin.Left, obj.Shape.Margin.Top, 0, 0);
                animation.To = new Thickness(new_Position.X, new_Position.Y, 0, 0);
                animation.Duration = TimeSpan.FromSeconds(1);
                obj.Shape.BeginAnimation(MarginProperty, animation);
            }));
        }


  
        void AnimationDeath(GameParticipant obj)
        {

            Dispatcher.BeginInvoke(new Action(delegate
           {


               var animation2 = new ThicknessAnimation();
               animation2.From = new Thickness(obj.Shape.Margin.Left, obj.Shape.Margin.Top, 0, 0);
               animation2.To = new Thickness(obj.Shape.Margin.Left + 26, obj.Shape.Margin.Top + 26, 0, 0);
               animation2.BeginTime = TimeSpan.FromSeconds(0.5);
               animation2.Duration = TimeSpan.FromSeconds(0.5);
               obj.Shape.BeginAnimation(MarginProperty, animation2);


               var animation = new DoubleAnimation();
               animation.From = obj.Shape.Height;
               animation.To = 0;
               animation.BeginTime = TimeSpan.FromSeconds(0.5);
               animation.Duration = TimeSpan.FromSeconds(0.5);
               obj.Shape.BeginAnimation(HeightProperty, animation);
               obj.Shape.BeginAnimation(WidthProperty, animation);
           }));
        }

        void ShootAnimation(GameParticipant obj, GameParticipant obj2)
        {
            Dispatcher.BeginInvoke(new Action(delegate
            {

                Point start = ScreenPosition(obj.Table_position);
                Point end = ScreenPosition(obj2.Table_position);


                Ellipse bullet = new Ellipse();

                bullet.Height = 10;
                bullet.Width = 10;

                start.X += (60 - bullet.Width) / 2;
                start.Y += (60 - bullet.Height) / 2;

                end.X += (60 - bullet.Width) / 2;
                end.Y += (60 - bullet.Height) / 2;

                bullet.Fill = Brushes.Black;

                Canvas.SetLeft(bullet, 0);
                Canvas.SetTop(bullet, 0);

                GameField.Children.Add(bullet);

                var animation = new ThicknessAnimation();
                animation.From = new Thickness(start.X, start.Y, 0, 0);
                animation.To = new Thickness(end.X, end.Y, 0, 0);
                animation.Duration = TimeSpan.FromSeconds(0.5);
                bullet.BeginAnimation(MarginProperty, animation);


                var animation2 = new DoubleAnimation();
                animation2.From = bullet.Height;
                animation2.To = 0;
                animation2.BeginTime = TimeSpan.FromSeconds(0.5);
                animation2.Duration = TimeSpan.FromSeconds(0.1);
                bullet.BeginAnimation(HeightProperty, animation2);
                bullet.BeginAnimation(WidthProperty, animation2);

                AnimationDeath(obj2);
            }));

        }


        void ItemAnimation(BoostItem item)
        {

            GameFieldMessage("+" + item.Value_increase + " шага", 0.5, 1, new Point(item.Shape.Margin.Left, item.Shape.Margin.Top-40), 24, true);

            var animation = new ThicknessAnimation();
            animation.From = new Thickness(item.Shape.Margin.Left, item.Shape.Margin.Top, 0, 0);
            animation.To = new Thickness(item.Shape.Margin.Left, item.Shape.Margin.Top - 50, 0, 0);
            animation.BeginTime = TimeSpan.FromSeconds(0.7);
            animation.Duration = TimeSpan.FromSeconds(0.5);
            item.Shape.BeginAnimation(MarginProperty, animation);


            var animation2 = new DoubleAnimation();
            animation2.From = 1;
            animation2.To = 0;
            animation2.BeginTime = TimeSpan.FromSeconds(0.7);
            animation2.Duration = TimeSpan.FromSeconds(0.5);
            item.Shape.BeginAnimation(OpacityProperty, animation2);

            
           
        }



        private void PickCell(object sender, MouseButtonEventArgs e) // выбираем клетку мышкой, преобразуем координаты и получаем позицию в нашей матрице
        {

            Point indexPos = IndexOfPosition(e.MouseDevice.GetPosition(GameField));
            PlayerMoving(indexPos);
        }


        void PlayerMoving(Point cell_for_action) // делаем ход
        {
            try
            {
                var cell = gameField[(int)cell_for_action.X, (int)cell_for_action.Y];
                if (cell.GetType() == typeof(EmptyCell))
                {
                    MakeMove(cell_for_action, player); // если пустая клетка - то ходмм
                }
                else if (cell.GetType() == typeof(Bot))
                {
                    Shoot(player, (Gamer)cell); // если враг, то атакуем
                }
                else if (cell.GetType() == typeof(BoostItem))
                {
                    EatingItem((BoostItem)cell); // если протеин - едим и переходим на позицию протеина
                    MakeMove(cell_for_action, player);
                }
                Steps_Change(); // соотвественно уменьшаем количество шагов
            }
            catch (FarStepException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        void EatingItem(BoostItem item) //едим протеин, анимируем, и он собстно удаляется 
        {
            player.Eat(item);
            ItemAnimation(item);
            KillFromTable(item);
        }
        

        void BotMoving( Bot bot) // 
        {
            Point indexPos = ((Bot)bot).MakeAction(CellAroundPosition(bot.Table_position,bot.Max_step));//выбранная ботом, в зависимости от условий, ячейка для действия

            var cell = gameField[(int)indexPos.X, (int)indexPos.Y];

            if (cell.GetType() == typeof(EmptyCell))
            {
                MakeMove(indexPos, bot);
            }
            else if (cell.GetType() == typeof(Gamer))
            {
                Shoot(bot, (Gamer)cell);
            }
        }

        void Shoot(Gamer ch, Gamer deathman)
        {
            ch.Shoot(deathman.Table_position);
            ShootAnimation(ch, deathman);
            KillFromTable(deathman);
            Dispatcher.BeginInvoke(new ThreadStart(delegate
          {

              GameFieldMessage(ch.Name +" убил " + deathman.Name + " из " + ch.Weapon.Name, 0, 2, new Point(deathman.Shape.Margin.Left, deathman.Shape.Margin.Top), 15, true);
              if (bots.Count == 0 || player == null)
              {
                  GameActive = false;
                  GameFieldMessage(ch.Name + " победил !", 2, 1, new Point(120, 250), 60, false);
              }
          }));
        }

        void GameFieldMessage(string text, double start, double duration,Point positon,int size,bool animation)
        {
            Label win = new Label();
            win.Content = text;
            win.Foreground = Brushes.Red;
            win.FontSize = size;
            Canvas.SetLeft(win, 0);
            Canvas.SetTop(win, 0);
            win.Margin = new Thickness(positon.X, positon.Y, 0, 0);
            GameField.Children.Add(win);


            var animation2 = new DoubleAnimation();
            animation2.From = 1;
            animation2.To = 0;
            animation2.BeginTime = TimeSpan.FromSeconds(start);
            animation2.Duration = TimeSpan.FromSeconds(duration);
            win.BeginAnimation(OpacityProperty, animation2);

            if (animation)
            {
                var animation3 = new ThicknessAnimation();
                animation3.From = new Thickness(positon.X, positon.Y, 0, 0);
                animation3.To = new Thickness(positon.X, positon.Y - 100, 0, 0);
                animation3.BeginTime = TimeSpan.FromSeconds(start);
                animation3.Duration = TimeSpan.FromSeconds(duration);
                win.BeginAnimation(MarginProperty, animation3);
            }
         

        }



        Rectangle[,] GridHint;

        void CreateGridOfHint() // создаем поле из клеток для подсвечивания информации о возможности хода
        {
            GridHint = new Rectangle[10, 10];
            for (int i = 0; i < GridHint.GetLength(0); i++)
            {
                for (int j = 0; j < GridHint.GetLength(1); j++)
                {
                    Rectangle rect = new Rectangle();
                    rect.Height = 60;
                    rect.Width = rect.Height = 60;
                    rect.Fill = Brushes.Red;
                    rect.Opacity = 0.0;
                    rect.MouseEnter += new MouseEventHandler(LightOnHint);
                    rect.MouseLeave += new MouseEventHandler(LightOutHint);
                    Canvas.SetLeft(rect, 0);
                    Canvas.SetTop(rect,0);
                    rect.Margin = new Thickness(i * 60, j * 60, 0, 0);
                    GridHint[i, j] = rect;
                    GameField.Children.Add(rect);
                }

            }
        }


        void LightOnHint(object sender, MouseEventArgs e) // при наведении на клетку поля происходит подсвечивание
        {
            if (player != null)
            {
                Point pos = IndexOfPosition(e.MouseDevice.GetPosition(GameField));
                if (pos.X < 0) pos.X = 0;
                if (pos.Y < 0) pos.Y = 0;
                if (pos.X > 9) pos.X = 9;
                if (pos.Y > 9) pos.Y = 9;

                var current_cell = (Rectangle)sender;


                var cell = CellAroundPosition(player.Table_position, player.Buffer_steps).Find(x => x.Table_position == pos);

                if (cell != null && player.Buffer_steps > 0)
                {
                    if (cell.GetType() == typeof(Bot)) current_cell.Fill = Brushes.Black;
                    if (cell.GetType() == typeof(EmptyCell)) current_cell.Fill = Brushes.White; // если клетка возможна для хода, то подсвечиваем белым цветом
                }
                else
                {
                    if (pos == player.Table_position) current_cell.Fill = Brushes.Yellow; // если навели на самого игрока, то подсвечиваем желтым
                    else current_cell.Fill = Brushes.Red;
                }

                var animation2 = new DoubleAnimation();
                animation2.From = current_cell.Opacity;
                animation2.To = 0.6;

                animation2.Duration = TimeSpan.FromSeconds(0.5);
                current_cell.BeginAnimation(OpacityProperty, animation2);
            }


        }

        void LightOutHint(object sender, MouseEventArgs e) // анимация затухания подсветки при убирании курсора с клетки
        {
            var current_cell = (Rectangle)sender;
            var animation2 = new DoubleAnimation();
            animation2.From = current_cell.Opacity;
            animation2.To = 0;

            animation2.Duration = TimeSpan.FromSeconds(0.5);
            current_cell.BeginAnimation(OpacityProperty, animation2);
        }


        void MakeMove(Point new_pos,Gamer ch) // делаем ход, заменяем старую позицию на пустую клетку, а новую на персонажа и анимируем
        {
            Point old_pos = ch.Table_position;
            ch.MakeStep(new_pos);
            gameField[(int)old_pos.X, (int)old_pos.Y] = new EmptyCell(old_pos);
            gameField[(int)new_pos.X, (int)new_pos.Y] = ch;
            Point new_Position = ScreenPosition(ch.Table_position);
            AnimationMove(ch, new_pos);
        }

        void KillFromTable(GameParticipant obj) // при убийстве удаляем убитый или сьеденный обьект, и ставим на их место пустую клетку
        {

            if (obj.GetType() == typeof(Gamer)) player = null;
            if(obj.GetType() == typeof(Bot))bots.Remove((Bot)obj);
            if (obj.GetType() == typeof(BoostItem)) items.Remove((BoostItem)obj);
            gameField[(int)obj.Table_position.X, (int)obj.Table_position.Y] = new EmptyCell(obj.Table_position);
        }



        void MainWindow_DoWork(object sender, DoWorkEventArgs e)
        {
            mutex.WaitOne(TimeSpan.FromMinutes(10)); // ждем, пока другие боты сходят
            if (bots.Contains((Bot)e.Argument) && player != null)
            {
                ((Bot)e.Argument).RecoveryMoves();
                BotMoving((Bot)e.Argument);
                Thread.Sleep(1000);
            }


            mutex.ReleaseMutex();

        }


        bool EndMove = false;
        bool GameActive;
        BackgroundWorker game;

        private void RunNewGame()
        {
           
            CreateGame();
            mutex = new Mutex(false, "");
            
            if (game != null )
            {
                game.Dispose(); 
            }
            
            game = new BackgroundWorker();
            game.DoWork += new DoWorkEventHandler(WorkGame);
            game.WorkerReportsProgress = true;
            game.RunWorkerAsync();
        }



        void WorkGame(object sender, DoWorkEventArgs e)
        {
            GameActive = true;


            do
            {
                Dispatcher.BeginInvoke(new Action(delegate
            {
                Steps_Change();
            }));
                
                if(player != null)player.RecoveryMoves();



                while (!EndMove && GameActive) // пока игрок не завершит ход
                {
                }


                EndMove = false;

                BlockUI(); // блокируем интерфейс, когда боты ходят

                int index = 0;
                counter = 0;
                while (index < bots.Count && player != null) // пока игрок жив и боты не убиты, боты совершают действия
                {
                    if (!GameActive && player == null) break;
                    bots_thread[index].RunWorkerCompleted += new RunWorkerCompletedEventHandler(LastBotsMove);
                    bots_thread[index].RunWorkerAsync(bots[index]);
                    index++;
                    
                }

            } while (GameActive && player != null); // игра идет, до победы игрока, либо ботов. 

            BlockUI(); // если игра закончена - блокируем интерфейс
           
        }


        void Steps_Change() // показываем информацию об оставшемся количестве шагов
        {
            if (player != null)
            {
                var str = "Осталось ходов:" + player.Buffer_steps;
                label2.Content = str;
            }
        }

        int counter;

        void LastBotsMove(object sender, AsyncCompletedEventArgs e) // когда все боты сходили - разблокируем интерфейс
        {
            counter++;
            if (counter >= bots.Count && player != null)
            {
                UnblocUI();
            }
        }

        void BlockUI() // блокировка интерфейса
        {
            Dispatcher.BeginInvoke(new Action(delegate
            {
                button1.IsEnabled = false;
                GameField.IsEnabled = false;
                label2.Visibility = Visibility.Hidden;
               

            }));
        }

        void UnblocUI() // разблокировка интерфейса
        {
            Dispatcher.BeginInvoke(new Action(delegate
            {
                button1.IsEnabled = true;
                GameField.IsEnabled = true;
                label2.Visibility = Visibility.Visible;
           
            }));
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            EndMove = true;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            UnblocUI();
            RunNewGame();
        }

       
    }
}
