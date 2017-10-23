using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Tetris.Model;
using Tetris.View;
using Tetris.ViewModel.Commands;

namespace Tetris.ViewModel
{
    public class GameFieldViewModel : ViewModelBase
    {
        public int Score { get; set; }
        public Grid TetrisGrig { get; set; }

        public ChangePositionCommand MoveClick { get; set; }
        public RotateCommand RotateClick { get; set; }

        private Board _GameBoard { get; set; }

        public GameFieldViewModel()
        {
            RotateClick = new RotateCommand(this);
            MoveClick = new ChangePositionCommand(this);

            _GameBoard = new Board();

        }


        public void ChangePosition(string direction)
        {
            Console.WriteLine(direction);
        }
        public void RotateBlock()
        {
            Console.WriteLine("rotate");
        }

    }
}
