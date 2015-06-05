using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace MyGame.GameClasses
{
	// Перечисление типов оружия
	public enum WeaponType 
	{
		KNIFE,
		SWORD,
		SPEAR,
		MAGIC_STAFF
	};

	public class Weapon: GameObject
	{
		// Радиус атаки оружия
		private int _radius;
		// Тип оружия
		private WeaponType _type;

		// Изображение оружия
		public Image WeaponView
		{
			get{return _gameObjView;}
			private set
			{
				//Изображение выбирается в зависимости от типа оружия
				if(_type == WeaponType.KNIFE) _gameObjView = MyGame.Properties.Resources.Knife;
				if (_type == WeaponType.MAGIC_STAFF) _gameObjView = MyGame.Properties.Resources.MagicStuff;
				if (_type == WeaponType.SPEAR) _gameObjView = MyGame.Properties.Resources.Spear;
				if (_type == WeaponType.SWORD) _gameObjView = MyGame.Properties.Resources.Sword;
			}
		}

		public int Radius
		{
			get{return _radius;}
			private set{_radius = value;}
		}

		public WeaponType Type
		{
			get { return _type; }
			set { _type = value; }
		}

		public Weapon(WeaponType type)
		{
			_radius= (int)type + 2;
			_type = type;
			WeaponView = new Bitmap(1, 1);
		}
	}
}
