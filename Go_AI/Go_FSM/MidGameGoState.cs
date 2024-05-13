using Go_Logic;

namespace Go_AI;

public class MidGameGoState : BaseGoState<EGoState>
{
    private IAI ai;

    public MidGameGoState() : base(EGoState.MidGame)
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
        int totalNumOfStones = gameState.Board.Get_size() * gameState.Board.Get_size(),
            numOfStonesUsed = totalNumOfStones - (gameState.blackStoneCounter + gameState.whiteStoneCounter);
        return numOfStonesUsed > (totalNumOfStones / 3) * 2;
    }

    public override (int, int) GetMove(GameState gameState)
    {
        ai.Update(gameState);
        return ai.GetMove();
    }

    public override EGoState GetNextState(GameState gameState)
    {
        if (Assert(gameState))
            return EGoState.EndGame;
        return EGoState.MidGame;
    }
}