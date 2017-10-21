using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Tetris.Model.Tetromino
{
    class Figure_T:TetrisBlock
    {
        public Figure_T() : base()
        {
            _CurrentShape = SetShape();
            _Value = 6;
        }
        private Point[] SetShape()
        {
            _Rotated = true;
            _Color = Brushes.Yellow;
            return new Point[]
            {                
                new Point(0,0),
                new Point(1,0),
                new Point(1,1),
                new Point(2,0),
            };
        }
    }
}