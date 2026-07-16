using System.Windows.Shapes;
using Minesweeper.MVVM;

namespace Minesweeper.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public RelayCommand GenerateBoardCommand => new RelayCommand(exceute => SetBoard(), canExecute => !BoardIsSet);
        public RelayCommand GoBackToSettingsCommand => new RelayCommand(exceute => BackToSettings(), canExecute => BoardIsSet);

        public RelayCommand IncreaseCellSizeCommand => new RelayCommand(exceute => IncreaseCellSize(), canExecute => { return true; });
        public RelayCommand DecreaseCellSizeCommand => new RelayCommand(exceute => DecreaseCellSize(), canExecute => CellSize>10);


        public event EventHandler<CellSizeChangedEventArgs>? CellSizeChanged;

        private int CellSize { get; set; }


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

        /// <summary>
        /// varaible used to turn on and off the settings row (row and column size, mine count)
        /// </summary>
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

        /// <summary>
        /// varaible used to turn on and off the Game statistics row (unflagged mines count, locations left to be revealed) 
        /// </summary>
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
            CellSize = 40;
            BoardVM = new BoardViewModel(this, Rows, Columns, Mines, CellSize);
            BoardIsSet = false;
            BoardSettingsRowSize = "100";
            GameStatsRowSize = "0";
            
        }

        public void SetBoard ()
        {
            BoardIsSet = true;
            BoardVM = new BoardViewModel(this, Rows, Columns, Mines, CellSize);
            BoardSettingsRowSize = "0";
            GameStatsRowSize = "100";
        }
        /// <summary>
        /// Sets the board to 10x10 with 20 mines
        /// </summary>
        public void SetBoardNoCustomSettings()
        {
            BoardIsSet = true;
            BoardSettingsRowSize = "0";
            GameStatsRowSize = "100";
        }

        public void BackToSettings()
        {
            Rows = 10;
            Columns = 10;
            Mines = 20;
            BoardVM = new BoardViewModel(this, Rows, Columns, Mines, CellSize);
            BoardIsSet = false;
            BoardSettingsRowSize = "100";
            GameStatsRowSize = "0*";
        }

        /// <summary>
        /// Decreases current cell size by 5, if current size is not less than 10
        /// </summary>
        private void DecreaseCellSize()
        {
            if(CellSize > 10)
            {
                CellSize -=5;
                CellSizeChanged.Invoke(this, new CellSizeChangedEventArgs(CellSize));
            }
        }
        private void IncreaseCellSize()
        {
            
            CellSize += 5;
            CellSizeChanged.Invoke(this, new CellSizeChangedEventArgs(CellSize));
        }
    }
}
