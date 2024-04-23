using Go_Logic;

namespace Go_AI
{
    public class RandomMoverAI : IAI
    {
        private GameState gamestate;
        //create a simple constructor
        public RandomMoverAI(GameState gamestate)
        {
            this.gamestate = gamestate;
        }
        public void Update(GameState state)
        {
            this.gamestate = state;
        }
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
