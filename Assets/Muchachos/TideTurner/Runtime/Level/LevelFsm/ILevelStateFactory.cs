namespace Muchachos.TideTurner.Runtime.Level.LevelFsm
{
    public interface ILevelStateFactory
    {
        TState Create<TState>() where TState : LevelStateBase;
    }
}