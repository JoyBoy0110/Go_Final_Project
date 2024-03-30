namespace Go_Logic
{
    public class Go_Board
    {
        public Dictionary<(int, int), Player> board_dict;
        private int size;

        public Go_Board(int size)
        {
            this.size = size;
            this.board_dict = new Dictionary<(int, int), Player>();
        }

        public void Add_Stone((int,int) coordinates, Player color)
        {
            board_dict.Add(coordinates, color);
        }

        public int Get_size()
        {
            return size;
        }

        public bool Check_empty((int,int) coordinates)
        {
            return board_dict.ContainsKey(coordinates);
        }

        /// <summary>
        /// returns true if the position is occupied
        /// </summary>
        /// <param name="coordinates"></param>
        /// <returns></returns>
        public bool IsOccupied((int,int) coordinates)
        {
            return board_dict.ContainsKey(coordinates);
        }

        /// <summary>
        /// returns true if the position is inside board
        /// </summary>
        /// <param name="coordinates"></param>
        /// <returns></returns>
        public bool IsInside((int,int) coordinates)
        {
            return coordinates.Item1 < size && coordinates.Item1 >= 0 && coordinates.Item2 < size && coordinates.Item2 >= 0;
        }


        public override bool Equals(object obj)
        {
            return obj is Go_Board board && size == board.size;
        }

        public override int GetHashCode()
        {
            int hashCode = 774960232;
            hashCode = hashCode * -1521134295 + EqualityComparer<Dictionary<(int,int), Player>>.Default.GetHashCode(board_dict);
            hashCode = hashCode * -1521134295 + size.GetHashCode();
            return hashCode;
        }

        public Go_Board Copy()
        {
            Go_Board copy = new Go_Board(this.size);
            foreach ((int,int) key in this.board_dict.Keys)
            {
                copy.board_dict.Add(key, this.board_dict[key]);
            }

            return copy;
        }
    }
}
