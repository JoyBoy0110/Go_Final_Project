using Go_Logic;

namespace Go_AI;

public class OpeningGoState : BaseGoState<EGoState>
{
    // random mover, ocupied corners | exits when all corners are ocupied
    private List<(int, int)> directions;

    public OpeningGoState() : base(EGoState.Opening)
    {
    }

    public override void Enter()
    {
        //initialize resources
        directions = new List<(int, int)>();
        directions.Add((1, 0));
        directions.Add((-1, 0));
        directions.Add((0, 1));
        directions.Add((0, -1));
    }

    public override void Exit()
    {
        //clean resources
        directions.Clear();
    }

    protected override bool Assert(GameState gameState)
    {
        GroupHandler groupHandler = new GroupHandler(gameState);
        int stoneSum = 0;
        foreach ((int, int) corner in gameState.Board.GetCorners())
        {
            if (gameState.Board.IsOccupied(corner))
                stoneSum += groupHandler.GetGroup(corner, gameState.Board.board_dict[corner]).Count;
        }

        return stoneSum >= 8;
    }

    public override (int, int) GetMove(GameState gameState)
    {
        (int, int) bestMove = (-1, -1);
        Go_Board board = gameState.Board;
        (int, int)[] corners = board.GetCorners();

        foreach ((int, int) corner in corners)
        {
            if (!board.IsOccupied(corner) && (!gameState.IsSuicide(corner, gameState.Player) ||
                                              gameState.IsSuicide(corner, gameState.Player) &&
                                              gameState.IsLegalSuicide(corner, gameState.Player)))
                bestMove = corner;
        }

        if (bestMove != (-1, -1))
            return bestMove;

        int sizeOfGroup = 5;
        GroupHandler groupHandler = new GroupHandler(gameState);
        foreach ((int, int) corner in corners)
        {
            if (board.IsOccupied(corner) && groupHandler.GetGroup(corner,
                    board.board_dict[corner]).Count <= sizeOfGroup)
                foreach ((int, int) direction in directions)
                {
                    (int, int) cornerNighbor = (corner.Item1 + direction.Item1, corner.Item2 + direction.Item2);
                    if (!board.IsOccupied(cornerNighbor))
                        bestMove = cornerNighbor;
                }
        }

        return bestMove;
    }

    public override EGoState GetNextState(GameState gameState)
    {
        if (Assert(gameState))
            return EGoState.MidGame;
        return EGoState.Opening;
    }
}