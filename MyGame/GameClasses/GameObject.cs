using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using MyGame.GameClasses.Interfaces;

namespace MyGame.GameClasses
{
	/// <summary>
	/// Класс Игровой объект
	/// </summary>
	public abstract class GameObject
	{
		// Изображение объекта
		protected Image _gameObjView;

		// Описание объекта
		public List<string> Description
		{
			get; private set;
		}

		/// <summary>
		///  Метод Draw занимается отрисовкой игровых объектов на графике
		/// </summary>
		/// <param name="g"> График для отрисовки </param>
		/// <param name="owner"> Клетка на котороый лежит объект </param>
		public void Draw(Graphics g, Cell owner)
		{
            var temp = new Bitmap(_gameObjView, owner.CellArea.Size);
            g.DrawImage(temp, owner.CellArea.Location);	
		}
	}
}
