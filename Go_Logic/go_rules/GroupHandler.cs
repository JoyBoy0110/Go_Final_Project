namespace Go_Logic
{
    public class GroupHandler
    {
        private (int, int)[] directions = {
            new (0, -1),
            new (-1, 0),
            new (0, 1),
            new (1, 0) };

        private GameState state;

        public GroupHandler(GameState state)
        {
            this.state = state;
        }

        public List<Dictionary<(int, int), Player>> GetGroups()
        {
            bool flag = false;
            List<Dictionary<(int, int), Player>> groups = new List<Dictionary<(int, int), Player>>();
            foreach ((int, int) coord in this.state.Board.board_dict.Keys)
            {
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

        private Dictionary<(int, int), Player> GetGroup((int, int) coord, Player color)
        {
            return GetGroup(coord, color, new Dictionary<(int, int), Player>());
        }

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
