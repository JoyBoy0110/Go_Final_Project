namespace Go_Logic
{
    public enum ActionType
    {
        Add, Remove, Pass, Check, None
    }
    public class GameState
    {
        public Go_Board Board { get; private set; }// the game board
        public Player Player { get; private set; }// the color of the current player
        private int boardSize;// the size of the board
        private double komi;// the advantage that the white player gets at the end of the game
        Go_Board NullBoard = new Go_Board(9);// a null board

        public int blackStoneCounter { get; private set; }// a field that keeps track of how many stones left for black
        public int whiteStoneCounter { get; private set; }// a field that keeps track of how many stones left for white

        public double blackScore { get; private set; }// a field that keeps track of the score that the black player has achived at the end of the game 
        public double whiteScore { get; private set; }// a field that keeps track of the score that the white player has achived at the end of the game

        public List<Dictionary<(int, int), Player>> boardGroups { get; private set; }
        private LibritiesHandler libritiesHandler;
        private GroupHandler groupHandler;
        private CuptureHandler cuptureHandler;
        private PlacingHandler placingHandler;
        private EndGameHandler endGameHandler;

        public Go_Board[] versions;// the last two versions of the board
        private ((int, int), Player) lastMove;// the last move that was made

        /// <summary>
        /// constructor for the game state, initialize all the components
        /// </summary>
        /// <param name="player"></param>
        /// <param name="board"></param>
        public GameState(Go_Board board, double komi)
        {
            NullBoard.Add_Stone((-1, -1), Player.None);
            Player = Player.Black;
            Board = board;
            boardSize = board.Get_size();
            blackStoneCounter = (boardSize * boardSize) / 2 + (boardSize * boardSize) % 2;
            whiteStoneCounter = (boardSize * boardSize) / 2;
            this.komi = komi;
            blackScore = 0;
            whiteScore = this.komi;
            boardGroups = new List<Dictionary<(int, int), Player>>();
            versions = new Go_Board[2];
            versions[0] = NullBoard.Copy();
            versions[1] = NullBoard.Copy();
            lastMove = ((-1, -1), Player.None);
        }
        public GameState(Go_Board board, Player starting_player, double komi)
        {
            NullBoard.Add_Stone((-1, -1), Player.None);
            Player = starting_player;
            Board = board;
            boardSize = board.Get_size();
            blackStoneCounter = (boardSize * boardSize) / 2 + (boardSize * boardSize) % 2;
            whiteStoneCounter = (boardSize * boardSize) / 2;
            this.komi = komi;
            blackScore = 0;
            whiteScore = this.komi;
            boardGroups = new List<Dictionary<(int, int), Player>>();
            versions = new Go_Board[2];
            versions[0] = NullBoard.Copy();
            versions[1] = NullBoard.Copy();
            lastMove = ((-1, -1), Player.None);
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

        /// <summary>
        /// addes a point to the score to the player that cuptured the stone
        /// </summary>
        /// <param name="cuptured"></param>
        public void AddCupturedStone(Player cuptured)
        {
            switch (cuptured)
            {
                case Player.Black:
                    whiteScore++;
                    break;
                case Player.White:
                    blackScore++;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// checks if the game has ended, if the game has ended returns true, if not returns false
        /// </summary>
        /// <returns></returns>
        public bool EndGame()
        {
            return GetEndType() != EndType.None;
        }

        /// <summary>
        /// returns the type of the end of the game, if the game ended because of a pass, material or none
        /// </summary>
        /// <returns></returns>
        public EndType GetEndType()
        {
            if (blackStoneCounter == 0 || whiteStoneCounter == 0)// no more stones to add
            {
                return EndType.Material;
            }
            if (!versions[0].Equals(NullBoard) && !versions[1].Equals(NullBoard) && versions[0].Equals(versions[1]))// two consecutive passes
            {
                return EndType.Pass;
            }
            return EndType.None;
        }

        /// <summary>
        /// returns the winner of the game, if a tie the none, if one won the player that won,
        /// if resign the player that didnt resign
        /// </summary>
        /// <param name="EndGameState"></param>
        /// <returns></returns>
        public Player GetWinner(EndType EndGameState)
        {
            endGameHandler = new EndGameHandler(this);
            return endGameHandler.EndGame(EndGameState);
        }

        /// <summary>
        /// returns the scores of the game with a tuple of the black score and the white score
        /// </summary>
        /// <returns></returns>
        public (double, double) GetScores()
        {
            endGameHandler = new EndGameHandler(this);
            double bScore = endGameHandler.SimpleCalculateScore(Player.Black);
            double wScore = endGameHandler.SimpleCalculateScore(Player.White);
            return (bScore, wScore);
        }

        /// <summary>
        /// a method that passes the turn, if both players pass the game ends
        /// </summary>
        /// <returns></returns>
        public bool Pass()
        {
            Update(ActionType.Pass, (-1,-1));
            AddCupturedStone(Player);
            if (EndGame())
            {
                return true;
            }
            Switch();
            return false;
        }

        /// <summary>
        /// a method that switches the player
        /// </summary>
        public void Switch()
        {
            Player = Player.Opponnent();
        }

        /// <summary>
        /// checks if a stone can be added to the board by the counter of the reamaing stones
        /// </summary>
        /// <returns></returns>
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

        public void Update(ActionType action, (int, int) coords)
        {
            //psudo code
            // save past board
            //do action
            // --update--
            // update groups
            // get librities
            // check if any group is captured
            // send to cupture handler
            //get the new board
            // update main board with updated board
            SaveVersions();
            switch (action)
            {
                case ActionType.Add:
                    Board.Add_Stone(coords, Player);
                    break;
                case ActionType.Remove:
                    break;
                case ActionType.Pass:
                    break;
                case ActionType.None:
                    break;
                default:
                    break;
            }
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
            lastMove = (coords, Player);
        }

        /// <summary>
        /// add a stone to the board if possible (if the placing is equal to legal suicide or normal)
        /// if possible add to board and return true if not dont add and return false
        /// </summary>
        /// <param name="cord"></param>
        /// <returns></returns>
        public bool AddStone((int, int) coords)
        {
            SaveVersions();
            placingHandler = new PlacingHandler(this);
            PlaceType placeType = placingHandler.EvaluatePlace(coords, Player);
            if (placeType != PlaceType.Normal && placeType != PlaceType.legal_suicide)
            {
                return false;
            }
            Update(ActionType.Add, coords);
            return true;
        }

        public bool IsKo((int, int) cord, Player color)
        {
            Go_Board temp = Board.Copy();
            Go_Board[] versionsTemp = new Go_Board[2];
            versionsTemp[0] = versions[0].Copy();
            versionsTemp[1] = versions[1].Copy();
            bool flag = false;
            Board.Add_Stone(cord, color);
            Update(ActionType.Check, cord);
            if (versions[1].Equals(new Go_Board(9)))
            {
                versions = versionsTemp;
                Board = temp;
                return flag;
            }
            flag = versions[1].Equals(Board);
            versions = versionsTemp;
            Board = temp;
            return flag;
        }
        
        public ((int,int),Player) GetLastMove()
        {
            return lastMove;
        }

        public bool IsLegalSuicide((int, int) cord, Player color)
        {
            if (Board.IsOccupied(cord))
            {
                return false;
            }
            GameState temp = new GameState(Board.Copy(), color, this.komi);
            temp.groupHandler = new GroupHandler(temp);
            temp.libritiesHandler = new LibritiesHandler(temp);
            temp.Board.Add_Stone(cord, Player);
            int flag = temp.libritiesHandler.GetNumberOfLibertiesOfGroup(temp.groupHandler.GetGroup(cord, Player));
            temp.Update(ActionType.Check, (-1, -1));
            if (temp.libritiesHandler.GetNumberOfLibertiesOfGroup(temp.groupHandler.GetGroup(cord, Player)) != 0 && flag == 0)
            {
                return true;
            }
            return false;
        }

        public bool IsSuicide((int, int) cord, Player color)
        {
            if (Board.IsOccupied(cord))
            {
                return false;
            }
            GameState temp = new GameState(Board.Copy(), color, this.komi);
            temp.Board.Add_Stone(cord, Player);
            temp.groupHandler = new GroupHandler(temp);
            temp.libritiesHandler = new LibritiesHandler(temp);
            if (temp.libritiesHandler.GetNumberOfLibertiesOfGroup(temp.groupHandler.GetGroup(cord, Player)) == 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// saves past two versions of the board
        /// </summary>
        private void SaveVersions()
        {
            versions[1] = versions[0];
            versions[0] = Board.Copy();
        }

        /// <summary>
        /// returns a copy of the game state
        /// </summary>
        /// <returns></returns>
        public GameState Copy()
        {
            GameState temp = new GameState(Board.Copy(), Player, this.komi);
            temp.blackStoneCounter = blackStoneCounter;
            temp.whiteStoneCounter = whiteStoneCounter;
            temp.blackScore = blackScore;
            temp.whiteScore = whiteScore;
            temp.boardGroups = boardGroups;
            temp.versions = versions;
            return temp;
        }
    }
}