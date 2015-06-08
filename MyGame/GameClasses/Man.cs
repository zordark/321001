using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyGame.GameClasses.Interfaces;
using System.Drawing;
using System.Windows.Forms;

namespace MyGame.GameClasses
{
	public abstract class Man: IDrawable, IMovable
	{

		// Изображение человека
		protected Image _skin;

		protected Panel _drawArea;
		protected BufferedGraphics _graphic;
		protected Pen _cellBorderPen;

		protected Weapon _weapon;
		protected Man _target;
		protected int _stepCount;


		// Контрол для отрисовки человека
		public PictureBox PicBox
		{
			get; set;
		}
		// Список клеток для шага
		public List<Cell> CellsToMove
		{
			get; set;
		}
		// Список клеток для удара
		public List<Cell> CellsToStrike
		{
			get; set;
		}

		// Клетка, на котороя стоит человек
		public Cell Position
		{
			get; set;
		}

		// Оружие человека
		public Weapon WeaponProp
		{
			get{return _weapon;}
			set
			{
				// При получении оружия обнуляем список клеток для удара
				CellsToStrike = new List<Cell>();
				// И обмениваем оружие на лежащее на клетке.
				var oldWeap = _weapon;
				_weapon = value;
				Position.GameObj = oldWeap;
			}
		}

		// Количество шагов
		public int StepCount
		{
			get { return _stepCount; }
			set 
			{ 
				_stepCount = value;
				if(_stepCount == 0)
				{
					CellsToMove = new List<Cell>();
					_target = null;
					_drawArea.Invalidate();
				}
			}
		}

		// Цель для атаки
		public Man Target
		{
			get{return _target;} 
			set
			{
				// При получении обновляем область отрисовки для поцветки
				_target = value;
				_drawArea.Invalidate();
			}
		}

		public Man(Cell position, Weapon weap, Panel drawArea, BufferedGraphics gr)
		{
			_stepCount = 1;
			_weapon = weap;

			_drawArea = drawArea;
			_graphic = gr;
			_cellBorderPen = new Pen(Color.Red, 4);
			PicBox = new PictureBox();
			PicBox.MouseHover += PicBox_MouseOnPict;
			PicBox.MouseLeave += PicBox_MouseLeave;

			Position = position;
			CellsToMove = new List<Cell>();
			CellsToStrike = new List<Cell>();
		}
		
		private void PicBox_MouseOnPict(object sender, EventArgs e)
		{
			DrawStrikeCells();
            _graphic.Render();
		}

		private void PicBox_MouseLeave(object sender, EventArgs e)
		{
			_drawArea.Invalidate();
		}

		public virtual void Draw(){}

		public void DrawStrikeCells()
		{
			foreach(var cell in CellsToStrike)
			{
				_graphic.Graphics.DrawRectangle(_cellBorderPen, cell.CellArea);	
			}
		}

		public void Move(Cell cell)
		{
			StepCount--;
			Position = cell;
			CellsToStrike = new List<Cell>();
			CellsToMove = new List<Cell>();
			PicBox.Location = Position.CellArea.Location;
		}

		public virtual void StepsReload()
		{
			StepCount = 1;
		}
	}
}
