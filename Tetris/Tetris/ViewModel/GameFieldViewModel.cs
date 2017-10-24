using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Tetris.Model;
using Tetris.ViewModel.Commands;
using Tetris.ViewModel.Messenger;

namespace Tetris.ViewModel
{
    public class GameFieldViewModel : ViewModelBase
    {
        private Grid _TetrisGrid;
        public Grid TetrisGrig
        {
            get { return _TetrisGrid; }
            set { _TetrisGrid = value; RaisePropertyChanged("TetrisGrig"); }
        }

        public ChangePositionCommand MoveClick { get; set; }
        public RotateCommand RotateClick { get; set; }
        public StartGameCommand StartGameClick { get; set; }

        private GameBoard _GameBoard;
        private Rectangle[,] _GridFill;
        public Rectangle[,] GridFill
        {
            get { return _GridFill; }
            set { _GridFill = value; RaisePropertyChanged("GridFill"); }
        }

        public GameFieldViewModel()
        {
            RotateClick = new RotateCommand(this);
            MoveClick = new ChangePositionCommand(this);
            StartGameClick = new StartGameCommand(this);
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<MvvmMessage>(this, HandleMessage);
        }
        private void AddChildrenToGrid()
        {
            for (int i = 0; i < TetrisGrig.ColumnDefinitions.Count; i++)
                for (int j = 0; j < TetrisGrig.RowDefinitions.Count; j++)
                {
                    GridFill[i, j] = new Rectangle();                    
                    Grid.SetRow(GridFill[i, j], j);
                    Grid.SetColumn(GridFill[i, j], i);
                    TetrisGrig.Children.Add(GridFill[i, j]);
                }
        }
        private void HandleMessage(MvvmMessage message)
        {
            if (TetrisGrig == null)
            {
                TetrisGrig = message.TetrisGrid;
                GridFill = new Rectangle[TetrisGrig.ColumnDefinitions.Count, TetrisGrig.RowDefinitions.Count];
                _GameBoard = new GameBoard(_TetrisGrid.RowDefinitions.Count, _TetrisGrid.ColumnDefinitions.Count);
                AddChildrenToGrid();
            }
        }
        public void ChangePosition(string direction)
        {
            Point directionPoint;
            ClearCurrentBlock();
            switch (direction)
            {
                case "Left":
                    directionPoint = new Point(-1, 0);
                    _GameBoard.MoveBlockLeftRight(directionPoint);
                    break;
                case "Right":
                    directionPoint = new Point(1, 0);
                    _GameBoard.MoveBlockLeftRight(directionPoint);
                    break;
                case "Down":
                    _GameBoard.MoveDownBlockButton();
                    break;
            }
            DrawCurrentBlock();
        }
        public void StartGame()
        {
            _GameBoard.StartTimer();
        }
        public void RotateBlock()
        {
            ClearCurrentBlock();
            _GameBoard.RotateShape();
            DrawCurrentBlock();
        }
        private void ClearCurrentBlock()
        {
            for (int i = 0; i < _GameBoard._CurrentBlock.CurrentFigurePosition.Length; i++)
            {
                int x = Convert.ToInt32(_GameBoard._CurrentBlock.CurrentFigurePosition[i].X);
                int y = Convert.ToInt32(_GameBoard._CurrentBlock.CurrentFigurePosition[i].Y);
                GridFill[x, y].Fill = null;
            }
        }
        private void DrawCurrentBlock()
        {
            for (int i = 0; i < _GameBoard._CurrentBlock.CurrentFigurePosition.Length; i++)
            {
                int x = Convert.ToInt32(_GameBoard._CurrentBlock.CurrentFigurePosition[i].X);
                int y = Convert.ToInt32(_GameBoard._CurrentBlock.CurrentFigurePosition[i].Y);
                GridFill[x, y].Fill = _GameBoard._CurrentBlock.Color;
            }
        }



    }
}
