using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Tetris.Model.Tetromino
{
    class Figure_L:TetrisBlock
    {
        public Figure_L() : base()
        {
            _CurrentShape = SetShape();
            _Value = 3;
        }
        private Point[] SetShape()
        {
            _Rotated = true;
            _Color = Brushes.Blue;
            return new Point[]
            {
                new Point(0,0),                                
                new Point(1,0),
                new Point(2,0),
                new Point(2,1),
            };
        }
    }
}
