using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Tetris.Model
{
    class Game 
    {
        private int _Score { get; set; }
        private Board _GameBoard { get;}
        private Random _Random;
        private DispatcherTimer _Timer;
        private Grid _TetrisGrid;

        public Game(Grid TetrisGrid)
        {
            _GameBoard = new Board(TetrisGrid);
            _Timer = new DispatcherTimer();
            _Timer.Interval = TimeSpan.FromSeconds(0.5);
            _Timer.Tick += _Timer_Tick;
            _TetrisGrid = TetrisGrid;
        }

        public void StartGame()
        {
            _Timer.Start();
            _Score = 0;
            _GameBoard.MakeNewBlock();

        }
        void _Timer_Tick(object sender,EventArgs e)
        {
            //Move Block (1 down)
            _GameBoard.MoveDownBlockTick(new Point(0, 1));              
        }
        
        public void ChangeBlockPosition(KeyEventArgs eKeyPressed)
        { 
            switch (eKeyPressed.Key)
            {
                case Key.A:
                    _GameBoard.MoveBlockLeftRight(new Point(-1, 0));
                    break;
                case Key.D:
                    _GameBoard.MoveBlockLeftRight(new Point(1, 0));
                    break;
                case Key.S:
                    _GameBoard.MoveDownBlockButton();
                    break;
                case Key.K:
                    _GameBoard.RotateShape();
                    break;
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        



    }
}
