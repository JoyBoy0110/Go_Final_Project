namespace Go_Logic
{
    public enum EndType
    {
        pass,
        resign,
        material
    }
    public class EndGameHandler : RulesHandler
    {
        
        public EndGameHandler(GameState state) : base(state)
        {
        }

        /// <summary>
        /// returns the winning status by the endtype it has gotten
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Player EndGame(EndType type)
        {
            if (type == EndType.pass || type == EndType.material)
            {
                double blackScore = SimpleCalculateScore(Player.Black), whiteScore = SimpleCalculateScore(Player.White);   
                if(blackScore == whiteScore)
                    return Player.None;
                if(whiteScore > blackScore)
                    return Player.White;
                if(whiteScore < blackScore)
                    return Player.Black;

            }
            if (type == EndType.resign)
            {
                return this.state.Player.Opponnent();
            }
            return Player.None;
        }
        
        /// <summary>
        /// gets the color of a player and calculates his score by the simple method
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public double SimpleCalculateScore(Player player)
        {
            double score = 0;

            foreach((int, int) coord in this.state.Board.board_dict.Keys)
            {
                if (this.state.Board.board_dict[coord] == player)
                {
                    score++;
                }
            }
            switch(player)
            {
                case Player.Black:
                    score += this.state.blackScore;
                    break;
                case Player.White:
                    score += this.state.whiteScore;
                    break;
                default:
                    break;
            }
            return score;
        }

    }
}
