namespace Go_Logic
{
    public class GroupHandler : RulesHandler
    {
        public GroupHandler(GameState state) : base(state)
        {
        }
        /// <summary>
        /// returns all the groups that are on the board
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<(int, int), Player>> GetGroups()
        {
            bool flag;
            List<Dictionary<(int, int), Player>> groups = new List<Dictionary<(int, int), Player>>();
            foreach ((int, int) coord in this.state.Board.board_dict.Keys)
            {
                flag = true;
                foreach (Dictionary<(int, int), Player> group in groups)
                {
                    if (group.ContainsKey(coord))
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    groups.Add(GetGroup(coord, this.state.Board.board_dict[coord]));
                }
            }
            return groups;
        }

        public Dictionary<(int, int), Player> GetGroup((int, int) coord, Go_Board board)
        {
            return GetGroup(coord, board.board_dict[coord]);
        }
        /// <summary>
        /// sends an empty group and returns the group that the coordinate is in
        /// </summary>
        /// <param name="coord"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public Dictionary<(int, int), Player> GetGroup((int, int) coord, Player color)
        {
            return GetGroup(coord, color, new Dictionary<(int, int), Player>());
        }

        /// <summary>
        /// a recursive function that returns the group that the coordinate is in
        /// </summary>
        /// <param name="coord"></param>
        /// <param name="color"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        private Dictionary<(int, int), Player> GetGroup((int, int) coord, Player color, Dictionary<(int, int), Player> group)
        {
            if (this.state.Board.IsInside(coord) && this.state.Board.IsOccupied(coord) && this.state.Board.board_dict[coord] == color && !group.ContainsKey(coord))
            {
                group.Add(coord, color);
                group = GetGroup(new(coord.Item1 + directions[0].Item1, coord.Item2 + directions[0].Item2), color, group);
                group = GetGroup(new(coord.Item1 + directions[1].Item1, coord.Item2 + directions[1].Item2), color, group);
                group = GetGroup(new(coord.Item1 + directions[2].Item1, coord.Item2 + directions[2].Item2), color, group);
                group = GetGroup(new(coord.Item1 + directions[3].Item1, coord.Item2 + directions[3].Item2), color, group);
            }
            return group;
        }

    }
}
    