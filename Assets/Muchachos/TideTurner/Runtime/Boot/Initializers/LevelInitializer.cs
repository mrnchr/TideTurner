using Muchachos.TideTurner.Runtime.Core.GameFsm;
using Muchachos.TideTurner.Runtime.Level.LevelFsm;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Boot.Initializers
{
    public class LevelInitializer : IInitializable
    {
        private readonly GameStateMachine _gameMachine;
        private readonly LevelStateMachine _levelMachine;

        public LevelInitializer(GameStateMachine gameMachine, LevelStateMachine levelMachine)
        {
            _gameMachine = gameMachine;
            _levelMachine = levelMachine;
        }
        
        public void Initialize()
        {
            _gameMachine.ChangeState<LevelGameState>();
            _levelMachine.ChangeState<StartLevelState>();
        }
    }
}