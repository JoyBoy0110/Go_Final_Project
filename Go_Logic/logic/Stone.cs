using Go_Logic.data_structures;
using System.Security.Cryptography.X509Certificates;

namespace Go_Logic
{
    public class Stone
    {
        private Player color;
        private Coordinates cord;
        public Stone(Player color, Coordinates cord)
        {
            this.color = color;
            this.cord = cord;
        }
    }
}
