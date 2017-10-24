using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using Tetris.Model.Tetromino;

namespace Tetris.Model
{
    class GameBoard
    {
        private readonly int _Rows, _Columns;
        private int[,] _ValueArray;

        private DispatcherTimer _Timer;
        private List<string> _ListOfTetrisBlock { get; }
        public TetrisBlock _CurrentBlock { get; private set; }

        public GameBoard(int rows, int columns)
        {
            _Rows = rows;
            _Columns = columns;
            _ValueArray = new int[_Columns, _Rows];            
            _ListOfTetrisBlock = new List<string>()
            {
                "Figure_I",
                "Figure_J",
                "Figure_L",
                "Figure_O",
                "Figure_S",
                "Figure_T",
                "Figure_Z",
            };
            _Timer = new DispatcherTimer();
            _Timer.Interval = TimeSpan.FromSeconds(0.5);
            _Timer.Tick += _Timer_Tick;
        }
        public void StartTimer()
        {
            _Timer.Start();
            if (_CurrentBlock == null)
                MakeNewBlock();
        }
        public void StopTimer()
        {
            _Timer.Stop();
        }
        private void _Timer_Tick(object sender, EventArgs e)
        {
            MoveDownBlockTick(new Point(0,1));
        }
        public void MakeNewBlock()
        {
            Random random = new Random();
            string newFigure = _ListOfTetrisBlock[random.Next(_ListOfTetrisBlock.Count)];
            switch (newFigure)
            {
                case "Figure_I":
                    _CurrentBlock = new Figure_I();
                    break;
                case "Figure_J":
                    _CurrentBlock = new Figure_J();
                    break;
                case "Figure_L":
                    _CurrentBlock = new Figure_L();
                    break;
                case "Figure_O":
                    _CurrentBlock = new Figure_O();
                    break;
                case "Figure_S":
                    _CurrentBlock = new Figure_S();
                    break;
                case "Figure_T":
                    _CurrentBlock = new Figure_T();
                    break;
                case "Figure_Z":
                    _CurrentBlock = new Figure_Z();
                    break;
            }     
        }
        public void MoveBlockLeftRight(Point newPosition)
        {
            foreach (Point p in _CurrentBlock.CurrentFigurePosition)
            {
                if (newPosition.X == -1)
                {
                    if (p.X == 0)
                        newPosition.X = 0;
                    else if (_ValueArray[Convert.ToInt32(p.X - 1), Convert.ToInt32(p.Y)] != 0)
                        newPosition.X = 0;
                }
                if (newPosition.X == 1)
                {
                    if (p.X == _Columns - 1)
                        newPosition.X = 0;
                    else if (_ValueArray[Convert.ToInt32(p.X + 1), Convert.ToInt32(p.Y)] != 0)
                        newPosition.X = 0;
                }
            }                         
            _CurrentBlock.IncreasePosition(newPosition);
        }
        public bool MoveDownBlockTick(Point newPositon)
        {
            foreach (Point p in _CurrentBlock.CurrentFigurePosition)
            {
                if (p.Y == _Rows - 1)
                {
                    WriteToArray();
                    return true;
                }

                else if (_ValueArray[Convert.ToInt32(p.X), Convert.ToInt32(p.Y + 1)] != 0)
                {
                    WriteToArray();
                    return true;
                }
            }
            _CurrentBlock.IncreasePosition(newPositon);
            return false;
        }
        public void MoveDownBlockButton()
        {
            while (MoveDownBlockTick(new Point(0, 1)));
        }
        public void RotateShape()
        {
            Point[] auxilaryVariable = _CurrentBlock.Rotate();

            if(CheckCollistionWhileRotating(auxilaryVariable))
                _CurrentBlock.ChangeShape(auxilaryVariable);                       
        }
        private bool CheckCollistionWhileRotating(Point[] auxiliaryVariable)
        {
            for (int i = 0; i < auxiliaryVariable.Length; i++)
            {
                //Check game field border
                if ((auxiliaryVariable[i].X + _CurrentBlock.CurrentPosition.X) < 0
                    || (auxiliaryVariable[i].X + _CurrentBlock.CurrentPosition.X) > _Columns - 1
                    || (auxiliaryVariable[i].Y + _CurrentBlock.CurrentPosition.Y) < 0
                    || (auxiliaryVariable[i].Y + _CurrentBlock.CurrentPosition.Y) > _Rows - 1)
                    return false;
                //Check left side of block
                else if (_ValueArray[Convert.ToInt32(auxiliaryVariable[i].X + _CurrentBlock.CurrentPosition.X), 
                    Convert.ToInt32(auxiliaryVariable[i].Y + _CurrentBlock.CurrentPosition.Y)] != 0)
                    return  false;
                //Check right side of block
                else if (_ValueArray[Convert.ToInt32(auxiliaryVariable[i].X + _CurrentBlock.CurrentPosition.X),
                    Convert.ToInt32(auxiliaryVariable[i].Y + _CurrentBlock.CurrentPosition.Y)] != 0)
                    return  false;
            }
            return true;
        }
        private void WriteToArray()
        {
            for (int i = 0; i < _CurrentBlock.CurrentFigurePosition.Length; i++)
            {
                _ValueArray[Convert.ToInt32(_CurrentBlock.CurrentFigurePosition[i].X), 
                    Convert.ToInt32(_CurrentBlock.CurrentFigurePosition[i].Y)] = _CurrentBlock.Value;
            }
            //ClearLine(CheckCompletedLine());
            MakeNewBlock();
        }
        private List<int> CheckCompletedLine()
        {
            List<int> rowsToClear = new List<int>();
            bool isFilled = true;
            for (int i = 0; i < _Rows; i++)
            {
                isFilled = true;
                for (int j = 0; j < _Columns; j++)
                {
                    if (_ValueArray[j, i] == 0)
                        isFilled = false;
                }
                if (isFilled)
                    rowsToClear.Add(i);
            }
            return rowsToClear;
        }
        /*
        private void ClearLine(List<int> rowsToClear)
        {
            if (rowsToClear.Count == 0) ;
            else
            {
                for (int i = 0; i < rowsToClear.Count; i++)
                    for (int j = 0; j < _Columns; j++)
                    {
                        _ValueArray[j, rowsToClear[i]] = 0;
                        _BlockControls[j, rowsToClear[i]].Fill = null;
                    }
            }
            var minRow =
                from row in rowsToClear
                select rowsToClear.Min();

            //FallingBlock(Convert.ToInt32(minRow));
        }
        */
        private void FallingBlock(int clearedRow)
        {
            for (int i = clearedRow ; i > 0; i--)
            {
                for (int j = 0; i < _Columns; j++)
                {

                }
            }
        }
    }
}
