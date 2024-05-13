using Go_Logic;

namespace Go_AI
{
    public interface IAI
    {
        
        /// <summary>
        /// updates the state of the game for the AI
        /// </summary>
        /// <param name="state"> updates the game state </param>
        public void Update(GameState state);
        
        /// <summary>
        /// returns the move that the AI decides to make
        /// </summary>
        /// <returns> a valid coordinate to put a stone in or (-1,-1) which will convert to a pass</returns>
        public (int, int) GetMove();

        /// <summary>
        /// cleans resources
        /// </summary>
        public void Clean();
    }
}
