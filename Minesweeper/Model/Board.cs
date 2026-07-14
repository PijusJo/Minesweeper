using Minesweeper.Model;

namespace Minesweeper.Model
{
    public class Board
    {
        public Board(int xSize, int ySize, int numOfMiness)
        {
            Xsize = xSize;
            Ysize = ySize;
            NumberOfMines = numOfMiness;
            Minefield = new Location[Xsize, Ysize];

            for (int x = 0; x < Xsize; x++)
            {
                for (int y = 0; y < Ysize; y++)
                {
                    Minefield[x, y] = new Location(x, y);
                 }
            }
            XCoordOfMines = new int[numOfMiness];
            YCoordOfMines = new int[numOfMiness];
        }

        //value indicating the number of collumns of locations in the minefield
        public int Xsize { get; set; }
        //value indicating the number of rows of locations in the minefield
        public int Ysize { get; set; }

        public int NumberOfMines { get; set; }

        public Location[,] Minefield;

        public int[] XCoordOfMines;
        public int[] YCoordOfMines;

        

        //converts the board to string for testing
        public string BoardToString(bool MustBeRevealed = false)
        {
            string board = "";
            for (int x = 0; x < Xsize; x++)
            {
                for (int y = 0; y < Ysize; y++)
                {
                    if (!MustBeRevealed || Minefield[x, y].HasBeenRevealed)
                    {
                        if (!Minefield[x, y].IsAMine)
                        {
                            board += Minefield[x, y].NearbyMines.ToString();
                        }
                        else
                        {
                            board += "X";
                        }
                    }
                    else
                    {
                        board += "█";
                    }

                }
                board += "\n";
            }
            return board;

        }
    }
}
