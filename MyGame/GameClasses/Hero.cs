using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace MyGame.GameClasses
{
	public class Hero: Man
	{
        private MagicStone _stone;


		// Магический камень игрока
        public MagicStone StoneProp
        {
            get { return _stone; }
            set
			{
				// При получении комня 
				// Если камня у игрока нет, то забираем его с клетки
				if (StoneProp == null)
				{
					_stone = value;
					Position.GameObj = null;
				}
				// Если есть, то обмениваем его, на тот что лежит на клетке
				else
				{
					var oldStone = _stone;
					_stone = value;
					Position.GameObj = oldStone;
				}	
			}
        }

        public Hero(Cell position, Weapon weap, Panel drawArea, BufferedGraphics g) :
			base(position, weap, drawArea, g)
		{
			// Выбираем картинку и подгоняем ее под размер клетки
			_skin = MyGame.Properties.Resources.HeroImg;
			_skin = new Bitmap(_skin, position.CellArea.Size);

            _stone = null;

			PicBox.Location = position.CellArea.Location;
			PicBox.Size = position.CellArea.Size;
			PicBox.Image = _skin;		
		}

		/// <summary>
		/// Игрок подбирает предмет с клетки, на которой стоит
		/// </summary>
		public void PickUpGameObj()
		{
			if (Position.GameObj is Weapon)
			{
				WeaponProp = (Weapon)Position.GameObj;
			}
			else
			{
				StoneProp = (MagicStone)Position.GameObj;
			}
			// Поднятие предмета заканчивает ход
			StepCount = 0;
			
		}

		/// <summary>
		/// Игрок проверяет стоит ли он на клетке с предметом
		/// </summary>
		/// <returns></returns>
		public bool CheckGameObjOnCell()
		{
			if (Position.GameObj != null && StepCount > 0)
			{
				return true;
			}
			return false;
		}

		/// <summary>
		///  Проверяет находится ли игрок в одном из 3 положении, которые допускают переход на следующий ход
		/// </summary>
		/// <returns> True -если игрок находится в нужном состоянии. False если нет.</returns>
		public bool InOneOfCorrectStates()
		{
			// Количество шагов равно 0
			if(StepCount == 0)
			{
				return true;
			} 
			// Игрок имеет камень скорости и шагнул
			if(StoneProp != null && StoneProp.Type == StoneType.SPEED && StepCount == 1)
			{
				return true;
			}
			// У игрока выбрана цель
			if(Target != null)
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// Восстанавливает количество шагов игрока
		/// </summary>
		public override void StepsReload()
		{
			StepCount = 1;
			// Если имеется камень скорости, то количество шагов за ход учеличивается
            if (StoneProp != null && StoneProp.Type == StoneType.SPEED)
            {
                StepCount = 2;
            }
		}

		/// <summary>
		/// Метод Draw отрисовывает клекти в которые игрок может сходить. А так же цель игрока, если она выбрана
		/// </summary>
		public override void Draw()
		{
			Pen p = new Pen(Color.DarkGray, 2); ;
			if (StepCount != 0)
			{
				p = new Pen(Color.CornflowerBlue, 4);
			}

			foreach (var cell in CellsToMove)
			{
				cell.Draw(_graphic.Graphics, p);
			}

			if (_target != null) _target.Draw();
		}
	}
}
