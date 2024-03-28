using Go_Logic.data_structures;

namespace Go_Logic
{
    public abstract class Place
    {
        public abstract PlaceType Type { get;  }
        public abstract Coordinates ToCordinates{ get; }

        public abstract void Execute(Go_Board board);

    }
}
