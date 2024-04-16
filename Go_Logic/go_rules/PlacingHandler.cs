using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go_Logic
{
    public class PlacingHandler : RulesHandler
    {
        public PlacingHandler(GameState state) : base(state)
        { }


        public PlaceType EvaluatePlace((int, int) coord, Player color)
        {
            if (!this.state.Board.IsInside(coord))
            {
                return PlaceType.Restricted;
            }

            if (this.state.Board.IsOccupied(coord))
            {
                return PlaceType.Restricted;
            }

            if (this.state.IsKo(coord, color))
            {
                return PlaceType.Ko;
            }

            if (this.state.IsSuicide(coord, color) && !this.state.IsLegalSuicide(coord, color))
            {
                return PlaceType.suicide;
            }
            if(this.state.IsLegalSuicide(coord,color))
            {
                return PlaceType.legal_suicide;
            }
            return PlaceType.Normal;
        }


    }
}
