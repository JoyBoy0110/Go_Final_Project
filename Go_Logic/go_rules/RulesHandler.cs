namespace Go_Logic
{
    public class RulesHandler
    {
        /// <summary>
        /// the directions in which the pieces can be placed from a certain position
        /// </summary>
        protected (int, int)[] directions = {
            new (0, -1),
            new (-1, 0),
            new (0, 1),
            new (1, 0) };

        protected GameState state;// the current state of the game

        /// <summary>
        /// a constructor for the RulesHandler
        /// </summary>
        /// <param name="state"></param>
        public RulesHandler(GameState state)
        {
            this.state = state;
        }
        /// <summary>
        /// updates the state of the game
        /// </summary>
        /// <param name="state"></param>
        public void Update(GameState state)
        {
            this.state = state;
        }
    }
}
