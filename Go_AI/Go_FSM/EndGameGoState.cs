using Go_Logic;

namespace Go_AI;

public class EndGameGoState : BaseGoState<EGoState>
{
    private HeuristicAI ai;

    public EndGameGoState() : base(EGoState.EndGame)
    {
    }

    public override void Enter()
    {
        //initialize resources
        ai = new HeuristicAI();
    }

    public override void Exit()
    {
        //clean resources
        if (ai != null)
            ai.Clean();
    }

    protected override bool Assert(GameState gameState)
    {
        //check if the game is over
        return gameState.EndGame();
    }

    public override (int, int) GetMove(GameState gameState)
    {
        ai.Update(gameState);
        return ai.GetMoveNoRandom();
    }

    public override EGoState GetNextState(GameState gameState)
    {
        if (Assert(gameState))
            return EGoState.Opening;
        return EGoState.EndGame;
    }
}