namespace Go_Logic
{
    public abstract class Place
    {
        public abstract PlaceType Type { get;  }
        public abstract (int,int) ToCordinates{ get; }

        public abstract void Execute(Go_Board board);

    }
}
