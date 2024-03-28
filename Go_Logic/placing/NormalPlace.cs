using Go_Logic.data_structures;

namespace Go_Logic
{
    public class NormalPlace : Place
    {
        public override PlaceType Type => PlaceType.Normal;
        public override Coordinates ToCordinates { get; }

        public NormalPlace(Coordinates cordinates)
        {
            ToCordinates = cordinates;
        }

        public override void Execute(Go_Board board)// temporary
        {
            if (board.IsInside(ToCordinates) && !board.IsOccupied(ToCordinates))
            {
                board.Add_Stone(ToCordinates, Player.Black);
            }
        }   

    }
}
