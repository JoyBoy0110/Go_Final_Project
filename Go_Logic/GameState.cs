using Go_Logic.data_structures;

namespace Go_Logic
{
    public class GameState
    {
        public Go_Board Board { get; }

        public Player Player { get; private set; }

        public int blackStoneCounter { get; private set; }

        public int whiteStoneCounter { get; private set; }

        private int boardSize;

        public GameState(Player player, Go_Board board)
        {
            Player = player;
            Board = board;
            boardSize = board.Get_size();
            blackStoneCounter = (boardSize * boardSize) / 2 + (boardSize * boardSize) % 2;
            whiteStoneCounter = (boardSize * boardSize) / 2;
        }

        public int DecreaseStone()
        {
            switch (Player)
            {
                case Player.Black:
                    blackStoneCounter--;
                    return blackStoneCounter;
                case Player.White:
                    whiteStoneCounter--;
                    return whiteStoneCounter;
                default:
                    return 0;
            }
        }
        public void Switch()
        {
            Player = Player.Opponnent();
        }
        
        public bool CanAdd()
        {
            switch (Player)
            {
                case Player.Black:
                    return blackStoneCounter > 0;
                case Player.White:
                    return whiteStoneCounter > 0;
                default:
                    return false;
            }
        }
    }
}
