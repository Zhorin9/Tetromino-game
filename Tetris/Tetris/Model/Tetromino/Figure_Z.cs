using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Tetris.Model.Tetromino
{
    class Figure_Z:TetrisBlock
    {
        public Figure_Z() : base()
        {
            _CurrentShape = SetShape();
            _Value = 7;
        }
        private Point[] SetShape()
        {
            _Rotated = true;
            _Color = Brushes.Pink;
            return new Point[]
            {
                new Point(0,1),
                new Point(1,1),
                new Point(1,0),                              
                new Point(2,0),
            };
        }
    }
}