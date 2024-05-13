namespace Go_Logic
{
    public enum Player // the color of a player, none is a default/ null option
    {
        Black,
        White,
        None
    }
    public static class PlayerExtensions
    {
        /// <summary>
        /// returns the opposit of the player that the function gets
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static Player Opponnent(this Player player)
        {
            switch (player)
            {
                case Player.Black:
                    return Player.White;

                case Player.White:
                    return Player.Black;
                default:
                    return Player.None;
            }
        }
    }

}
