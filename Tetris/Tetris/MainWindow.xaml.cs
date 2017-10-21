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
using Tetris.Model;
using Tetris.View;
namespace Tetris
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Game NewGame;
        public MainWindow()
        {
            InitializeComponent();
            NewGame = new Game(GridField.ReturnGrid);
            NewGame.StartGame();
            
        }
        
        private void Window_Initialized(object sender, EventArgs e)
        {

        }
        private void Window_KeyDown(object sender, KeyEventArgs eKeyPressed)
        {           
            NewGame.ChangeBlockPosition(eKeyPressed);
        }
    }
}
