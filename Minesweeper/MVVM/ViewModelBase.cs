using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Minesweeper.MVVM
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class CellSizeChangedEventArgs : EventArgs
    {
        public int NewCellSize { get;}

        public CellSizeChangedEventArgs ( int newCellSize)
        {
            NewCellSize = newCellSize;
        }
    }

}
