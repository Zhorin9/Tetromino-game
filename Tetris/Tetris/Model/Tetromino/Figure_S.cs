using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Tetris.Model.Tetromino
{
    class Figure_S :TetrisBlock
    {
        public Figure_S() : base()
        {
            _CurrentShape = SetShape();
            _Value = 5;
        }
        private Point[] SetShape()
        {
            _Rotated = true;
            _Color = Brushes.Violet;
            return new Point[]
            {
                new Point(0,0),
                new Point(1,0),
                new Point(1,1),
                new Point(2,1),
            };
        }
    }
}