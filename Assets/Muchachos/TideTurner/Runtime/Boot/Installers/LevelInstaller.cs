using Muchachos.TideTurner.Runtime.Boot.Initializers;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Boot
{
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindLevelInitializer();
        }

        private void BindLevelInitializer()
        {
            Container
                .BindInterfacesTo<LevelInitializer>()
                .AsSingle();
        }
    }
}