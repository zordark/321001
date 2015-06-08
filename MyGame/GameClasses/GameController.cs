using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using MyGame.GameClasses.Interfaces;

namespace MyGame.GameClasses
{
	public class GameController
	{
		// Ручка для отрисовки клеток
		private Pen _p;
		private Random _r;

		// Зона отрисовки
		private Panel _drawArea;
		// График для отрисоки игры
        private BufferedGraphics _graphic;

		public List<List<Cell>> Cells
		{
			get; private set;
		}

		public Hero Player
		{
			get; private set;
		}

		public List<Enemy> Enemies
		{
			get; private set;
		}

		// Флаг окончания игры
		public bool GameIsEnded
		{
			get; set;
		}

		public GameController(Panel drawArea, BufferedGraphics g, int xCells, int yCells)
		{
			_p = new Pen(Color.DarkGray, 2);
			_r = new Random();
			_drawArea = drawArea;
			_graphic = g;

			GameIsEnded = false;

			var cellSize = new Size((int)(_drawArea.Width / xCells), (int)(_drawArea.Height / yCells));
			Cells = new List<List<Cell>>();
			InitializeCells(cellSize, xCells, yCells);

			var freeCells = new List<Cell> ();
			for(int i = 0; i < Cells.Count; i++)
			{
				freeCells.AddRange(Cells[i]);
			}
            InitializeGameObj(freeCells);
			InitializeEnemies(freeCells);
			InitializeHero(freeCells);

			for (int i = 0; i < drawArea.Controls.Count; i++)
			{
				if(drawArea.Controls[i] != Player.PicBox)
				{
					drawArea.Controls[i].Click += DrawAreaControl_Ckick;
				}
			}
		}

		/// <summary>
		/// Создает поле(матрицу клеток)
		/// </summary>
		/// <param name="cellSize"> Размер клетки </param>
		/// <param name="xC"> Количекство клеток по горизонтали </param>
		/// <param name="yC"> Колическтво клеток по вертикали </param>
		private void InitializeCells(Size cellSize, int xC, int yC)
		{
			for(int i = 0; i < xC; i++)
			{
				Cells.Add(new List<Cell>());
				for (int j = 0; j < yC; j++)
				{
					Cells[i].Add(new Cell(cellSize, new Point(i * cellSize.Width, j * cellSize.Height)));
				}		
			}
		}

		/// <summary>
		/// Создает игровые объекты(магические камни и оружие)
		/// </summary>
		/// <param name="freeCells">Список свободных клеток. На входе - все кслетки игрового поля. Клетки с GameObject-ом удаляются</param>
		private void InitializeGameObj(List<Cell> freeCells)
		{
			int weapCount = _r.Next(3, 6);
			int stonesCount = _r.Next(3, 6);

			for (int i = 0; i < weapCount; i++)
			{
				var temp = freeCells[_r.Next(freeCells.Count)];
				temp.GameObj = new Weapon((WeaponType)_r.Next(1, 4));
				freeCells.Remove(temp);
			}

			for (int i = 0; i < stonesCount; i++)
			{
				var temp = freeCells[_r.Next(freeCells.Count)];
				temp.GameObj = new MagicStone((StoneType)_r.Next(1, 3));
				freeCells.Remove(temp);
			}
		}

		/// <summary>
		/// Создает врагов 
		/// </summary>
		/// <param name="freeCells"> Список свободных клеток. Без тех клеток, на которых есть GameObject </param>
		private void InitializeEnemies(List<Cell> freeCells)
		{
			Enemies = new List<Enemy>();
			for(int i = 0; i < 10; i++)
			{
				var temp = freeCells[_r.Next(freeCells.Count)];
				Enemies.Add(new Enemy(temp, new Weapon((WeaponType)_r.Next(0, 3)), _drawArea, _graphic));
				GetMoveCells(Enemies[i]);
				GetStrikeCells(Enemies[i]);
				
				_drawArea.Controls.Add(Enemies[i].PicBox);
				freeCells.Remove(temp);
				RemoveEnemyStrikeCells(freeCells, Enemies[i]);
			}
		}

		/// <summary>
		/// Создает персонажа для игрока
		/// </summary>
		/// <param name="freeCells"> Список не занятых клеток(без GameObject-ов и врагов. А также без клеток, по которым могут сразу ударить враги) </param>
		private void InitializeHero(List<Cell> freeCells)
		{
			Player = new Hero(freeCells[_r.Next(freeCells.Count)], new Weapon(WeaponType.KNIFE), _drawArea, _graphic);
			GetMoveCells(Player);
			GetStrikeCells(Player);
			_drawArea.Controls.Add(Player.PicBox);
		}

		/// <summary>
		/// Удаляет из списка свободных клеток все клетки, по которым может попасть враг
		/// </summary>
		/// <param name="fCells"> Список свободных клеток </param>
		/// <param name="enemy"></param>
		private void RemoveEnemyStrikeCells(List<Cell> fCells, Enemy enemy)
		{
			foreach(var cell in enemy.CellsToStrike)
			{
				if(fCells.Contains(cell)) fCells.Remove(cell);						
			}
		}

		/// <summary>
		/// Определяет куда может сходить игрок или враг
		/// </summary>
		/// <param name="man"> Экземплят абстрактного класса Man (человек) </param>
		public void GetMoveCells(Man man)
		{
			Point manCoords = FindManPosition(man);
			if (manCoords.X - 1 >= 0) man.CellsToMove.Add(Cells[manCoords.X - 1][manCoords.Y]);
			if (manCoords.X + 1 < Cells.Count) man.CellsToMove.Add(Cells[manCoords.X + 1][manCoords.Y]);
			if (manCoords.Y - 1 >= 0) man.CellsToMove.Add(Cells[manCoords.X][manCoords.Y - 1]);
			if (manCoords.Y + 1 < Cells[0].Count) man.CellsToMove.Add(Cells[manCoords.X][manCoords.Y + 1]);
		}

		/// <summary>
		/// Определяет список клеток, в которые человек сможет ударить
		/// </summary>
		/// <param name="man"> Экземплят абстрактного класса Man (человек) </param>
		public void GetStrikeCells(Man man)
		{
			var attackRadius = man.WeaponProp.Radius;
			Point manCoords = FindManPosition(man);
			// Если человек является игроком, то проверяем наличие и тип магического камня
            if(man is Hero)
            {
				if ((man as Hero).StoneProp != null && (man as Hero).StoneProp.Type == StoneType.ATTACK)
                {
					//Если игрок имеет камень атаки, то радиус увеличивается
                    attackRadius++;
                }
            }
			// Определяем горизонтальные границы зоны удара
			int indXLeft = manCoords.X - attackRadius > 0 ? (manCoords.X - attackRadius) : 0;
			int indXRight = manCoords.X + attackRadius < Cells.Count ? (manCoords.X + attackRadius) : (Cells.Count - 1);
			// Определяем вертикальные границы зоны удара
			int indYUp = manCoords.Y - attackRadius > 0 ? (manCoords.Y - attackRadius) : 0;
			int indYDown = manCoords.Y + attackRadius < Cells[0].Count ? (manCoords.Y + attackRadius) : (Cells[0].Count - 1);

			// Добавляем клетки в список CellsToStrike человека
			for(int i = indXLeft; i <= indXRight; i++)
			{
				for(int j = indYUp; j <= indYDown; j++)
				{
					if(Cells[i][j] != man.Position)
					{
						if (Cells[i][j].CellArea.Location.X == man.Position.CellArea.Location.X)
						{
							man.CellsToStrike.Add(Cells[i][j]);
						}
						if (Cells[i][j].CellArea.Location.Y == man.Position.CellArea.Location.Y)
						{
							man.CellsToStrike.Add(Cells[i][j]);
						}
					}
				}
			}
		}

		/// <summary>
		/// Определяет координаты человека на доске
		/// </summary>
		/// <param name="man"> Тот(игрок или враг), чти координаты ищутся </param>
		/// <returns> Точку с координатами клетки в матрице </returns>
		private Point FindManPosition(Man man)
		{
			Point p = new Point(0, 0);
			for (int i = 0; i < Cells.Count; i++)
			{
				for (int j = 0; j < Cells[i].Count; j++)
				{
					if (Cells[i][j] == man.Position)
					{
						p = new Point(i, j);
						break;
					}
				}
			}
			return p;
		}

		/// <summary>
		/// При клике на одну из клеток для шага игрока, перемещает игрока
		/// </summary>
		/// <param name="clickPosition"> Точка клика </param>
		public void ClickOnDrawArea(Point clickPosition)
		{	
			bool haveMove = false;
			int ind = 0;
			while(!haveMove && (ind < Player.CellsToMove.Count))
			{
				if(Player.CellsToMove[ind].CellArea.Contains(clickPosition) && Player.StepCount > 0)
				{
					Player.Target = null;
					Player.Move(Player.CellsToMove[ind]);
					GetMoveCells(Player);
					GetStrikeCells(Player);
					haveMove = true;
				}
				ind++;
			}
		}

		/// <summary>
		/// Проводит завершение ходя 
		/// </summary>
		public void EndTurn()
		{
			// Если игрок не обозначил цель,
			if(Player.Target == null)
			{
				// то восстанавливаем его шаги и находим новые клетки для ходьбы и для удара
				Player.StepsReload();
                GetStrikeCells(Player); 
				GetMoveCells(Player);
			}
			else
			{
				if(Player.StepCount != 0)
				{
					// Иначе игрок убивает цель
					Kill(Player.Target);
				}
				else
				{
					Player.Target = null;
					Player.StepsReload();
				}
			}
			// Ходят враги
			EnemiesTurn();
		}

		/// <summary>
		/// Просчитывает ход врагов
		/// </summary>
		private void EnemiesTurn()
		{
			// Флаг об убийстве игрока
			var killPlayer = false;
			// Каждый враг
			foreach(var enemy in Enemies)
			{
				// проверяет все клетки для удара и если игрок на одной из них убивает его
				foreach(var cell in enemy.CellsToStrike)
				{
					if(!killPlayer && cell.Equals(Player.Position))
					{
						enemy.Target = Player;
						Kill(enemy.Target);
						killPlayer = true;
					}
				}
				// Если флаг не поднят
				if(!killPlayer)
				{
					// то удаляем клетки на которых стоит другой враг
					foreach(var enemyBlocker in Enemies)
					{
						enemy.DelBlockedCells(enemyBlocker);
					}
					// Враг выбирает направление для хода
					enemy.ChooseDirection(_r);
					var enemyMoved = false;
					// Ищем клетку, на которой стоит враг
					for (int i = 0; i < Cells.Count; i++)
					{
						for(int j = 0; j < Cells[i].Count; j++)
						{
							if (Cells[i][j].Equals(enemy.Position))
							{
								// перемещаем врага
								EnemyMakeMove(enemy, i, j);
								enemyMoved = true;
								break;
							}
						}
						if(enemyMoved) break;
					}
				}
			}
		}

		/// <summary>
		/// Враг ходит в выбранном направлении. Клетки для ходьбы и удара пересчитываются
		/// </summary>
		/// <param name="e"> Ходящий враг </param>
		/// <param name="x"> Его координата по X </param>
		/// <param name="y"> Его координата по Y </param>
		private void EnemyMakeMove(Enemy e, int x, int y)
		{
			switch(e.MoveDir)
			{
				case MoveDirection.DOWN: e.Move(Cells[x][y-1]);
										break;
				case MoveDirection.LEFT: e.Move(Cells[x - 1][y]);
										break;
				case MoveDirection.RIGHT: e.Move(Cells[x + 1][y]);
										break;
				case MoveDirection.UP: e.Move(Cells[x][y + 1]);
										break;
			}
			GetMoveCells(e);
			GetStrikeCells(e);	
		}

		/// <summary>
		/// Убирает с поля выбранного человека
		/// </summary>
		/// <param name="man"></param>
		public void Kill(Man man)
		{
			// Если убивают игрока
			if(man is Hero)
			{	
				// то проверям наличие камня защиты
                if (Player.StoneProp != null && Player.StoneProp.Type == StoneType.ARMOR)
                {
					//Если камень есть, то уничтожаем его
                    Player.StoneProp = null;
                    MessageBox.Show("Защитный камень уничтожен.");
                }
                else
                {
					// Иначе убиваем игрока
                    Player = null;
                    _drawArea.Controls.Remove(man.PicBox);
                }
			}
			else
			{
				// Иначе игрок убивает врага и восстанавливает очки шагов
				Player.Target = null;
				Enemies.Remove((Enemy)man);
                _drawArea.Controls.Remove(man.PicBox);
                Player.StepsReload();
			}
		}

		/// <summary>
		/// Проверяет закончена ли игра. Если нет, то проверяет условия окнчания игры.
		/// Если одно из условий выполнено, то выводится сообщение об окончании
		/// </summary>
        public void GameEnd()
        {
            if (!GameIsEnded)
            {
				// Если остался только игрок
                if (_drawArea.Controls.Count == 1 && Enemies.Count == 0)
                {
                    MessageBox.Show("Игрок победил!");
                    GameIsEnded = true;
                }
				// Исли игрок был уничтожен
                if (Player == null)
                {
                    MessageBox.Show("Игрок побежден!");
                    GameIsEnded = true;
                }
            }
        }

		/// <summary>
		/// Отрисовывает игровае поле(все объекты не являющиеся котролами, принадлежащими _drawArea).
		/// </summary>
		public void Draw()
		{
			for (int i = 0; i < Cells.Count; i++)
			{
				for (int j = 0; j < Cells[i].Count; j++)
				{
					Cells[i][j].Draw(_graphic.Graphics, _p);
				}
			}
			if(Player != null) Player.Draw();
		}

		/// <summary>
		/// Игрок обозначеат врага как своб цель
		/// </summary>
		/// <param name="sender"> Контрол PictureBox принадлежащий экземпляру класса Enemy</param>
		/// <param name="e"></param>
		public void DrawAreaControl_Ckick(object sender, EventArgs e)
		{
			if(!GameIsEnded)
			{
				Enemy searchedEnemy = null;
				foreach(var enemy in Enemies)
				{
					if(enemy.PicBox == (sender as PictureBox)) searchedEnemy = enemy;	
				}
				if(Player.CellsToStrike.Contains(searchedEnemy.Position) && Player.Target != searchedEnemy)
				{
					Player.Target = searchedEnemy;
				}
			}
		}
	}
}