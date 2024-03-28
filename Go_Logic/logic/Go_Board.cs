namespace Go_Logic
{
    public class Go_Board : Go_Equipment
    {
        public Dictionary<Tuple<int, int>, Player> board_dict;
        private int size;

        public Go_Board(int size)
        {
            this.size = size;
            this.board_dict = new Dictionary<Tuple<int, int>, Player>();
        }

        public void Add_Stone(Tuple<int, int> cordinates, Player color)
        {
            board_dict.Add(cordinates, color);
        }

        public int Get_size()
        {
            return size;
        }

        public bool Check_empty(Tuple<int, int> cordinates)
        {
            return board_dict.ContainsKey(cordinates);
        }

        /// <summary>
        /// returns true if the position is occupied
        /// </summary>
        /// <param name="cordinates"></param>
        /// <returns></returns>
        public bool IsOccupied(Tuple<int, int> cordinates)
        {
            return board_dict.ContainsKey(cordinates);
        }

        /// <summary>
        /// returns true if the position is inside board
        /// </summary>
        /// <param name="cordinates"></param>
        /// <returns></returns>
        public bool IsInside(Tuple<int, int> cordinates)
        {
            return cordinates.Item1 <= size && cordinates.Item1 >= 1 && cordinates.Item2 <= size && cordinates.Item2 >= 1;
        }

        public override bool Equals(object obj)
        {
            return obj is Go_Board board &&
                   EqualityComparer<Dictionary<Tuple<int, int>, Player>>.Default.Equals(board_dict, board.board_dict) &&
                   size == board.size;
        }

        public override int GetHashCode()
        {
            int hashCode = 774960232;
            hashCode = hashCode * -1521134295 + EqualityComparer<Dictionary<Tuple<int, int>, Player>>.Default.GetHashCode(board_dict);
            hashCode = hashCode * -1521134295 + size.GetHashCode();
            return hashCode;
        }

        public override Go_Equipment Copy()
        {
            Go_Board copy = new Go_Board(this.size);
            foreach (Tuple<int, int> key in this.board_dict.Keys)
            {
                copy.board_dict.Add(key, this.board_dict[key]);
            }

            return copy;
        }


        //public static bool operator ==(Go_Board left, Go_Board right)
        //{
        //    return EqualityComparer<Go_Board>.Default.Equals(left, right);
        //}

        //public static bool operator !=(go_board left, go_board right)
        //{
        //    return !(left == right);
        //}
    }
}
