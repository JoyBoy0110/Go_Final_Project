using Go_Logic.data_structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go_Logic.go_rules
{
    public class handler
    {
        private Coordinates[] directions = {
            new Coordinates(0, -1),
            new Coordinates(-1, 0),
            new Coordinates(0, 1),
            new Coordinates(1, 0) };

        private GameState state;
        private Go_Board board;

        public handler(GameState state)
        {
            this.state = state;
            this.board = state.Board;
        }


        public bool IsCaptured(Coordinates cord)
        {
            Dictionary<Coordinates, Player> group = GetGroup(cord, board.board_dict[cord]);// creates the group of the stone
            int liberties = GetLibertiesOfGroup(group);
            if (liberties < 0 || liberties > 4 * group.Count)
            {
                throw new Exception("Invalid number of liberties: need to check code");

            }
            if (liberties == 0)// && CheckGroup())
            {
                return true;
            }
            return false;
        }
        public int GetLibertiesOfGroup(Dictionary<Coordinates, Player> group)// 1 stone is also a group
        {
            int liberties = 0;
            foreach (Coordinates coord in group.Keys)
            {
                Dictionary<Coordinates, Player> neighbors = Get_Neighbors(coord);
                liberties += 4 - neighbors.Count;
            }
            return liberties;
        }
        public Dictionary<Coordinates, Player> Get_Neighbors(Coordinates cord)
        {
            Dictionary<Coordinates, Player> neighbor_dict = new Dictionary<Coordinates, Player>();
            foreach (Coordinates direction in directions)
            {
                Coordinates neighbor = new Coordinates(cord.x + direction.x, cord.y + direction.y);
                if (board.IsInside(neighbor))
                {
                    neighbor_dict.Add(neighbor, board.board_dict[neighbor]);
                }
            }
            return neighbor_dict;
        }

        private Dictionary<Coordinates, Player> GetGroup(Coordinates coord, Player color)
        {
            return GetGroup(coord, color, new Dictionary<Coordinates, Player>());
        }
        private Dictionary<Coordinates, Player> GetGroup(Coordinates coord, Player color, Dictionary<Coordinates, Player> group)
        {
            if (board.IsInside(coord) && board.IsOccupied(coord) && board.board_dict[coord] == color && group.ContainsKey(coord))
            {
                group.Add(coord, color);
                group = GetGroup(new Coordinates(coord.x + directions[0].x, coord.y + directions[0].y), color, group);
                group = GetGroup(new Coordinates(coord.x + directions[1].x, coord.y + directions[1].y), color, group);
                group = GetGroup(new Coordinates(coord.x + directions[2].x, coord.y + directions[2].y), color, group);
                group = GetGroup(new Coordinates(coord.x + directions[3].x, coord.y + directions[3].y), color, group);
            }
            return group;
        }




        public Place GetPlacingState(Coordinates cordinates)
        {
            // if the position is occupied or outside the board
            if (board.IsOccupied(cordinates) || !board.IsInside(cordinates))
            {
                return null;
            }
            if (0 == 0)//CanAdd() &&)
            {
                return new NormalPlace(cordinates);
            }
            return new NormalPlace(cordinates);
        }
        /// <summary>
        /// checks if a move is possible' returns false if not
        /// </summary>
        /// <param name="cord"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        private bool IsPossible(Coordinates cord)// cord are where to put the stone colored color
        {
            Stone stone = new Stone(state.Player, cord);
            if (!board.IsOccupied(cord) && IsCaptured(cord))
            {
                return false;// suicide, can add, not ocupide
            }
            return true;
        }






        
        //        IF x is not off the edge THEN

        //          IF there is a stone at x AND it is the given color AND it is not marked THEN

        //              mark it

        //CALL COUNT(NORTH(x), color) CALL COUNT(EAST(x), color) CALL COUNT(SOUTH(x), color) CALL COUNT(WEST(x), color) ELSE IF there is no stone at x THEN

        //mark the point as a liberty increment the liberty count END END
    }
}
