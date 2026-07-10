using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Animation;
using Minesweeper.Model;
using Minesweeper.MVVM;

namespace Minesweeper.ViewModel
{
    public class BoardViewModel : ViewModelBase
    {
        private bool hasBeenStarted {  get; set; }
        public bool HasBeenStarted 
        {
            get 
            { 
                return hasBeenStarted; 
            }

            set 
            {
                hasBeenStarted = value;
                OnPropertyChanged();
            } 
        
        }
        public int Columns { get; set; }

        public int Rows { get; set; }

        public int NumOfMines { get; set; }

        public Board GameBoard { get; set; }

        //number of locations flagged, need to be equal 
        public int Flaggednumber { get; set; }

        public int UnrevealedLocations {  get; set; }
        
        private LocationViewModel[,] cells { get; set; }
        public LocationViewModel[,] Cells
        {
            get 
            { 
                return cells; 
            }


            set
            { 
                cells = value;
                OnPropertyChanged();
            } 
        }

        public IEnumerable<LocationViewModel> FlattenedCells => Cells.Cast<LocationViewModel>();
        public BoardViewModel(int columns = 5, int rows = 5, int numOfMines = 2)
        {
            Columns = columns;
            Rows = rows;
            NumOfMines = numOfMines;
            GameBoard = new Board(Rows, Columns, NumOfMines);
            cells = new LocationViewModel[Rows,Columns];
            for (int x = 0; x < Rows; x++)
                for (int y = 0; y < Columns; y++)
                {
                    cells[x, y] = new LocationViewModel(this, GameBoard.Minefield[x, y]);
                } 
            HasBeenStarted = false;

            Flaggednumber = 0;
            UnrevealedLocations = Rows*Columns - NumOfMines;
        }
        public void UpdateLocation(int XCoord, int YCoord)
        {
            if (!GameBoard.Minefield[XCoord, YCoord].HasBeenRevealed)
            {
                UnrevealedLocations--;
                GameBoard.Minefield[XCoord, YCoord].HasBeenRevealed = true;
                cells[XCoord, YCoord].Reveal();
                if (GameBoard.Minefield[XCoord, YCoord].NearbyMines == 0)
                    for (int x = Math.Max(0, XCoord - 1); x < Math.Min(Rows, XCoord + 2); x++)
                        for (int y = Math.Max(0, YCoord - 1); y < Math.Min(Columns, YCoord + 2); y++)
                            if (x != XCoord || y != YCoord)
                                if (!GameBoard.Minefield[x, y].HasBeenRevealed && !GameBoard.Minefield[x, y].MarkedAsMine)
                                    UpdateLocation(x, y);
                CheckVictory();
            } 
        }
        

        public void CheckLocation(int x, int y)
        {
            if (!hasBeenStarted)
            {
                GenerateBoard(x, y);
                hasBeenStarted = true;
            }
            if (GameBoard.Minefield[x, y].IsAMine)
            {
                cells[x,y].Reveal();
                for (int i = 0; i < GameBoard.NumberOfMines; i++)
                {
                    cells[GameBoard.XCoordOfMines[i], GameBoard.YCoordOfMines[i]].Reveal();
                }
                MessageBox.Show("BOOOOOOOOOOOOOOM!!!!!", "YOU DIED", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            UpdateLocation(x, y);
        }


        public void CheckVictory()
        {
            if (Flaggednumber==GameBoard.NumberOfMines && UnrevealedLocations==0)
            {
                MessageBox.Show("YAYYYYYYYYYYYYYYYYYYYYYYY YOU WON !!!!", "Victory!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }


        public void MarkLocationAsMine(int x, int y)
        {
            if (!GameBoard.Minefield[x, y].HasBeenRevealed)
            {
                if (!GameBoard.Minefield[x, y].MarkedAsMine)
                {
                    GameBoard.Minefield[x, y].MarkedAsMine = true;
                    Flaggednumber++;
                    CheckVictory();
                }
                else
                {
                    GameBoard.Minefield[x, y].MarkedAsMine = false;
                    Flaggednumber--;
                }
            }
        }
        
        public void FirstReveal(int x, int y)
        {
            GenerateBoard(x, y);
            UpdateLocation(x,y);
        }
        //This function generates the board AFTER the first click, ensuring that the clicked location is not a mine and has no neighboring mines
        public void GenerateBoard(int StartXCoord, int StartYCoord)
        {
            int currNumOfMines = 0;
            Random rnd = new Random();
            while (currNumOfMines < GameBoard.NumberOfMines)
            {

                int Row = rnd.Next(Rows);
                int Column = rnd.Next(Columns);

                //Ensuring the starting location (where the user clicked) is free from bombs
                if (!(Math.Abs(StartYCoord - Column) <= 1 &&
                      Math.Abs(StartXCoord - Row) <= 1))
                {
                    bool alreadyExists = false;
                    for (int i = 0; i < currNumOfMines; i++)
                    {
                        if (GameBoard.YCoordOfMines[i] == Column && GameBoard.XCoordOfMines[i] == Row)
                        {
                            alreadyExists = true;
                            break;
                        }
                    }
                    if (!alreadyExists)
                    {
                        GameBoard.XCoordOfMines[currNumOfMines] = Row;
                        GameBoard.YCoordOfMines[currNumOfMines] = Column;
                        GameBoard.Minefield[Row, Column].IsAMine = true;
                        currNumOfMines++;
                    }
                }
            }

            //logic for udpating the NearbyMines count for each location
            //becauses we have each bombs location, we can just add +1 to each neighboring location
            for (int i = 0; i < GameBoard.NumberOfMines; i++)
            {
                for (int x = Math.Max(0, GameBoard.XCoordOfMines[i] - 1); x < Math.Min(GameBoard.Xsize, GameBoard.XCoordOfMines[i] + 2); x++)
                    for (int y = Math.Max(0, GameBoard.YCoordOfMines[i] - 1); y < Math.Min(GameBoard.Ysize, GameBoard.YCoordOfMines[i] + 2); y++)
                    {
                        GameBoard.Minefield[x, y].NearbyMines++;
                    }
                        
            }
        }

    }
}

