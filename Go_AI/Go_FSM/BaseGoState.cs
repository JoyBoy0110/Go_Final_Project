using Go_Logic;

namespace Go_AI;

public abstract class BaseGoState<EState> where EState : Enum
{
    public EState StateKey { get; private set; }

    public BaseGoState(EState stateKey)
    {
        StateKey = stateKey;
    }

    public abstract void Enter();
    public abstract void Exit();

    /// <summary>
    /// asserts the state of the game
    /// </summary>
    /// <param name="gameState"></param>
    /// <returns></returns>
    protected abstract bool Assert(GameState gameState);

    /// <summary>
    /// returns the move to be made by the AI
    /// </summary>
    /// <returns></returns>
    public abstract (int, int) GetMove(GameState gameState);

    /// <summary>
    /// returns the next state of the game that  the AI should transition to
    /// </summary>
    /// <returns></returns>
    public abstract EState GetNextState(GameState gameState);
}