using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MindGame
{
    class EmptyCell:GameParticipant  // класс пустых клеток для ходов 
    {
        public EmptyCell(Point pos)
            : base("")
        {
            Table_position = pos;
        }
    }
}
