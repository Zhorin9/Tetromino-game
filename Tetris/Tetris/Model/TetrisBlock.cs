using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Tetris.Model
{
    abstract class TetrisBlock
    {
        protected int _Value;
        public int Value { get { return _Value; } }

        protected Point _CurrentPosition { get; set; }
        public Point CurrentPosition { get { return _CurrentPosition; } }

        public Point[] CurrentFigurePosition
        {
            get
            {
                Point[] currentPosition = new Point[_CurrentShape.Length];
                for(int i =0; i<_CurrentShape.Length; i++)
                {
                    currentPosition[i] = new Point( _CurrentShape[i].X + _CurrentPosition.X, _CurrentShape[i].Y + _CurrentPosition.Y);
                }
                return currentPosition;
            }
        }
        protected Point[] _CurrentShape;
        protected bool _Rotated;

        protected Brush _Color { get; set; }
        public Brush Color { get { return _Color; } }

        public TetrisBlock()
        {            
            _CurrentPosition = new Point(5, 0);
        }
        public void IncreasePosition(Point _newPosition)
        {
            Point newCurrrentPosition = new Point(_CurrentPosition.X + _newPosition.X, _CurrentPosition.Y + _newPosition.Y);
            _CurrentPosition = newCurrrentPosition;
        }
        public void ChangeShape(Point[] newShape)
        {
            for(int i = 0; i < _CurrentShape.Length; i++)
            {
                _CurrentShape[i] = newShape[i];
            }
        }
        public virtual Point[] Rotate()
        {
            Point constantCoorditanion = _CurrentShape[1];
            Point[] auxiliaryVariable = new Point[_CurrentShape.Length];
            auxiliaryVariable[1] = _CurrentShape[1];
            for (int i = 0; i < _CurrentShape.Length; i++)
            {
                if (i == 1)
                    i = 2;
                Point rotatedPoint = _CurrentShape[i];
                Vector vrVector = Point.Subtract(rotatedPoint, constantCoorditanion);
                Vector vtVector = new Vector(vrVector.X * 0 + vrVector.Y * -1, vrVector.X * 1 + vrVector.Y * 0);
                auxiliaryVariable[i] = Point.Add(constantCoorditanion, vtVector);
            }
            return auxiliaryVariable;
        }
    }
}

