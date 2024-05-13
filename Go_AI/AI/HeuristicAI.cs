using Go_Logic;

namespace Go_AI
{
    public class HeuristicAI : IAI
    {
        private GameState gamestate; // the current state of the game

        /// <summary>
        /// a constructor for the HeuristicAI class
        /// </summary>
        /// <param name="gamestate"></param>
        public HeuristicAI(GameState gamestate)
        {
            this.gamestate = gamestate;
        }
        public HeuristicAI()
        {
            this.gamestate = new GameState(new Go_Board(-1), -1);// null gamestate
        }

        /// <summary>
        /// updates the state of the game for the AI
        /// </summary>
        /// <param name="state"></param>
        public void Update(GameState state)
        {
            this.gamestate = state.Copy();
        }

        public void Clean()
        {
            if (gamestate != null)
                gamestate.Clean();
        }

        /// <summary>
        /// returns the move that the AI decides to make based on the current gamestate, using a heuristic approach
        /// </summary>
        /// <returns> a valid coordinate to put a stone in or a pass</returns>
        public (int, int) GetMove()
        {
            //psudo code
            //
            // 1. If the group of the ai has only one liberty, protect the largest group
            //
            // 2. If opponent's group have only one liberty left and it has the best point value
            //
            // 3. If the group of the computer has two liberties
            // then choose the the one resulting in more liberties
            //
            // 4. If the gr oup of the oponnent has two liberties
            // then choose the the one resulting in less liberties
            //
            // 5. Match patterns to build strong shape, if found any
            // consider that instead of ch  asing the group
            //
            // 6. If none of the above apply, choose a random move
            //
            // 7. if there isn't a legal move, pass

            GameState temp = null;
            (int, int) bestMove = (-1, -1);

            // 1. If the group of the ai has only one liberty, protect the largest group
            bestMove = SaveGroup(gamestate.Copy(), 0);
            if (bestMove != (-1, -1))
                return bestMove;

            // 2. If opponent's group have only one liberty left and it has the best point value
            bestMove = KillGroup(gamestate.Copy(), 0);
            if (bestMove != (-1, -1))
                return bestMove;

            // 3. If the group of the computer has two liberties
            // then choose the one resulting in more liberties
            bestMove = GuardGroup(gamestate.Copy(), 0);
            if (bestMove != (-1, -1))
                return bestMove;

            // 4. If the group of the oponnent has two liberties
            // then choose the the one resulting in less liberties
            bestMove = SurroundGroup(gamestate.Copy(), 0);
            if (bestMove != (-1, -1))
                return bestMove;

            //5. Match patterns to build strong shape, if found any
            // consider that instead of ch  asing the group
            bestMove = PatternMatch(gamestate.Copy(), gamestate.Board.Get_size());
            if (bestMove != (-1, -1))
                return bestMove;

            // 6. If none of the above apply, choose a random move
            bestMove = RandomMove(gamestate.Copy(), gamestate.Board.Get_size());
            if (bestMove != (-1, -1))
                return bestMove;

            return bestMove;
        }

        /// <summary>
        /// returns the move that the AI decides to make based on the current gamestate, using a heuristic approach,
        /// without a randomizer
        /// </summary>
        /// <returns> a valid coordinate to put a stone in or a pass</returns>
        public (int, int) GetMoveNoRandom()
        {
            GameState temp = null;
            (int, int) bestMove = (-1, -1);

            // 1. If the group of the ai has only one liberty, protect the largest group
            bestMove = SaveGroup(gamestate.Copy(), 0);
            if (bestMove != (-1, -1))
                return bestMove;

            // 2. If opponent's group have only one liberty left and it has the best point value
            bestMove = KillGroup(gamestate.Copy(), 0);
            if (bestMove != (-1, -1))
                return bestMove;

            // 3. If the group of the computer has two liberties
            // then choose the one resulting in more liberties
            bestMove = GuardGroup(gamestate.Copy(), 0);
            if (bestMove != (-1, -1))
                return bestMove;

            // 4. If the group of the oponnent has two liberties
            // then choose the the one resulting in less liberties
            bestMove = SurroundGroup(gamestate.Copy(), 0);
            if (bestMove != (-1, -1))
                return bestMove;

            //5. Match patterns to build strong shape, if found any
            // consider that instead of ch  asing the group
            bestMove = PatternMatch(gamestate.Copy(), gamestate.Board.Get_size());
            if (bestMove != (-1, -1))
                return bestMove;

            return bestMove;
        }

        /// <summary>
        /// implementation of the first heuristic rule
        /// </summary>
        /// <param name="copy"></param>
        /// <param name="maxSize"></param>
        /// <returns></returns>
        private (int, int) SaveGroup(GameState copy, int maxSize)
        {
            (int, int) saveMove = (-1, -1);
            foreach (Dictionary<(int, int), Player> group in copy.boardGroups)
            {
                LibritiesHandler libritiesHandler = new LibritiesHandler(copy);
                PlacingHandler placingHandler = new PlacingHandler(copy);

                //if the color of the group is the player color and the group has only one liberty
                if (group[group.Keys.First()] == this.gamestate.Player &&
                    libritiesHandler.GetNumberOfLibertiesOfGroup(group) == 1 &&
                    this.gamestate.Board.NotAnEdge(libritiesHandler.GetLibritiesOfGroup(group).Keys.First()) &&
                    group.Count >= maxSize)
                {
                    PlaceType tempType = (placingHandler.EvaluatePlace(
                        libritiesHandler.GetLibritiesOfGroup(group).Keys.First(),
                        this.gamestate.Player));
                    if (tempType == PlaceType.Normal || tempType == PlaceType.legal_suicide)
                    {
                        saveMove = libritiesHandler.GetLibritiesOfGroup(group).Keys.First();
                        maxSize = group.Count;
                    }
                }
            }

            return saveMove;
        }

        /// <summary>
        /// implementation of the second heuristic rule
        /// </summary>
        /// <param name="copy"></param>
        /// <param name="maxSize"></param>
        /// <returns></returns>
        private (int, int) KillGroup(GameState copy, int maxSize)
        {
            (int, int) killMove = (-1, -1);
            foreach (Dictionary<(int, int), Player> group in copy.boardGroups)
            {
                PlacingHandler placingHandler = new PlacingHandler(copy);
                LibritiesHandler libritiesHandler = new LibritiesHandler(copy);

                //if the color of the group is oposite to the player and the group has only one liberty
                if (group[group.Keys.First()] == this.gamestate.Player.Opponnent() &&
                    libritiesHandler.GetNumberOfLibertiesOfGroup(group) == 1 && group.Count >= maxSize)
                {
                    PlaceType tempType = (placingHandler.EvaluatePlace(
                        libritiesHandler.GetLibritiesOfGroup(group).Keys.First(),
                        this.gamestate.Player));
                    if (tempType == PlaceType.Normal || tempType == PlaceType.legal_suicide)
                    {
                        killMove = libritiesHandler.GetLibritiesOfGroup(group).Keys.First();
                        maxSize = group.Count;
                    }
                }
            }

            return killMove;
        }

        /// <summary>
        /// implementation of the third heuristic rule
        /// </summary>
        /// <param name="copy"></param>
        /// <param name="maxSize"></param>
        /// <returns></returns>
        private (int, int) GuardGroup(GameState copy, int maxSize)
        {
            (int, int) bestMove = (-1, -1);
            foreach (Dictionary<(int, int), Player> group in copy.boardGroups)
            {
                int numOfLibrities = 0;
                LibritiesHandler libritiesHandler = new LibritiesHandler(copy);

                //if the color of the group is the player color and the group has two liberties
                if (group[group.Keys.First()] == this.gamestate.Player &&
                    libritiesHandler.GetNumberOfLibertiesOfGroup(group) == 2 && group.Count >= maxSize)
                {
                    Dictionary<(int, int), Player> librities = libritiesHandler.GetLibritiesOfGroup(group);
                    foreach ((int, int) coord in librities.Keys)
                    {
                        GameState temp = copy.Copy();
                        bool flag = temp.AddStone(coord);
                        if (flag)
                        {
                            libritiesHandler.Update(temp);
                            GroupHandler groupHandler = new GroupHandler(temp);

                            int tempNumOfLib =
                                libritiesHandler.GetNumberOfLibertiesOfGroup(groupHandler.GetGroup(coord, temp.Player));
                            if (tempNumOfLib > numOfLibrities)
                            {
                                numOfLibrities = tempNumOfLib;
                                bestMove = coord;
                                maxSize = group.Count;
                            }
                        }
                    }
                }
            }

            return bestMove;
        }

        /// <summary>
        /// implementation of the fourth heuristic rule
        /// </summary>
        /// <param name="copy"></param>
        /// <param name="maxSize"></param>
        /// <returns></returns>
        private (int, int) SurroundGroup(GameState copy, int maxSize)
        {
            (int, int) bestMove = (-1, -1);
            foreach (Dictionary<(int, int), Player> group in copy.boardGroups)
            {
                int numOfLibrities = 4;
                LibritiesHandler libritiesHandler = new LibritiesHandler(copy);

                //if the color of the group is the player color and the group has two liberties
                if (group[group.Keys.First()] == this.gamestate.Player.Opponnent() &&
                    libritiesHandler.GetNumberOfLibertiesOfGroup(group) == 2 && group.Count >= maxSize)
                {
                    Dictionary<(int, int), Player> librities = libritiesHandler.GetLibritiesOfGroup(group);
                    foreach ((int, int) coord in librities.Keys)
                    {
                        GameState temp = copy.Copy();
                        bool flag = temp.AddStone(coord);
                        if (flag)
                        {
                            libritiesHandler.Update(temp);
                            GroupHandler groupHandler = new GroupHandler(temp);
                            int tempNumOfLib =
                                libritiesHandler.GetNumberOfLibertiesOfGroup(
                                    groupHandler.GetGroup(coord, temp.Player.Opponnent()));
                            if (tempNumOfLib < numOfLibrities)
                            {
                                numOfLibrities = tempNumOfLib;
                                bestMove = coord;
                                maxSize = group.Count;
                            }
                        }
                    }
                }
            }

            return bestMove;
        }

        /// <summary>
        /// implementation of the fifth heuristic rule
        /// </summary>
        /// <param name="copy"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        private (int, int) PatternMatch(GameState copy, int size)
        {
            (int, int) bestMove = (-1, -1);
            Dictionary<(int, int), Player> board = copy.Board.board_dict;
            GameState temp = null;
            foreach ((int, int) coord in board.Keys)
            {
                if (board[coord] == copy.Player.Opponnent())
                {
                    int row = coord.Item1, col = coord.Item2;

                    //pattern no. 1
                    if (row > 0 && col > 0 && col < size - 1)
                    {
                        if (board.ContainsKey((row - 1, col - 1)) && board.ContainsKey((row - 1, col + 1)) &&
                            board[(row - 1, col - 1)] == copy.Player && board[(row - 1, col + 1)] == copy.Player)
                        {
                            temp = copy.Copy();
                            (int, int) checkCoord = (row - 1, col); // one above oponnent
                            if (temp.AddStone(checkCoord))
                            {
                                bestMove = checkCoord;
                            }
                        }
                    }

                    //pattern no. 2
                    if (row > 0 && col > 0 && col < size - 1)
                    {
                        if (board.ContainsKey((row - 1, col - 1)) && board.ContainsKey((row, col + 1)) &&
                            board[(row - 1, col - 1)] == copy.Player && board[(row, col + 1)] == copy.Player)
                        {
                            temp = copy.Copy();
                            (int, int) checkCoord = (row - 1, col); // one above oponnent
                            if (temp.AddStone(checkCoord))
                            {
                                bestMove = checkCoord;
                            }
                        }
                    }

                    //pattern no. 3
                    if (col > 0 && col < size - 1 && row < size - 1)
                    {
                        if (board.ContainsKey((row, col - 1)) && board.ContainsKey((row, col + 1)) &&
                            board[(row, col - 1)] == copy.Player && board[(row, col + 1)] == copy.Player)
                        {
                            temp = copy.Copy();
                            (int, int) checkCoord = (row + 1, col); // one below oponnent
                            if (temp.AddStone(checkCoord))
                            {
                                bestMove = checkCoord;
                            }
                        }
                    }

                    //pattern no. 4
                    if (row > 0 && col > 0 && col < size - 2)
                    {
                        if (board.ContainsKey((row - 1, col - 1)) && board.ContainsKey((row - 1, col + 2)) &&
                            board[(row - 1, col - 1)] == copy.Player && board[(row - 1, col + 2)] == copy.Player)
                        {
                            temp = copy.Copy();
                            (int, int) checkCoord = (row - 1, col); // one above oponnent
                            if (temp.AddStone(checkCoord))
                            {
                                bestMove = checkCoord;
                            }
                        }
                    }

                    //pattern no. 5
                    if (row > 0 && col > 1 && col < size - 2)
                    {
                        if (board.ContainsKey((row - 2, col - 1)) && board.ContainsKey((row - 1, col + 2)) &&
                            board[(row - 2, col - 1)] == copy.Player && board[(row - 1, col + 2)] == copy.Player)
                        {
                            temp = copy.Copy();
                            (int, int) checkCoord = (row - 1, col); // one above oponnent
                            if (temp.AddStone(checkCoord))
                            {
                                bestMove = checkCoord;
                            }
                        }
                    }

                    //pattern no. 6
                    if (col > 1 && row < size - 1)
                    {
                        if (board.ContainsKey((row, col - 1)) && board.ContainsKey((row + 1, col - 2)) &&
                            board[(row, col - 1)] == copy.Player && board[(row + 1, col - 2)] == copy.Player)
                        {
                            temp = copy.Copy();
                            (int, int) checkCoord = (row + 1, col); // one below oponnent
                            if (temp.AddStone(checkCoord))
                            {
                                bestMove = checkCoord;
                            }
                        }
                    }

                    //pattern no. 7
                    if (col > 1 && row > 0)
                    {
                        if (board.ContainsKey((row - 1, col)) && board.ContainsKey((row - 1, col - 2)) &&
                            board[(row - 1, col)] == copy.Player && board[(row - 1, col - 2)] == copy.Player)
                        {
                            temp = copy.Copy();
                            (int, int) checkCoord = (row, col - 1); // one to the left oponnent
                            if (temp.AddStone(checkCoord))
                            {
                                bestMove = checkCoord;
                            }
                        }
                    }
                }
            }

            return bestMove;
        }

        /// <summary>
        /// implementation of the random mover
        /// </summary>
        /// <param name="copy"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        private (int, int) RandomMove(GameState copy, int size)
        {
            Random random = new Random();
            int Rrow = random.Next(size);
            int Rcol = random.Next(size);
            while (!copy.AddStone((Rrow, Rcol)))
            {
                Rrow = random.Next(size);
                Rcol = random.Next(size);
            }

            return (Rrow, Rcol);
        }
    }
}