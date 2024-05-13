using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Go_Logic;

namespace Go_UI
{
    public static class Images
    {
        private static readonly Dictionary<Player, ImageSource> StoneImageSources = new()
        {
            {Player.White, LoadImage("Assets/white_stone.png")},
            {Player.Black, LoadImage("Assets/black_stone.png")}
        };// a dictionary that contains the images of the players

        private static readonly Dictionary<int, ImageSource> BoardImageSources = new()
        {
            {9, LoadImage("C:/Users/naorb/source/repos/goFinalProject/Go_UI/Assets/9Board.png")},
            {13, LoadImage("C:/Users/naorb/source/repos/goFinalProject/Go_UI/Assets/13Board.png")}
        };// a dictionary that contains the images of the players

        /// <summary>
        /// a function that loads a new image by a path 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static ImageSource LoadImage(string filePath)
        {
            return new BitmapImage(new Uri(filePath, UriKind.Relative));
        }

        /// <summary>
        /// a function that returns the image of a player by the player color
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static ImageSource GetStoneImage(Player color)
        {
            ImageSource value;
            if (StoneImageSources.TryGetValue(color, out value))
                return value;

            else
                return null;
        }

        /// <summary>
        /// a function that returns the image of a board by its size
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static ImageSource GetBoardImage(int size)
        {
            ImageSource value;
            if (BoardImageSources.TryGetValue(size, out value))
                return value;

            else
                return null;
        }
    }
}
