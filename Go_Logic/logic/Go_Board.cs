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

        public void Add_Stone((int, int) coordinates, Player color)
        {
            board_dict.Add(coordinates, color);
        }

        public int Get_size()
        {
            return size;
        }

        public bool Check_empty((int, int) coordinates)
        {
            return board_dict.ContainsKey(coordinates);
        }

        /// <summary>
        /// returns true if the position is occupied
        /// </summary>
        /// <param name="coordinates"></param>
        /// <returns></returns>
        public bool IsOccupied((int, int) coordinates)
        {
            return board_dict.ContainsKey(coordinates);
        }

        /// <summary>
        /// returns true if the position is inside board
        /// </summary>
        /// <param name="coordinates"></param>
        /// <returns></returns>
        public bool IsInside((int, int) coordinates)
        {
            return coordinates.Item1 < size && coordinates.Item1 >= 0 && coordinates.Item2 < size && coordinates.Item2 >= 0;
        }

        /// <summary>
        /// retuns true if the piece is not on the edge of the board
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        public bool NotAnEdge((int, int) coordinates)
        {
            return coordinates.Item1 != 0 && coordinates.Item1 != size - 1 && coordinates.Item2 != 0 && coordinates.Item2 != size - 1;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Go_Board Oboard = (Go_Board)obj;
            return size == Oboard.size && Board_Equal(Oboard);
        }

        public bool Board_Equal(Go_Board board)
        {
            if (board_dict.Count != board.board_dict.Count)
            {
                return false;
            }
            foreach ((int, int) key in board_dict.Keys)
            {
                if (!board.board_dict.ContainsKey(key) || board.board_dict[key] != board_dict[key])
                {
                    return false;
                }
            }
            return true;
        }
        public Go_Board Copy()
        {
            if (this == null)
            {
                return null;
            }
            Go_Board copy = new Go_Board(this.size);
            foreach ((int, int) key in this.board_dict.Keys)
            {
                if (key != (-1, -1))
                    copy.board_dict.Add(key, this.board_dict[key]);
            }

            return copy;
        }
    }
}
