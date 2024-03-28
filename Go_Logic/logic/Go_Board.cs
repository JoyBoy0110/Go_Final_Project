using Go_Logic.data_structures;

namespace Go_Logic
{
    public class Go_Board : Go_Equipment
    {
        public Dictionary<Coordinates, Player> board_dict;
        private int size;

        public Go_Board(int size)
        {
            this.size = size;
            this.board_dict = new Dictionary<Coordinates, Player>();
        }

        public void Add_Stone(Coordinates coordinates, Player color)
        {
            board_dict.Add(coordinates, color);
        }

        public int Get_size()
        {
            return size;
        }

        public bool Check_empty(Coordinates coordinates)
        {
            return board_dict.ContainsKey(coordinates);
        }

        /// <summary>
        /// returns true if the position is occupied
        /// </summary>
        /// <param name="coordinates"></param>
        /// <returns></returns>
        public bool IsOccupied(Coordinates coordinates)
        {
            return board_dict.ContainsKey(coordinates);
        }

        /// <summary>
        /// returns true if the position is inside board
        /// </summary>
        /// <param name="coordinates"></param>
        /// <returns></returns>
        public bool IsInside(Coordinates coordinates)
        {
            return coordinates.x < size && coordinates.x >= 0 && coordinates.y < size && coordinates.y >= 0;
        }


        public override bool Equals(object obj)
        {
            return obj is Go_Board board &&
                   EqualityComparer<Dictionary<Coordinates, Player>>.Default.Equals(board_dict, board.board_dict) &&
                   size == board.size;
        }

        public override int GetHashCode()
        {
            int hashCode = 774960232;
            hashCode = hashCode * -1521134295 + EqualityComparer<Dictionary<Coordinates, Player>>.Default.GetHashCode(board_dict);
            hashCode = hashCode * -1521134295 + size.GetHashCode();
            return hashCode;
        }

        public override Go_Equipment Copy()
        {
            Go_Board copy = new Go_Board(this.size);
            foreach (Coordinates key in this.board_dict.Keys)
            {
                copy.board_dict.Add(key, this.board_dict[key]);
            }

            return copy;
        }
    }
}
