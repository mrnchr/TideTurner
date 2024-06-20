using Muchachos.TideTurner.Runtime.Core.GameFsm;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Boot.Initializers
{
    public class LevelInitializer : IInitializable
    {
        private readonly GameStateMachine _gameMachine;

        public LevelInitializer(GameStateMachine gameMachine)
        {
            _gameMachine = gameMachine;
        }
        
        public void Initialize()
        {
            _gameMachine.ChangeState<LevelGameState>();
        }
    }
}