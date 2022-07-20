using TegridyCore;
using TegridyCore.StateBindings;
using Zenject;

namespace Game.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSystemManager();
            BindGameStartup();
        }
        
        private void BindSystemManager()
        {
            Container.Bind<SystemManager>()
                .AsSingle()
                .WhenInjectedInto(typeof(GameStartup), typeof(SystemStateBindService));
        }

        private void BindGameStartup()
        {
            Container.BindInterfacesAndSelfTo<GameStartup>()
                .AsSingle()
                .NonLazy();
        }
    }
}