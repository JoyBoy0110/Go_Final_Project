using Go_Logic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Go_AI;

namespace Go_UI
{
    /// <summary>
    /// Interaction logic for AIGamePage.xaml
    /// </summary>
    public partial class AIGamePage : Window
    {

        private readonly Image[,] PieceImages = new Image[9, 9];
        private readonly Image[,] HoverImages = new Image[9, 9];
        private GameState gameState;
        private (int, int) hoverCordinates = (-1, -1);
        private bool endOfGame;
        private Player AiColor;
        private IAI Ai;
        private bool Enable = true;


        public AIGamePage(double komi, Player player)
        {
            InitializeComponent();
            AiColor = player;
            gameState = new GameState(new Go_Board(9), komi);// need to add komi, a form which inside of him buttons which' let as select how much advantage to give the oponnent
            Ai = new HeuristicAI(gameState);
            InitializeBoard();
            DrawCurrentBoard(gameState.Board);
            Set_Cursor(gameState.Player);
            endOfGame = false;
            if (AiColor == Player.Black)
            {
                Enable = false;
                Handle_AI_Move();
            }
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

        /// <summary>
        /// draws the current board by the board that we get from gameState
        /// </summary>
        /// <param name="board"></param>
        private void DrawCurrentBoard(Go_Board board)
        {
            foreach ((int, int) cordinates in board.board_dict.Keys)
            {
                PieceImages[cordinates.Item1, cordinates.Item2].Source = Images.GetImage(board.board_dict[cordinates]);
            }
            for (int row = 0; row < PieceImages.GetLength(0); row++)
                for (int col = 0; col < PieceImages.GetLength(1); col++)
                {
                    if (PieceImages[row, col].Source != null && !gameState.Board.board_dict.ContainsKey((row, col)))
                    {
                        PieceImages[row, col].Source = null;
                    }
                }
        }

        /// <summary>
        /// gets mouse position and returns the cordinates of the grid
        /// </summary>
        /// <param name="mousePosition"></param>
        /// <returns></returns>
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
            if (Enable)
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
        }

        private void Board_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Enable)
            {
                Point mousePosition = e.GetPosition(PiecesGrid);
                if (!Handle_A_Move(mousePosition))
                {
                    endOfGame = gameState.EndGame();
                }
            }
            if(endOfGame)
            {
                EndGame(gameState.GetEndType());
            }
        }

        private bool Handle_A_Move(Point position)
        {
            bool flag = false;
            (int, int) placeCordinates = ToGridCordinates(position);
            if (!gameState.CanAdd() || gameState.Board.IsOccupied(placeCordinates) || gameState.GetLastMove() == (placeCordinates, gameState.Player))
            {
                return flag;
            }
            if (placeCordinates != (-1, -1))
            {
                if (gameState.AddStone(placeCordinates))
                {
                    flag = true;
                    gameState.DecreaseStone();
                }
            }
            if (flag)
            {
                DrawCurrentBoard(gameState.Board);
                Switch_Turn(false); 
            }
            return flag;
        }

        private void Handle_AI_Move()
        {
            Ai.Update(gameState);
            bool flag = false;
            (int, int) placeCordinates = Ai.GetMove();
            if (placeCordinates == (-1, -1))
            {
                Pass();
                return;
            }
            if (gameState.AddStone(placeCordinates))
            {
                flag = true;
                gameState.DecreaseStone();
            }
            if (flag)
            {
                DrawCurrentBoard(gameState.Board);
                Switch_Turn(false);
            }
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

        private void Board_MouseLeave(object sender, MouseEventArgs e)
        {
            HideImage();
        }

        /// <summary>
        /// method that switches the turn of the game
        /// </summary>
        private void Switch_Turn(bool isAIPass)
        {
            if(!isAIPass)
            {
                gameState.Switch();
            }
            Set_Cursor(gameState.Player);
            if (gameState.Player != AiColor)
            {
                Enable = true;
            }
            else
            {
                Enable = false;
                Handle_AI_Move();
            }
        }

        /// <summary>
        /// a method that sets the cursor image1
        /// </summary>
        /// <param name="player"></param>
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

        private void EndGame(EndType end)
        {
            EndScreen gp = new EndScreen(end, this.gameState.GetWinner(end), this.gameState.GetScores());
            this.Close();
            gp.ShowDialog();
        }

        /// <summary>
        /// a button that passes the turn, if both players pass the game ends
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pass()
        {
            endOfGame = gameState.Pass();
            if(gameState.Player != AiColor)
                Switch_Turn(true);
            if (endOfGame)
            {
                EndGame(gameState.GetEndType());
            }
        }
        private void Pass_Click(object sender, RoutedEventArgs e)
        {
            Pass();
        }

        /// <summary>
        /// a button that resigns the game, the player who pressed it loses
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Resign_Click(object sender, RoutedEventArgs e)
        {
            endOfGame = true;
            if (endOfGame)
            {
                EndGame(EndType.Resign);
            }
        }
    }
}
