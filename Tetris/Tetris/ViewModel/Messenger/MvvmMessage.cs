using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Tetris.ViewModel.Messenger
{
    class MvvmMessage
    {
        public Grid TetrisGrid { get; set; }
        public int Score { get; set; }
    }
}
