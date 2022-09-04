using Game.Gameplay.Factories;
using Game.Gameplay.Models;
using TegridyUtils.Extensions;
using Zenject;

namespace Game.Installers 
{
    public class FactoriesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<LevelFactory>().AsSingle();
            Container.Bind<ApplicationData>().FromFactory<ApplicationDataFactory>().AsSingle();
            Container.Bind<BulletsPoolFactory>().AsSingle();
        }
    }
}