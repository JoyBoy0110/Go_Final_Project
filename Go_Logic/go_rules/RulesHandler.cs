namespace Go_Logic
{
    public class RulesHandler
    {
        protected (int, int)[] directions = {
            new (0, -1),
            new (-1, 0),
            new (0, 1),
            new (1, 0) };

        protected GameState state;

        public RulesHandler(GameState state)
        {
            this.state = state;
        }
    }
}
