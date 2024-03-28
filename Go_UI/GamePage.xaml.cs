using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Go_Logic;

namespace Go_UI
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Window
    {
        private readonly Image[,] PieceImages = new Image[9, 9];
        private readonly Image[,] HoverImages = new Image[9, 9];
        private GameState gameState;
        private Tuple<int, int> hoverCordinates = null;


        public GamePage()
        {
            InitializeComponent();
            gameState = new GameState(Player.Black, new Go_Board(9));// need to add komi, a form which inside of him buttons which' let as select how much advantage to give the oponnent
            InitializeBoard();
            DrawCurrentBoard(gameState.Board);
        }

        private void InitializeBoard()
        {
            for (int row = 0; row < PieceImages.GetLength(0); row++)
                for (int col = 0; col < PieceImages.GetLength(1); col++)
                {
                    PieceImages[row, col] = new Image();
                    PieceImages[row, col].Width = 50;
                    PieceImages[row, col].Height = 50;
                    PiecesGrid.Children.Add(PieceImages[row, col]);

                    HoverImages[row, col] = new Image();
                    HoverImages[row, col].Opacity = 0.5;
                    HoverImages[row, col].Width = 50;
                    HoverImages[row, col].Height = 50;
                    PlacingGrid.Children.Add(HoverImages[row, col]);
                }
        }

        private void DrawCurrentBoard(Go_Board board)
        {
            foreach (Tuple<int, int> cordinates in board.board_dict.Keys)
            {
                PieceImages[cordinates.Item1, cordinates.Item2].Source = Images.GetImage(board.board_dict[cordinates]);
            }
        }

        private Tuple<int, int> ToGridCordinates(Point mousePosition)
        {
            int row = (int)(mousePosition.Y / (Board.Height / 9));
            int col = (int)(mousePosition.X / (Board.Width / 9));
            return new Tuple<int, int>(row, col);
        }

        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePosition = e.GetPosition(PiecesGrid);
            Tuple<int, int> temp = ToGridCordinates(mousePosition);
            if (temp != null && !temp.Equals(hoverCordinates))
            {
                HideImage();
                hoverCordinates = temp;
            }
            if (hoverCordinates != null)
            {
                int row = hoverCordinates.Item1;
                int col = hoverCordinates.Item2;
                HoverImages[row, col].Source = Images.GetImage(gameState.Player);
            }
        }

        private void Board_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point mousePosition = e.GetPosition(PiecesGrid);
            Tuple<int, int> placeCordinates = ToGridCordinates(mousePosition);
            if (!gameState.CanAdd())
            {
                //MessageBox.Show("You can't add more stones");
                return;
            }
            if (placeCordinates != null && !gameState.Board.IsOccupied(placeCordinates))
            {
                int row = hoverCordinates.Item1;
                int col = hoverCordinates.Item2;
                gameState.Board.Add_Stone(placeCordinates, gameState.Player);
                gameState.DecreaseStone();
            }
            DrawCurrentBoard(gameState.Board);
        }

        private void HideImage()
        {
            if (hoverCordinates != null)
            {
                int row = hoverCordinates.Item1;
                int col = hoverCordinates.Item2;
                HoverImages[row, col].Source = null;
            }
        }

        private Image CloneImage(Image orginal)
        {
            Image temp = new Image();
            temp.Source = orginal.Source;
            temp.Width = orginal.Width;
            temp.Height = orginal.Height;
            temp.Opacity = orginal.Opacity;
            return temp;
        }

        private void Board_MouseLeave(object sender, MouseEventArgs e)
        {
            HideImage();
        }

        private void Switch_Turn()
        {
            gameState.Switch();
        }

        private void Pass_Click(object sender, RoutedEventArgs e)
        {
            Switch_Turn();
        }
    }
}
