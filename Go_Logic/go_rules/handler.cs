
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go_Logic.go_rules
{
    public class handler
    {
        private (int,int)[] directions = {
            new (0, -1),
            new (-1, 0),
            new (0, 1),
            new (1, 0) };

        private GameState state;

        public handler(GameState state)
        {
            this.state = state;
        }


        public bool IsCaptured((int,int) cord)
        {
            Dictionary<(int,int), Player> group = GetGroup(cord, this.state.Board.board_dict[cord]);// creates the group of the stone
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
        public int GetLibertiesOfGroup(Dictionary<(int,int), Player> group)// 1 stone is also a group
        {
            int liberties = 0;
            foreach ((int,int) coord in group.Keys)
            {
                Dictionary<(int,int), Player> neighbors = Get_Neighbors(coord);
                liberties += 4 - neighbors.Count;
            }
            return liberties;
        }
        public Dictionary<(int,int), Player> Get_Neighbors((int,int) cord)
        {
            Dictionary<(int,int), Player> neighbor_dict = new Dictionary<(int,int), Player>();
            foreach ((int,int) direction in directions)
            {
                (int,int) neighbor = new (cord.Item1 + direction.Item1, cord.Item2 + direction.Item2);
                if(!this.state.Board.IsInside(neighbor))
                {
                    neighbor_dict.Add(neighbor, Player.None);
                }
                else if (this.state.Board.board_dict.ContainsKey(neighbor))
                {
                    neighbor_dict.Add(neighbor, this.state.Board.board_dict[neighbor]);
                }
            }
            return neighbor_dict;
        }

        private Dictionary<(int,int), Player> GetGroup((int,int) coord, Player color)
        {
            return GetGroup(coord, color, new Dictionary<(int,int), Player>());
        }
        private Dictionary<(int,int), Player> GetGroup((int,int) coord, Player color, Dictionary<(int,int), Player> group)
        {
            if (this.state.Board.IsInside(coord) && this.state.Board.IsOccupied(coord) && this.state.Board.board_dict[coord] == color && !group.ContainsKey(coord))
            {
                group.Add(coord, color);
                group = GetGroup(new (coord.Item1 + directions[0].Item1, coord.Item2 + directions[0].Item2), color, group);
                group = GetGroup(new (coord.Item1 + directions[1].Item1, coord.Item2 + directions[1].Item2), color, group);
                group = GetGroup(new (coord.Item1 + directions[2].Item1, coord.Item2 + directions[2].Item2), color, group);
                group = GetGroup(new (coord.Item1 + directions[3].Item1, coord.Item2 + directions[3].Item2), color, group);
            }
            return group;
        }




        public Place GetPlacingState((int,int) cordinates)
        {
            // if the position is occupied or outside the board
            if (this.state.Board.IsOccupied(cordinates) || !this.state.Board.IsInside(cordinates))
            {
                return null;
            }
            if (0 == 0)//CanAdd() &&)
            {
                return new NormalPlace(cordinates);
            }
            return new NormalPlace(cordinates);
        }
        private bool IsPossible((int,int) cord)// cord are where to put the stone colored color
        {
            Stone stone = new Stone(state.Player, cord);
            if (!this.state.Board.IsOccupied(cord) && IsCaptured(cord))
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
