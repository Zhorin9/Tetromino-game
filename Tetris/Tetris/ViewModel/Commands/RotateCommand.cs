using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Tetris.ViewModel.Commands
{
    public class RotateCommand : ICommand
    {
        private readonly GameFieldViewModel _ViewModel;
        public event EventHandler CanExecuteChanged;

        public RotateCommand(GameFieldViewModel viewModel)
        {
            _ViewModel = viewModel;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _ViewModel.RotateBlock();
        }
    }
}
