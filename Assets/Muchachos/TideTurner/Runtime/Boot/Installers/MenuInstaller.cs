using Muchachos.TideTurner.Runtime.Boot.Initializers;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Boot
{
    public class MenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindMenuInitializer();
        }

        private void BindMenuInitializer()
        {
            Container
                .BindInterfacesTo<MenuInitializer>()
                .AsSingle();
        }
    }
}