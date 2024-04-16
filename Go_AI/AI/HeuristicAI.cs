using Go_Logic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go_AI
{
    public class HeuristicAI
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


        //public int CalculateScore(GameState state)
        //{
        //    int score = 0;
        //    score += CalculateTerritory(state.Player);
        //    score += CalculateLiberties(state.Player);
        //    return score;
        //}
        //public int CalculateTerritory(Player color)
        //{
        //    int territory = 0;
        //    List<Dictionary<(int, int), Player>> groups = new GroupHandler(this.gamestate).GetGroups();
        //    foreach (Dictionary<(int, int), Player> group in groups)
        //    {
        //        if (group.Values.First() == color)
        //        {
        //            territory += group.Count;
        //        }
        //    }
        //    return territory;
        //}



        //peusodo code:

        // 1. If the group of the side to move has only one liberty
        // then save it by putting a stone there unless it's a board edge
        //
        // 2. If opponent's group have only one liberty left
        //   then capture it
        //
        // 3. If the group of the computer has two liberties
        // then choose the the one resulting in more liberties
        //
        // 4. If opponent's group have more than one liberty
        // then try to surround it
        //
        // 5. Match patterns to build strong shape, if found any
        // consider that instead of chasing the group

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
            // consider that instead of chasing the group
            //

            GroupHandler groupHandler = new GroupHandler(gamestate);
            LibritiesHandler libritiesHandler = new LibritiesHandler(gamestate);
            int size = gamestate.Board.Get_size();
            GameState copy = gamestate.Copy();
            (int, int) bestMove = (-1, -1);
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
                    bestMove = libritiesHandler.GetLibritiesOfGroup(group).Keys.First();
                    maxSize = group.Count;
                }
            }

            if (bestMove != (-1, -1))
            {
                return bestMove;
            }

            // 2. If opponent's group have only one liberty left and it has the best point value
            maxSize = 0;
            foreach (Dictionary<(int, int), Player> group in copy.boardGroups)
            {
                //if the color of the group is oposite to the player and the group has only one liberty
                if (group[group.Keys.First()] == this.gamestate.Player.Opponnent() && libritiesHandler.GetNumberOfLibertiesOfGroup(group) == 1 && group.Count >= maxSize)
                {
                    bestMove = libritiesHandler.GetLibritiesOfGroup(group).Keys.First();
                    maxSize = group.Count;
                }
            }

            if (bestMove != (-1, -1))
            {
                return bestMove;
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
                        GameState temp = copy.Copy();
                        bool flag = temp.AddStone(coord);
                        if (flag)
                        {
                            int tempNumOfLib = libritiesHandler.GetNumberOfLibertiesOfGroup(groupHandler.GetGroup(coord, temp.Player));
                            if (tempNumOfLib > numOfLibrities)
                            {
                                numOfLibrities = tempNumOfLib;
                                bestMove = coord;
                            }
                        }
                    }
                    maxSize = group.Count;
                }
            }

            if (bestMove != (-1, -1))
            {
                return bestMove;
            }

            // 4. If the group of the oponnent has two liberties
            // then choose the the one resulting in less liberties
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
                        GameState temp = copy.Copy();
                        bool flag = temp.AddStone(coord);
                        if (flag)
                        {
                            int tempNumOfLib = libritiesHandler.GetNumberOfLibertiesOfGroup(groupHandler.GetGroup(coord, temp.Player));
                            if (tempNumOfLib > numOfLibrities)
                            {
                                numOfLibrities = tempNumOfLib;
                                bestMove = coord;
                            }
                        }
                    }
                    maxSize = group.Count;
                }
            }




            return bestMove;
        }

        public int CalculateLiberties((int, int) coord)
        {
            int liberties = 0;
            this.gamestate.AddStone(coord);
            return liberties;
        }

    }
}
