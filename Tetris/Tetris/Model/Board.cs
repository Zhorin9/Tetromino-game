using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Tetris.Model.Tetromino;

namespace Tetris.Model
{
    class Board
    {
        private int _Rows { get; }
        private int _Columns { get; }

        private Rectangle[,] _BlockControls;
        private int[,] _ValueArray;

        private List<string> _ListOfTetrisBlock { get; }
        private TetrisBlock _CurrentBlock;

        private Grid _TetrisGrid { get; set; }

        public Board(Grid tetrisGrid)
        {
            _TetrisGrid = tetrisGrid;
            _Rows = tetrisGrid.RowDefinitions.Count;
            _Columns = tetrisGrid.ColumnDefinitions.Count;
            _BlockControls = new Rectangle[_Columns, _Rows];
            _ValueArray = new int[_Columns, _Rows];

            for (int i = 0; i < _Columns; i++)            
                for (int j = 0; j < _Rows; j++)
                {
                    _BlockControls[i, j] = new Rectangle();
                    Grid.SetRow(_BlockControls[i, j], j);
                    Grid.SetColumn(_BlockControls[i, j], i);
                    _ValueArray[i, j] = 0;
                    _TetrisGrid.Children.Add(_BlockControls[i, j]);
                }            
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
            DrawCurrentBlock();         
        }
        public void MoveBlockLeftRight(Point newPosition)
        {
            ClearCurrentBlock();
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
            DrawCurrentBlock();
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
            ClearCurrentBlock();
            _CurrentBlock.IncreasePosition(newPositon);
            DrawCurrentBlock();
            return false;
        }
        public void MoveDownBlockButton()
        {
            while (MoveDownBlockTick(new Point(0, 1)));
        }

        public void RotateShape()
        {
            ClearCurrentBlock();
            Point[] auxilaryVariable = _CurrentBlock.Rotate();

            if(CheckCollistionWhileRotating(auxilaryVariable))
                _CurrentBlock.ChangeShape(auxilaryVariable);                       
            DrawCurrentBlock();
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
                else if (_ValueArray[Convert.ToInt32(auxiliaryVariable[i].X + _CurrentBlock.CurrentPosition.X), Convert.ToInt32(auxiliaryVariable[i].Y + _CurrentBlock.CurrentPosition.Y)] != 0)
                    return  false;
                //Check right side of block
                else if (_ValueArray[Convert.ToInt32(auxiliaryVariable[i].X + _CurrentBlock.CurrentPosition.X), Convert.ToInt32(auxiliaryVariable[i].Y + _CurrentBlock.CurrentPosition.Y)] != 0)
                    return  false;
            }
            return true;
        }
        private void WriteToArray()
        {
            for (int i = 0; i < _CurrentBlock.CurrentFigurePosition.Length; i++)
            {
                _ValueArray[Convert.ToInt32(_CurrentBlock.CurrentFigurePosition[i].X), Convert.ToInt32(_CurrentBlock.CurrentFigurePosition[i].Y)] = _CurrentBlock.Value;
            }
            ClearLine(CheckCompletedLine());
            MakeNewBlock();
        }
        private void ClearCurrentBlock()
        {
            for (int i = 0; i < _CurrentBlock.CurrentFigurePosition.Length; i++)
            {
                int x = Convert.ToInt32(_CurrentBlock.CurrentFigurePosition[i].X);
                int y = Convert.ToInt32(_CurrentBlock.CurrentFigurePosition[i].Y);
                _BlockControls[x, y].Fill = null;
            }
        }
        private void DrawCurrentBlock()
        {
            for (int i = 0; i < _CurrentBlock.CurrentFigurePosition.Length; i++)
            {
                int x = Convert.ToInt32(_CurrentBlock.CurrentFigurePosition[i].X);
                int y = Convert.ToInt32(_CurrentBlock.CurrentFigurePosition[i].Y);
                _BlockControls[x, y].Fill = _CurrentBlock.Color;
            }
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





/*
                    for (int i = 0; i<_Rows; i++)
                    {
                        for (int j = 0; j<_Columns; j++)
                        {
                            Console.Write("{0} ", _ValueArray[j, i]);
                        }
                        Console.WriteLine();

                    }*/



/* 
 * 
 * 
    private bool CheckCollision()
    {
        foreach (Point p in _CurrentBlock.CurrentPosition)
        {
            if (_ValueArray[Convert.ToInt32(p.X - 1), Convert.ToInt32(p.Y)] != 0)
                return true;
            if (p.X == 0)
                return true;
            if (_ValueArray[Convert.ToInt32(p.X + 1), Convert.ToInt32(p.Y)] != 0)
                return true;
            if (p.X == _Columns - 1)
                return true;
        }
        return false;
    }
*/
 