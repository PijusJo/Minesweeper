using System.Collections.ObjectModel;
using System.Windows.Data;
using Minesweeper.Model;
using Minesweeper.MVVM;

namespace Minesweeper.ViewModel
{
    public class BoardViewModel : ViewModelBase
    {
        public int Collumns { get; set; }

        public int Rows { get; set; }

        public int NumOfMines { get; set; }

        public int StartXCoord { get; set; } = 2;

        public int StartYCoord { get; set; } = 2;

        public Board GameBoard { get; set; }
           
        public LocationViewModel[,] Cells { get; set; }

        public BoardViewModel(int collumns = 5, int rows = 5, int numOfMines = 2)
        {
            Collumns = collumns;
            Rows = rows;
            NumOfMines = numOfMines;
            GameBoard = new Board(Rows, Collumns, NumOfMines);
            Cells = new LocationViewModel[Rows,Collumns];
            for (int x = 0; x < Rows; x++)
                for(int y = 0; y < Collumns; y++)
                {
                    Cells[x, y] = new LocationViewModel(this, GameBoard.Minefield[x, y]);
                }
            
            
            

        }
        public void CheckLocation(int x, int y)
        {
            GameBoard.CheckLocation(x, y);
        }
    }
}

