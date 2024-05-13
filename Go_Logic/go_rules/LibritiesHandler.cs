    namespace Go_Logic
{
    public class LibritiesHandler : RulesHandler
    {
        
        public LibritiesHandler(GameState state) : base(state)
        {
        }
        /// <summary>
        /// check if a group is captured, if it is return true, else return false
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool IsCaptured(Dictionary<(int, int), Player> group)
        {
            int liberties = GetNumberOfLibertiesOfGroup(group);
            if (liberties < 0 || liberties > 4 * group.Count)
            {
                throw new Exception("Invalid number of liberties: need to check code");

            }
            if (liberties == 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// gets a group and returns the number of liberties of a group
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public int GetNumberOfLibertiesOfGroup(Dictionary<(int, int), Player> group)// 1 stone is also a group
        {
            List<Dictionary<(int, int), Player>> libritiesList = new List<Dictionary<(int, int), Player>>();
            int liberties = 0, index = 0;
            foreach ((int, int) coord in group.Keys)
            {
                libritiesList.Add(new Dictionary<(int, int), Player>());
                libritiesList[index] = Get_Librities(coord);
                foreach ((int, int) cord in libritiesList[index].Keys)
                {
                    if (GroupNotContaintsLibrity(libritiesList, cord))
                    {
                        liberties++;
                    }
                }
                index++;
            }
            return liberties;
        }
        
        private bool GroupNotContaintsLibrity(List<Dictionary<(int, int), Player>> allLibrities, (int, int) cord)
        {
            for (int index = 0; index < allLibrities.Count - 1; index++)
            {
                Dictionary<(int, int), Player> librities = allLibrities[index];
                foreach ((int, int) cordinates in librities.Keys)
                {
                    if (cordinates == cord)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// gets a group and returns a dictionary contains the librities of that group
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public Dictionary<(int, int), Player> GetLibritiesOfGroup(Dictionary<(int, int), Player> group)
        {
            Dictionary<(int, int), Player> groupLibrities_dict = new Dictionary<(int, int), Player>();
            foreach ((int, int) coord in group.Keys)
            {
                Dictionary<(int, int), Player> librities = Get_Librities(coord);
                foreach ((int, int) cord in librities.Keys)
                {
                    if (!groupLibrities_dict.ContainsKey(cord))
                    {
                        groupLibrities_dict.Add(cord, librities[cord]);
                    }
                }
            }
            return groupLibrities_dict;
        }

        /// <summary>
        /// gets a coordinate and returns a dictionary contains the liberties of that coordinate
        /// </summary>
        /// <param name="cord"></param>
        /// <returns></returns>
        public Dictionary<(int, int), Player> Get_Librities((int, int) cord)
        {
            Dictionary<(int, int), Player> librity_dict = new Dictionary<(int, int), Player>();
            foreach ((int, int) direction in directions)
            {
                (int, int) neighbor = new(cord.Item1 + direction.Item1, cord.Item2 + direction.Item2);
                if (this.state.Board.IsInside(neighbor) && !this.state.Board.board_dict.ContainsKey(neighbor))
                {
                    librity_dict.Add(neighbor, Player.None);
                }
            }
            return librity_dict;
        }
    }
}
