using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Tetris.Model
{
    class Figure_O:TetrisBlock
    {
        public Figure_O() : base()
        {
            _CurrentShape = SetShape();
            _Value = 4;
        }
        private Point[] SetShape()
        {
            _Rotated = true;
            _Color = Brushes.Red;
            return new Point[]
            {
                new Point(0,1),
                new Point(0,0),
                new Point(1,1),
                new Point(1,0),
            };            
        }
        public override Point[] Rotate()
        {
            return _CurrentShape;
        }
    }
}
