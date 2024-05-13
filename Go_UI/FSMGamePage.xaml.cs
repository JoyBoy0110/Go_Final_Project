using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Go_AI;
using Go_Logic;

namespace Go_UI;

public partial class FSMGamePage : Window
{
    private readonly Image[,] PieceImages; // the images of the pieces
    private readonly Image[,] HoverImages; // the images of the hover pieces
    private GameState gameState; // the game state
    private (int, int) hoverCordinates = (-1, -1); // the cordinates of the hover image
    private bool endOfGame; // a flag that indicates if the game ended
    private Player AiColor; // the color of the AI
    private GoStateMachine fsm; // the finite state machine of the game
    private bool Enable = true; // a flag that indicates if the player can play

    /// <summary>
    /// a constructor that initializes the game, the board, the AI and the images,if the ai is black, the AI plays first
    /// </summary>
    /// <param name="komi"></param>
    /// <param name="player"></param>
    public FSMGamePage(double komi, Player player, int size)
    {
        InitializeComponent();
        AiColor = player;
        gameState = new GameState(new Go_Board(size), komi);
        PieceImages = new Image[size, size];
        HoverImages = new Image[size, size];
        PlacingGrid.Rows = size;
        PlacingGrid.Columns = size;
        PiecesGrid.Rows = size;
        PiecesGrid.Columns = size;
        Board_Image.ImageSource = Images.GetBoardImage(size);
        fsm = new GoStateMachine();
        InitializeBoard();
        DrawCurrentBoard(gameState.Board);
        Set_Cursor(gameState.Player);
        endOfGame = false;
        fsm.Start();
        if (AiColor == Player.Black)
        {
            Enable = false;
            Handle_AI_Move();
        }
    }

    /// <summary>
    /// a method that initializes the board
    /// </summary>
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
            PieceImages[cordinates.Item1, cordinates.Item2].Source = Images.GetStoneImage(board.board_dict[cordinates]);
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
        int size = gameState.Board.Get_size();
        int row = (int)(mousePosition.Y / (Board.Height / size));
        int col = (int)(mousePosition.X / (Board.Width / size));
        if (row < 0 || row >= size || col < 0 || col >= size)
            return (-1, -1);
        return new(row, col);
    }

    /// <summary>
    /// a method that handles the mouse move event, and shows the hover image
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
                HoverImages[row, col].Source = Images.GetStoneImage(gameState.Player);
            }
        }
    }

    /// <summary>
    /// a method that handles the mouse down event, and adds a stone to the board if possible
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Board_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (Enable)
        {
            Point mousePosition = e.GetPosition(PiecesGrid);
            Handle_A_Move(mousePosition);
        }

        if (endOfGame)
        {
            EndGame(gameState.GetEndType());
        }
    }

    /// <summary>
    /// a methid that handles the move of the player, and adds a stone to the board if possible
    /// </summary>
    /// <param name="position"></param>
    private void Handle_A_Move(Point position)
    {
        bool flag = false;
        (int, int) placeCordinates = ToGridCordinates(position);
        if (gameState.Board.IsOccupied(placeCordinates))
        {
            return;
        }

        if (gameState.GetLastMove() == (placeCordinates, gameState.Player) ||
            gameState.IsKo(placeCordinates, gameState.Player))
        {
            return;
        }

        if (!gameState.CanAdd())
        {
            endOfGame = gameState.EndGame();
            if (endOfGame)
            {
                EndGame(gameState.GetEndType());
            }

            return;
        }

        if (placeCordinates != (-1, -1))
        {
            int row = hoverCordinates.Item1;
            int col = hoverCordinates.Item2;
            if (gameState.AddStone(placeCordinates))
            {
                flag = true;
                gameState.DecreaseStone();
            }
        }

        if (flag)
        {
            DrawCurrentBoard(gameState.Board);
            Switch_Turn();
        }
    }

    /// <summary>
    /// a method that handles the AI move, and generates a move for the AI, and adds a stone to the board if possible
    /// </summary>
    private void Handle_AI_Move()
    {
        bool flag = false;
        (int, int) placeCordinates = fsm.Update(gameState.Copy());
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
            Switch_Turn();
        }
    }

    /// <summary>
    /// a method that hides the hover image
    /// </summary>
    private void HideImage()
    {
        if (hoverCordinates != (-1, -1))
        {
            int row = hoverCordinates.Item1;
            int col = hoverCordinates.Item2;
            HoverImages[row, col].Source = null;
        }
    }

    /// <summary>
    /// a method that handles the mouse leave event, and hides the hover image
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Board_MouseLeave(object sender, MouseEventArgs e)
    {
        HideImage();
    }

    /// <summary>
    /// method that switches the turn of the game
    /// </summary>
    private void Switch_Turn()
    {
        gameState.Switch();
        Set_Cursor(gameState.Player);
        if (gameState.Player.Opponnent() == AiColor)
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

    /// <summary>
    /// a method that ends the game and opens the end screen
    /// </summary>
    /// <param name="end"></param>
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
        Player player = gameState.Player;
        endOfGame = gameState.Pass();
        if (endOfGame)
        {
            EndGame(gameState.GetEndType());
        }
        Set_Cursor(gameState.Player);
        if (gameState.Player.Opponnent() == AiColor)
        {
            Enable = true;
        }
        else
        {
            Enable = false;
            Handle_AI_Move();
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