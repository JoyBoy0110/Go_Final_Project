using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go_Logic
{
    public class CuptureHandler : RulesHandler
    {
        public CuptureHandler(GameState state) : base(state)
        {
        }

        public Go_Board Capture(Dictionary<(int, int), Player> group)
        {
            Go_Board copy = this.state.Board.Copy();
            foreach ((int, int) cordinates in group.Keys)
            {
                copy.board_dict.Remove(cordinates);
            }
            return copy;
        }
    }
}
