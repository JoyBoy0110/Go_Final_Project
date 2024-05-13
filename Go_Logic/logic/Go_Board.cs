namespace Go_Logic
{
    public class Go_Board
    {
        public Dictionary<(int, int), Player>
            board_dict; // the board of the game, key = coordinates (row,column), value = color of piece

        private int size; // the size of the board, for example if it's 9 the board is 9x9 board

        /// <summary>
        /// a constructor for Go_Board, initialize size and dictionary
        /// </summary>
        /// <param name="size"></param>
        public Go_Board(int size)
        {
            this.size = size;
            this.board_dict = new Dictionary<(int, int), Player>();
        }

        /// <summary>
        /// add a stone to the board
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="color"></param>
        public void Add_Stone((int, int) coordinates, Player color)
        {
            board_dict.Add(coordinates, color);
        }

        /// <summary>
        /// returns the size of the board
        /// </summary>
        /// <returns></returns>
        public int Get_size()
        {
            return size;
        }

        /// <summary>
        /// checks if the position is empty
        /// </summary>
        /// <param name="coordinates"></param>
        /// <returns></returns>
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
            return coordinates.Item1 < size && coordinates.Item1 >= 0 && coordinates.Item2 < size &&
                   coordinates.Item2 >= 0;
        }

        /// <summary>
        /// returns true if the piece is not on the edge of the board
        /// </summary>
        /// <returns></returns>
        public bool NotAnEdge((int, int) coordinates)
        {
            return coordinates.Item1 != 0 && coordinates.Item1 != size - 1 && coordinates.Item2 != 0 &&
                   coordinates.Item2 != size - 1;
        }

        /// <summary>
        /// an override for the Equals method, checks if the size and the board are equal
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Go_Board Oboard = (Go_Board)obj;
            return size == Oboard.size && Board_Equal(Oboard);
        }

        /// <summary>
        /// a side function for the Equals method, checks if the board is equal by keys and their values
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
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

        /// <summary>
        /// a copy method for the board
        /// </summary>
        /// <returns></returns>
        public Go_Board Copy()
        {
            if (this == null)
            {
                return null;
            }

            Go_Board copy = new Go_Board(this.size);
            foreach ((int, int) key in this.board_dict.Keys)
            {
                if (key != (-1, -1) && board_dict[key] != Player.None)
                    copy.board_dict.Add(key, this.board_dict[key]);
                if (key == (-1, -1) && board_dict[key] == Player.None)
                    copy.board_dict.Add(key, this.board_dict[key]);
            }

            return copy;
        }

        /// <summary>
        /// returns the cordinates of the corners of the board
        /// </summary>
        /// <returns></returns>
        public (int, int)[] GetCorners()
        {
            // square of size if after decimal its more than 0.5 then add 1 if less than 0.5 then add 0
            int addedValue = 0;
            if (Math.Sqrt(size) % 1 < 0.5)
                addedValue = (int)Math.Floor(Math.Sqrt(size)) - 1;
            if (Math.Sqrt(size) % 1 >= 0.5)
                addedValue = (int)Math.Ceiling(Math.Sqrt(size)) - 1;
            return new (int, int)[] { (addedValue, addedValue), (size - addedValue - 1, addedValue),
                (size - addedValue - 1, size - addedValue - 1), (addedValue, size - addedValue - 1)};
        }

        /// <summary>
        /// cleans resources
        /// </summary>
        public void Clean()
        {
            board_dict.Clear();
        }
    }
}