namespace Go_Logic
{
    public class CuptureHandler : RulesHandler
    {
        public CuptureHandler(GameState state) : base(state)
        {
        }

        /// <summary>
        /// removes the group that has got eaten from the board
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public Go_Board Capture(Dictionary<(int, int), Player> group)
        {
            Go_Board copy = this.state.Board.Copy();
            foreach ((int, int) cordinates in group.Keys)
            {
                copy.board_dict.Remove(cordinates);
            }
            return copy;
        }
    }
}
