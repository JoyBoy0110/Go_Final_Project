using System.Windows.Media;
using System.Windows.Media.Imaging;
using Go_Logic;

namespace Go_UI
{
    public static class Images
    {
        private static readonly Dictionary<Player, ImageSource> ImageSources = new()
        {
            {Player.White, LoadImage("Assets/white_stone.png")},
            {Player.Black, LoadImage("Assets/black_stone.png")}
        };
        private static ImageSource LoadImage(string filePath)
        {
            return new BitmapImage(new Uri(filePath, UriKind.Relative));
        }
        public static ImageSource GetImage(Player color)
        {
            switch (color)
            {
                case Player.White:
                    return ImageSources[color];
                case Player.Black:
                    return ImageSources[color];
                default:
                    return null;
            }
        }
    }
}
