using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Go_Logic;
using Go_Logic.go_rules;

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
        private (int, int) hoverCordinates = (-1, -1);


        public GamePage()
        {
            InitializeComponent();
            gameState = new GameState(Player.Black, new Go_Board(9));// need to add komi, a form which inside of him buttons which' let as select how much advantage to give the oponnent
            InitializeBoard();
            DrawCurrentBoard(gameState.Board);
            Set_Cursor(gameState.Player);
            test();
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
            foreach ((int, int) cordinates in board.board_dict.Keys)
            {
                PieceImages[cordinates.Item1, cordinates.Item2].Source = Images.GetImage(board.board_dict[cordinates]);
            }
        }

        private (int, int) ToGridCordinates(Point mousePosition)
        {
            int row = (int)(mousePosition.Y / (Board.Height / 9));
            int col = (int)(mousePosition.X / (Board.Width / 9));
            if (row < 0 || row >= 9 || col < 0 || col >= 9)
                return (-1, -1);
            return new(row, col);
        }

        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePosition = e.GetPosition(PiecesGrid);
            (int, int) temp = ToGridCordinates(mousePosition);
            if (temp != (-1, -1) && !temp.Equals(hoverCordinates))
            {
                HideImage();
                hoverCordinates = temp;
            }
            if (hoverCordinates != (-1, -1))
            {
                int row = hoverCordinates.Item1;
                int col = hoverCordinates.Item2;
                HoverImages[row, col].Source = Images.GetImage(gameState.Player);
            }
        }

        private void Board_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point mousePosition = e.GetPosition(PiecesGrid);
            Handle_A_Move(mousePosition);
            test();
        }


        private void Handle_A_Move(Point position)
        {
            (int, int) placeCordinates = ToGridCordinates(position);
            if (!gameState.CanAdd() || gameState.Board.IsOccupied(placeCordinates))
            {
                //MessageBox.Show("You can't add more stones");
                return;
            }
            if (placeCordinates != (-1, -1))
            {
                int row = hoverCordinates.Item1;
                int col = hoverCordinates.Item2;
                gameState.Board.Add_Stone(placeCordinates, gameState.Player);
                gameState.DecreaseStone();
            }
            DrawCurrentBoard(gameState.Board);
            Switch_Turn();
        }


        private void HideImage()
        {
            if (hoverCordinates != (-1, -1))
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
            Set_Cursor(gameState.Player);
        }

        private void Set_Cursor(Player player)
        {
            switch (player)
            {
                case Player.Black:
                    Game.Cursor = GoCursors.BlackCursor;
                    break;
                case Player.White:
                    Game.Cursor = GoCursors.WhiteCursor;
                    break;
                default:
                    break;
            }
        }

        private void Pass_Click(object sender, RoutedEventArgs e)
        {
            Switch_Turn();
        }

        private void test()
        {
            //if (gameState.Board.board_dict.ContainsKey((0, 0)))
            //{

            //    handler testHandler = new handler(gameState);
            //    text_block.Text = testHandler.IsCaptured((0, 0)).ToString();
            //}
            testLib();
        }
        private void testLib()
        {
            GameState test = new GameState(Player.Black, new Go_Board(9));
            test.Board.Add_Stone((0, 0), Player.Black);
            test.Board.Add_Stone((1, 0), Player.Black);
            test.Board.Add_Stone((1, 1), Player.Black);
            test.Board.Add_Stone((1, 2), Player.Black);
            test.Board.Add_Stone((0, 2), Player.Black);
            test.Board.Add_Stone((0, 3), Player.White);
            test.Board.Add_Stone((1, 3), Player.White);
            test.Board.Add_Stone((2, 2), Player.White);
            test.Board.Add_Stone((2, 1), Player.White);
            test.Board.Add_Stone((2, 0), Player.White);
            LibritiesHandler testHandler = new LibritiesHandler(test);
            text_block.Text = testHandler.IsCaptured((0, 0)).ToString();
        }
    }
}
