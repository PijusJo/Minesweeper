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
            Minefield = new Location[Ysize, Xsize];

            for (int x = 0; x < Xsize; x++)
            {
                for (int y = 0; y < Ysize; y++)
                {
                    Minefield[x, y] = new Location(x,y);
                }
            }


            XCoordOfMines = new int [numOfMiness];
            YCoordOfMines = new int [numOfMiness];
        }

        //value indicating the number of collumns of locations in the minefield
        public int Xsize { get; set; }
        //value indicating the number of rows of locations in the minefield
        public int Ysize { get; set; }

        public int NumberOfMines { get; set; }

        public Location[,] Minefield;

        private int[] XCoordOfMines;
        private int[] YCoordOfMines;

        //This function generates the board AFTER the first click, ensuring that the clicked location is not a mine and has no neighboring mines
        public void GenerateBoard(int StartXCoord, int StartYCoord)
        {
            int currNumOfMines = 0;
            Random rnd = new Random();
            while (currNumOfMines < NumberOfMines)
            {

                int Row = rnd.Next(Xsize);
                int Collumn = rnd.Next(Ysize);

                //Ensuring the starting location (where the user clicked) is free from bombs
                if (!(Math.Abs(StartYCoord - Collumn) <=1 &&
                      Math.Abs(StartXCoord - Row) <=1))
                {
                    bool alreadyExists = false;
                    for (int i = 0; i < currNumOfMines; i++)
                    {
                        if (YCoordOfMines[i] == Collumn && XCoordOfMines[i] == Row)
                        {
                            alreadyExists = true;
                            break;
                        }
                    }
                    if (!alreadyExists) {
                        XCoordOfMines[currNumOfMines] = Row;
                        YCoordOfMines[currNumOfMines] = Collumn;
                        Minefield[Row,Collumn].IsAMine = true;
                        currNumOfMines++;
                    }
                }

            }

            //logic for udpating the NearbyMines count for each location
            //becauses we have each bombs location, we can just add +1 to each neighboring location
            for (int i = 0; i < NumberOfMines; i++)
            {
                for (int x = Math.Max(0, XCoordOfMines[i] - 1); x < Math.Min(Xsize, XCoordOfMines[i] + 2); x++)
                    for (int y = Math.Max(0, YCoordOfMines[i] - 1); y < Math.Min(Ysize, YCoordOfMines[i] + 2); y++)
                        Minefield[x, y].NearbyMines++;
            }
            UpdateLocation(StartXCoord, StartYCoord);
        }

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
        //update locations starting from the given
        private void UpdateLocation(int XCoord, int YCoord)
        {
            Minefield[XCoord, YCoord].HasBeenRevealed = true;
            if(Minefield[XCoord, YCoord].NearbyMines==0)
                for (int x = Math.Max(0, XCoord - 1); x < Math.Min(Xsize, XCoord + 2); x++)
                    for (int y = Math.Max(0, YCoord - 1); y < Math.Min(Ysize, YCoord + 2); y++)
                        if (x!= XCoord || y!= YCoord)
                            if (!Minefield[x, y].HasBeenRevealed)
                                UpdateLocation(x,y);
        }


        public bool CheckLocation(int XCoord, int YCoord)
        {
            if (Minefield[XCoord, YCoord].IsAMine)
            {
                //Console.Clear();
                //Console.WriteLine("BOOOOM");
                return false;
            }
            UpdateLocation(XCoord, YCoord);
            return true;
        }
    }
}
