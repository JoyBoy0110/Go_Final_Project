using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go_Logic.data_structures
{
    public class Coordinates
    {
        
        private Tuple<int, int> coordinates;

        // x = row , y = column
        public Coordinates(int x, int y)
        {
            coordinates = new Tuple<int, int>(x, y);
        }
        public int x
        {
            get
            {
                return coordinates.Item1;
            }
        }
        public int y
        {
            get
            {
                return coordinates.Item2;
            }
        }
    }
}
