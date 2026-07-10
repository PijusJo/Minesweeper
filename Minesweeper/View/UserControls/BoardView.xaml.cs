using System.Windows.Controls;
using Minesweeper.ViewModel;

namespace Minesweeper.View.UserControls 
{
    /// <summary>
    /// Interaction logic for Board.xaml
    /// </summary>
    public partial class BoardView : UserControl
    {
        public BoardView()
        {
            InitializeComponent();
            BoardViewModel vm = new BoardViewModel(5, 5, 3);
            DataContext = vm;
        }
    }
}
