namespace Go_Logic
{
    public class Stone
    {
        private Player color;
        private (int,int) cord;
        public Stone(Player color, (int,int) cord)
        {
            this.color = color;
            this.cord = cord;
        }
    }
}
