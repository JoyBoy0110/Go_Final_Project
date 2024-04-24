﻿using Go_Logic;

namespace Go_AI
{
    public class HeuristicAI : IAI
    {
        private GameState gamestate;
        public HeuristicAI(GameState gamestate)
        {
            this.gamestate = gamestate;
        }
        public void Update(GameState state)
        {
            this.gamestate = state;
        }


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
            // 4. If the group of the oponnent has two liberties
            // then choose the the one resulting in less liberties
            //
            // 5. Match patterns to build strong shape, if found any
            // consider that instead of ch  asing the group
            //

            GroupHandler groupHandler = new GroupHandler(gamestate);
            LibritiesHandler libritiesHandler = new LibritiesHandler(gamestate);
            PlacingHandler placingHandler = new PlacingHandler(gamestate);
            int size = gamestate.Board.Get_size();
            GameState copy = gamestate.Copy();
            GameState temp = null;
            (int, int) bestMove = (-1, -1);
            (int, int) defendGroup = (-1, -1); // for the first case
            (int, int) attackGroup = (-1, -1); // for the second case
            (int, int) freeGroup = (-1, -1); // for the third case
            (int, int) surroundGroup = (-1, -1); // for the fourth case
            int maxSize;
            int numOfLibrities;
            Dictionary<(int, int), Player> librities;

            // 1. If the group of the ai has only one liberty, protect the largest group
            maxSize = 0;
            foreach (Dictionary<(int, int), Player> group in copy.boardGroups)
            {
                //if the color of the group is the player color and the group has only one liberty
                if (group[group.Keys.First()] == this.gamestate.Player && libritiesHandler.GetNumberOfLibertiesOfGroup(group) == 1 &&
                    this.gamestate.Board.NotAnEdge(libritiesHandler.GetLibritiesOfGroup(group).Keys.First()) && group.Count >= maxSize)
                {
                    PlaceType tempType = (placingHandler.EvaluatePlace(libritiesHandler.GetLibritiesOfGroup(group).Keys.First(), this.gamestate.Player));
                    if (tempType == PlaceType.Normal || tempType == PlaceType.legal_suicide)
                    {
                        defendGroup = libritiesHandler.GetLibritiesOfGroup(group).Keys.First();
                        maxSize = group.Count;
                    }
                }
            }

            // 2. If opponent's group have only one liberty left and it has the best point value
            maxSize = 0;
            foreach (Dictionary<(int, int), Player> group in copy.boardGroups)
            {
                //if the color of the group is oposite to the player and the group has only one liberty
                if (group[group.Keys.First()] == this.gamestate.Player.Opponnent() && libritiesHandler.GetNumberOfLibertiesOfGroup(group) == 1 && group.Count >= maxSize)
                {
                    PlaceType tempType = (placingHandler.EvaluatePlace(libritiesHandler.GetLibritiesOfGroup(group).Keys.First(), this.gamestate.Player));
                    if (tempType == PlaceType.Normal || tempType == PlaceType.legal_suicide)
                    {
                        attackGroup = libritiesHandler.GetLibritiesOfGroup(group).Keys.First();
                        maxSize = group.Count;
                    }
                }
            }

            // 3. If the group of the computer has two liberties
            // then choose the the one resulting in more liberties
            maxSize = 0;
            foreach (Dictionary<(int, int), Player> group in copy.boardGroups)
            {
                numOfLibrities = 0;
                //if the color of the group is the player color and the group has two liberties
                if (group[group.Keys.First()] == this.gamestate.Player && libritiesHandler.GetNumberOfLibertiesOfGroup(group) == 2 && group.Count >= maxSize)
                {
                    librities = libritiesHandler.GetLibritiesOfGroup(group);
                    foreach ((int, int) coord in librities.Keys)
                    {
                        temp = copy.Copy();
                        bool flag = temp.AddStone(coord);
                        if (flag)
                        {
                            int tempNumOfLib = libritiesHandler.GetNumberOfLibertiesOfGroup(groupHandler.GetGroup(coord, temp.Player));
                            if (tempNumOfLib > numOfLibrities)
                            {
                                numOfLibrities = tempNumOfLib;
                                freeGroup = coord;
                                maxSize = group.Count;
                            }
                        }
                    }
                }
            }

            // 4. If the group of the oponnent has two liberties
            // then choose the the one resulting in less liberties
            maxSize = 0;
            foreach (Dictionary<(int, int), Player> group in copy.boardGroups)
            {
                numOfLibrities = 4;
                //if the color of the group is the player color and the group has two liberties
                if (group[group.Keys.First()] == this.gamestate.Player.Opponnent() && libritiesHandler.GetNumberOfLibertiesOfGroup(group) >= 2 && group.Count >= maxSize)
                {
                    librities = libritiesHandler.GetLibritiesOfGroup(group);
                    foreach ((int, int) coord in librities.Keys)
                    {
                        temp = copy.Copy();
                        bool flag = temp.AddStone(coord);
                        if (flag)
                        {
                            int tempNumOfLib = libritiesHandler.GetNumberOfLibertiesOfGroup(groupHandler.GetGroup(coord, temp.Player));
                            if (tempNumOfLib < numOfLibrities)
                            {
                                numOfLibrities = tempNumOfLib;
                                surroundGroup = coord;
                                maxSize = group.Count;
                            }
                        }
                    }
                }
            }

            //temporary
            //temp = copy.Copy();
            //System.Random random = new System.Random();
            //int x = random.Next(size);
            //int y = random.Next(size);
            //while (!temp.AddStone((x, y)))
            //{
            //    x = random.Next(size);
            //    y = random.Next(size);
            //}
            //bestMove = (x, y);
            //end of temporary

            if (defendGroup != (-1, -1))
            {
                bestMove = defendGroup;
            }
            else if (attackGroup != (-1, -1))
            {
                bestMove = attackGroup;
            }
            else if (freeGroup != (-1, -1))
            {
                bestMove = freeGroup;
            }
            else if (surroundGroup != (-1, -1))
            {
                bestMove = surroundGroup;
            }

            return bestMove;
        }
    }
}
