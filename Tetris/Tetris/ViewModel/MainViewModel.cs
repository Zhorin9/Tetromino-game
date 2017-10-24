using GalaSoft.MvvmLight;
using Tetris.Model;

namespace Tetris.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public int Score { get; set; }
        private Game _Game;
        public MainViewModel()
        {
            _Game = new Game();
        }

    }
}