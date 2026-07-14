using System.Windows;
using Minesweeper.Model;
using Minesweeper.MVVM;

namespace Minesweeper.ViewModel
{
    public class LocationViewModel : ViewModelBase
    {
        private readonly BoardViewModel _board;
        public Location currentlocation { get; }

        public bool Marked {  get; set; }

        private string colour { get; set; }

        //value, which sets the buttons background colour
        public string Colour
        {
            get
            {
                return colour;
            }
            set
            {
                colour = value;
                OnPropertyChanged();
            }
        }

        private string text {  get; set; }

        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                OnPropertyChanged();
            }
        }
        private string btntext { get; set; }

        public string BtnText
        {
            get
            {
                return btntext;
            }
            set
            {
                btntext = value;
                OnPropertyChanged();
            }
        }


        private int opacity;
        public int Opacity 
        {
            get { return opacity; }

            set
            {
                opacity = value;
                OnPropertyChanged();
            }  
        }


        public RelayCommand LeftClickCommand => new RelayCommand(exceute => LeftClick(), canExecute => !Marked);
        public RelayCommand RightClickCommand => new RelayCommand(exceute => RightClick(), canExecute => !currentlocation.HasBeenRevealed);

        public LocationViewModel(BoardViewModel board, Location location)
        {
            _board = board;
            currentlocation = location;
            Opacity = 100;
            Text = "";
            Colour = "Gray";
            Marked = false;
            BtnText = "";

        }
        private void LeftClick()
        {
            if (!Marked)
            {
                if (!currentlocation.HasBeenRevealed)
                    _board.CheckLocation(currentlocation.Xcoord, currentlocation.Ycoord);
                else
                {
                    _board.CheckNeighbours3X3(currentlocation.Xcoord, currentlocation.Ycoord);
                }
            }
            

        }
        private void RightClick()
        {
            if (!currentlocation.HasBeenRevealed)
            {
                UpdateMarking();
                _board.MarkLocationAsMine(currentlocation.Xcoord, currentlocation.Ycoord);
            }   
        }

        public void Reveal()
        {
            if (!currentlocation.IsAMine)
            {
                Opacity = 0;
                if (currentlocation.NearbyMines != 0)
                {
                    Text= currentlocation.NearbyMines.ToString();
                }

            }
            else
            {
                Colour = "Red";
            }
        }
        public void UpdateMarking()
        {
            if (Marked)
            {
                BtnText = "";
                Marked = false;
                //Colour = "Gray";
            }
            else
            {
                BtnText = "X";
                Marked = true;
                //Colour = "White";
            }
                
        }

    }
}
