using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Tetris.ViewModel.Commands
{
    public class ChangePositionCommand : ICommand
    {
        private readonly GameFieldViewModel _ViewModel;
        public event EventHandler CanExecuteChanged;

        public ChangePositionCommand(GameFieldViewModel viewModel)
        {
            _ViewModel = viewModel;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            string direct = parameter as string;
            _ViewModel.ChangePosition(direct);
        }
    }
}
