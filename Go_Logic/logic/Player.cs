using System.Runtime.CompilerServices;

namespace Go_Logic
{
    public enum Player
    {
        Black,
        White,
        None
    }
    public static class PlayerExtensions
    {
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
