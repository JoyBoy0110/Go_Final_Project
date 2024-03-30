

namespace Go_Logic
{
    public class GameState
    {
        public Go_Board Board { get; private set; }
        public Player Player { get; private set; }
        private int boardSize;

        public int blackStoneCounter { get; private set; }
        public int whiteStoneCounter { get; private set; }
        public int blackCaptures{ get; private set; }
        public int whiteCaptures { get; private set; }

        public List<Dictionary<(int,int), Player>> boardGroups { get; private set; }
        private LibritiesHandler libritiesHandler;
        private GroupHandler groupHandler;
        private CuptureHandler cuptureHandler;


        public GameState(Player player, Go_Board board)
        {
            Player = player;
            Board = board;
            boardSize = board.Get_size();
            blackCaptures = 0;
            whiteCaptures = 0;
            blackStoneCounter = (boardSize * boardSize) / 2 + (boardSize * boardSize) % 2;
            whiteStoneCounter = (boardSize * boardSize) / 2;
            boardGroups = new List<Dictionary<(int, int), Player>>();
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
        public void AddCupturedStone(Player cuptured)
        {
            switch (cuptured)
            {
                case Player.Black:
                    whiteCaptures++;
                    break;
                case Player.White:
                    blackCaptures++;
                    break;
                default:
                    break;
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

        public void Update()
        {
            //psudo code
            // update groups
            // get librities
            // check if any group is captured
            // send to cupture handler
            //get the new board
            // update main board with updated board
            groupHandler = new GroupHandler(this);
            libritiesHandler = new LibritiesHandler(this);
            boardGroups = groupHandler.GetGroups();
            foreach (Dictionary<(int, int), Player> group in boardGroups)
            {
                if (libritiesHandler.IsCaptured(group))
                {
                    cuptureHandler = new CuptureHandler(this);
                    this.Board = cuptureHandler.Capture(group);
                    foreach ((int, int) cord in group.Keys)
                    {
                        AddCupturedStone(group[cord]);
                    }
                }
            }
        }

        public bool removeStone((int, int) coordinates)
        {
            if (Board.IsOccupied(coordinates))
            {
                switch (Board.board_dict[coordinates])
                {
                    case Player.Black:
                        blackStoneCounter++;
                        break;
                    case Player.White:
                        whiteStoneCounter++;
                        break;
                    default:
                        break;
                }
                Board.board_dict.Remove(coordinates);
                return true;
            }
            return false;
        }

        //public Place GetPlacingState((int, int) cordinates)
        //{
        //    // if the position is occupied or outside the board
        //    if (this.state.Board.IsOccupied(cordinates) || !this.state.Board.IsInside(cordinates))
        //    {
        //        return null;
        //    }
        //    if (0 == 0)//CanAdd() &&)
        //    {
        //        return new NormalPlace(cordinates);
        //    }
        //    return new NormalPlace(cordinates);
        //}
        //private bool IsPossible((int, int) cord)// cord are where to put the stone colored color
        //{
        //    Stone stone = new Stone(state.Player, cord);
        //    if (!this.state.Board.IsOccupied(cord) && IsCaptured(cord))
        //    {
        //        return false;// suicide, can add, not ocupide
        //    }
        //    return true;
        //}
    }
}
