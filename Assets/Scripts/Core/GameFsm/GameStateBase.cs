public abstract class GameStateBase : IState
{
    protected GameStateMachine _machine;

    protected GameStateBase(GameStateMachine machine)
    {
        _machine = machine;
    }

    public abstract void Enter();
    public abstract void Exit();
}