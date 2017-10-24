using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tetris.ViewModel.Messenger;

namespace Tetris.View
{
    /// <summary>
    /// Interaction logic for GameField.xaml
    /// </summary>
    public partial class GameField : UserControl
    {
        public GameField()
        {
            InitializeComponent();
            SendGrid(this.GridField);
        }
        private void SendGrid(Grid tetrisGrid)
        {
            var msg = new MvvmMessage() { TetrisGrid = tetrisGrid  };
            Messenger.Default.Send<MvvmMessage>(msg); 
        }

    }
}
