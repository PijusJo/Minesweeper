using System.Windows.Controls;
using Minesweeper.ViewModel;

namespace Minesweeper.View
{
    /// <summary>
    /// Interaction logic for Board.xaml
    /// </summary>
    public partial class Board : UserControl
    {
        public Board()
        {
            InitializeComponent();
            BoardViewModel vm = new BoardViewModel();
            DataContext = vm;
        }
    }
}
