using Go_Logic;

namespace Go_AI
{
    public interface IAI
    {
        public void Update(GameState state);
        public (int, int) GetMove();
    }
}
