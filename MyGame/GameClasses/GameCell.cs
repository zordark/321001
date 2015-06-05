using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MyGame.GameClasses
{
	public class Cell
	{
		// Область отрисовки клетки
		private Rectangle _cellArea;
		// Кисть для закраски клетки
		private Brush _brush;
		// Предмет, лежащий на клетке
		private GameObject _gameObj;

		public Rectangle CellArea
		{
			get{return _cellArea;}
			private set{_cellArea = value;}
		}

		public GameObject GameObj
		{	
			get{return _gameObj;}
			set{_gameObj = value;}
		}

		public Cell(Size cellSize, Point cornerPosition)
		{
            _gameObj = null;
			_cellArea = new Rectangle(cornerPosition, cellSize);
			_brush = new SolidBrush(Color.Snow);
		}

		/// <summary>
		/// Метод Draw рисует клетку на графике
		/// </summary>
		/// <param name="g"> График для отрисовки </param>
		/// <param name="p"> Ручкадля отрисовки границ клетки </param>
		public void Draw(Graphics g, Pen p)
		{
			g.FillRectangle(_brush, _cellArea);
			g.DrawRectangle(p, _cellArea);
            if (_gameObj != null)
            {
                _gameObj.Draw(g, this);
            }
		}

		// Сравнивает две клетки по их размерам и местоположению
		public override bool Equals(object cell)
		{
			var c = (Cell)cell;
			if(CellArea.Equals(c.CellArea))
			{
				return true;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
