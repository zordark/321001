using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace MyGame.GameClasses
{
	// Перечисление возможных направлений движения
	public enum MoveDirection
	{
		UP, DOWN, LEFT, RIGHT
	};
	
	public class Enemy: Man
	{
		// Выбранное врагом направление
		private MoveDirection _mDir;

		public MoveDirection MoveDir
		{
			get{return _mDir;}
			set{_mDir = value;}
		}

        public Enemy(Cell position, Weapon weap, Panel drawArea, BufferedGraphics g) :
			base(position, weap, drawArea, g)
		{
			// Выбираем картинку и подгоняем ее под размер клетки
			_skin = MyGame.Properties.Resources.EvilSkin;
			_skin = new Bitmap(_skin, position.CellArea.Size);

			PicBox.Location = position.CellArea.Location;
			PicBox.Size = position.CellArea.Size;
			PicBox.Image = _skin;	
		}

		/// <summary>
		/// Выбирает случайное направление для движения
		/// </summary>
		/// <param name="r"></param>
		public void ChooseDirection(Random r)
		{
			var canMoveDir = new List<int>();
			for(int i = 0; i < CellsToMove.Count; i++)
			{
				if(CellsToMove[i].CellArea.Location.X > Position.CellArea.Location.X)
				{
					canMoveDir.Add(3);
				}
				else
				{
					if (CellsToMove[i].CellArea.Location.X < Position.CellArea.Location.X) canMoveDir.Add(2);
				}
				if (CellsToMove[i].CellArea.Location.Y > Position.CellArea.Location.Y)
				{
					canMoveDir.Add(0);
				}
				else
				{
					if (CellsToMove[i].CellArea.Location.Y < Position.CellArea.Location.Y) canMoveDir.Add(1);
				}
			}
			_mDir = (MoveDirection)canMoveDir[r.Next(canMoveDir.Count)];
		}

		/// <summary>
		/// Удаляет клетку из списка клеток для хода, если на ней находится враг
		/// </summary>
		/// <param name="enemy"></param>
		public void DelBlockedCells(Enemy enemy)
		{
			for(int i = 0; i < CellsToMove.Count; i++)
			{
				if(enemy.Position == CellsToMove[i])
				{	
					CellsToMove.RemoveAt(i);
					break;
				}
			}
		}

		/// <summary>
		/// Метод Draw рисует обводку вокруг врага, если он выбран целью игрока
		/// </summary>
		public override void Draw()
		{
			_graphic.Graphics.DrawRectangle(_cellBorderPen, Position.CellArea);
		}
	}
}
