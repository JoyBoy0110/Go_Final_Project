using Go_Logic;

namespace Go_AI
{
    public class RandomMoverAI : IAI
    {
        private GameState gamestate;// the current state of the game
        
        /// <summary>
        /// a constructor for the RandomMoverAI
        /// </summary>
        /// <param name="gamestate"></param>
        public RandomMoverAI(GameState gamestate)
        {
            this.gamestate = gamestate;
        }

        public GameState GameState
        {
            get => default;
            set
            {
            }
        }

        public void Clean()
        {
            gamestate.Clean();
        }
        /// <summary>
        /// updates the state of the game
        /// </summary>
        /// <param name="state"></param>
        public void Update(GameState state)
        {
            this.gamestate = state;
        }

        /// <summary>
        /// returns a random move
        /// </summary>
        /// <returns></returns>
        public (int,int) GetMove()
        {
            int size = gamestate.Board.Get_size();
            GameState copy = gamestate.Copy();
            System.Random random = new System.Random();
            int x = random.Next(size);
            int y = random.Next(size);
            while (!copy.AddStone((x,y)))
            {
                x = random.Next(size);
                y = random.Next(size);
            }
            return (x, y);
        }
    }
}
