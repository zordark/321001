using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGame.GameClasses.Interfaces
{
	public interface IMovable
	{
		void Move(Cell cell);

		void StepsReload();
	}
}
