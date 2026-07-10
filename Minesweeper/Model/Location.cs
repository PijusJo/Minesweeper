
namespace Minesweeper.Model
{
    public class Location
    {
        //value indicating the The X coordinate (row number) of the mine
        public int Xcoord { get; set; }

        //value indicating the Y coordinate ( Collumn numbber) of the mine
        public int Ycoord { get; set; }

        public bool IsAMine { get; set; }

        public int NearbyMines { get; set; }

        public bool HasBeenRevealed { get; set; }

        public bool MarkedAsMine { get; set; }

        public Location(int xcoord, int ycoord, bool isAMine = false)
        {
            Xcoord = xcoord; 
            Ycoord = ycoord;
            IsAMine = isAMine;
            NearbyMines = 0;
            HasBeenRevealed = false;
            MarkedAsMine = false;
        }

    }
}
