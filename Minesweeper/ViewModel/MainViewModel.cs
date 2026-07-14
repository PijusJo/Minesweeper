using System.Windows.Shapes;
using Minesweeper.MVVM;

namespace Minesweeper.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public RelayCommand GenerateBoardCommand => new RelayCommand(exceute => SetBoard(), canExecute => !BoardIsSet);
        public RelayCommand GoBackToSettingsCommand => new RelayCommand(exceute => BackToSettings(), canExecute => BoardIsSet);

        private BoardViewModel boardVM{  get; set; }

        public BoardViewModel BoardVM
        {
            get 
            { 
                return boardVM;
            }
            set 
            {
                boardVM = value;
                OnPropertyChanged();
            }
        }

        public bool BoardIsSet { get; set; }

        
        private string boardSettingsRowSize { get; set; }

        //varaible used to turn on and off the settings row (row and column size, mine count)
        public string BoardSettingsRowSize
        {
            get
            {
                return boardSettingsRowSize;
            }
            set
            {
                boardSettingsRowSize = value;
                OnPropertyChanged();
            }
        }

        private string gameStatsRowSize { get; set; }

        //varaible used to turn on and off the Game statistics row (unflagged mines count, locations left to be revealed) 
        public string GameStatsRowSize
        {
            get
            {
                return gameStatsRowSize;
            }
            set
            {
                gameStatsRowSize = value;
                OnPropertyChanged();
            }
        }

        private int rows { get; set; }
        public int Rows 
        { 
            get
            {
                return rows;
            } 
            set
            {
                rows = value;
                OnPropertyChanged();
            } 
        }

        private int columns { get; set; }
        public int Columns
        {
            get
            {
                return columns;
            }
            set
            {
                columns = value;
                OnPropertyChanged();
            }
        }

        private int mines { get; set; }
        public int Mines
        {
            get
            {
                return mines;
            }
            set
            {
                mines = value;
                OnPropertyChanged();
            }
        }
        public MainViewModel()
        {
            Rows = 10;
            Columns = 10;
            Mines = 20;
            BoardVM = new BoardViewModel(this, Rows, Columns, Mines);
            BoardIsSet = false;
            BoardSettingsRowSize = "1*";
            GameStatsRowSize = "0*";
        }

        public void SetBoard ()
        {
            BoardIsSet = true;
            BoardVM = new BoardViewModel(this, Rows, Columns, Mines);
            BoardSettingsRowSize = "0*";
            GameStatsRowSize = "1*";
        }
        public void SetBoardNoCustomSettings()
        {
            BoardIsSet = true;
            BoardSettingsRowSize = "0*";
            GameStatsRowSize = "1*";
        }

        public void BackToSettings()
        {
            Rows = 10;
            Columns = 10;
            Mines = 20;
            BoardVM = new BoardViewModel(this, Rows, Columns, Mines);
            BoardIsSet = false;
            BoardSettingsRowSize = "1*";
            GameStatsRowSize = "0*";
        }

    }
}
