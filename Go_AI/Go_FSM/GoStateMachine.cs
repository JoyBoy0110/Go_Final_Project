using Go_Logic;

namespace Go_AI;

public enum EGoState
{
    Opening, // random mover, ocupied corners | exits when all corners are ocupied
    MidGame, // heuristic mover | exits when more than 2 thirds of your stones has been used
    EndGame // heuristic mover no randomizer | exits when no moves are left = game over
}

public class GoStateMachine
{
    protected Dictionary<EGoState, BaseGoState<EGoState>> states = new Dictionary<EGoState, BaseGoState<EGoState>>();
    public BaseGoState<EGoState> CurrentGoState { get; protected set; }
    private bool isIransitioning = false;
    private EGoState startingState = EGoState.Opening;

    public GoStateMachine()
    {
        states.Add(EGoState.Opening, new OpeningGoState());
        states.Add(EGoState.MidGame, new MidGameGoState());
        states.Add(EGoState.EndGame, new EndGameGoState());
        CurrentGoState = states[startingState];
    }

    public (int, int) Update(GameState gameState)
    {
        EGoState nextStateKey = CurrentGoState.GetNextState(gameState);
        if (!isIransitioning && nextStateKey.Equals(CurrentGoState.StateKey))
        {
            return CurrentGoState.GetMove(gameState);
        }
        else
        {
            if (TransitionToState(nextStateKey))
                return Update(gameState);
        }
        return (-1, -1);
    }

    private bool TransitionToState(EGoState nextStateKey)
    {
        isIransitioning = true;
        if (nextStateKey == startingState)
        {
            isIransitioning = false;
            return false;
        }

        CurrentGoState.Exit();
        CurrentGoState = states[nextStateKey];
        CurrentGoState.Enter();
        isIransitioning = false;
        return true;
    }

    /// <summary>
    /// initializes resources
    /// </summary>
    public void Start()
    {
        CurrentGoState.Enter();
    }

    /// <summary>
    /// cleans resources
    /// </summary>
    public void Stop()
    {
        CurrentGoState.Exit();
        states.Clear();
    }

    public string GetStateName()
    {
        return CurrentGoState.StateKey.ToString();
    }
}