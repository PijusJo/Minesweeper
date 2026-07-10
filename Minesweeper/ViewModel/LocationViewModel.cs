using System.Security.Cryptography.X509Certificates;
using System.Windows.Input;
using Minesweeper.Model;
using Minesweeper.MVVM;

namespace Minesweeper.ViewModel
{
    public class LocationViewModel : ViewModelBase
    {
        private readonly BoardViewModel _board;
        public Location currentlocation { get; }

        public RelayCommand LeftClickCommand => new RelayCommand(exceute => LeftClick(), canExecute => !currentlocation.HasBeenRevealed );
        public RelayCommand RightClickCommand => new RelayCommand(exceute => RightClick(), canExecute => currentlocation.HasBeenRevealed);

        public LocationViewModel(BoardViewModel board, Location location)
        {
            _board = board;
            currentlocation = location;
        }
        private void LeftClick()
        {
            
        }
        private void RightClick()
        {

        }

    }
}
