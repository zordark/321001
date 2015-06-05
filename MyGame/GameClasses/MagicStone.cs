using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MyGame.GameClasses
{
	// Перечисление типов магических камней
	public enum StoneType
	{
		ARMOR,
		SPEED,
		ATTACK
	}

	public class MagicStone: GameObject
	{
		// Тип камня
		private StoneType _type;

		public StoneType Type
		{
			get{return _type;}
			private set{_type = value;}
		}

		// Изображение магического камня
        public Image StoneView
        {
            get { return _gameObjView; }
            private set
            {
				//Изображение камня выбирается в зависимости от типа
                if (_type == StoneType.ARMOR) _gameObjView = MyGame.Properties.Resources.ArmorStone;
                if (_type == StoneType.ATTACK) _gameObjView = MyGame.Properties.Resources.AttackStone;
                if (_type == StoneType.SPEED) _gameObjView = MyGame.Properties.Resources.SpeedStone;

            }
        }

        public MagicStone(StoneType type)
		{ 
			_type = type;
            StoneView = _gameObjView = new Bitmap(1, 1);
			
		}
	}
}
