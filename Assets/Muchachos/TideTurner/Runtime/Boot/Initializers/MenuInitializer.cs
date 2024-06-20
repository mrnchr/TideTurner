using Muchachos.TideTurner.Runtime.Core.GameFsm;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Boot.Initializers
{
    public class MenuInitializer : IInitializable
    {
        private readonly GameStateMachine _gameMachine;

        public MenuInitializer(GameStateMachine gameMachine)
        {
            _gameMachine = gameMachine;
        }
        
        public void Initialize()
        {
            _gameMachine.ChangeState<MenuGameState>();
        }
    }
}