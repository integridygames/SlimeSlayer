using Game.Gameplay.Factories;
using Game.Gameplay.Models;
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
            Container.Bind<EssencePoolFactory>().AsSingle();
        }
    }
}