using Game.Gameplay.Factories;
using Game.Gameplay.Models;
using Game.Gameplay.Models.SampleScene;
using Zenject;

namespace Game.Installers.SampleScene
{
    public class DataInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ApplicationData>().FromFactory<ApplicationDataFactory>().AsSingle();
            
            Container.Bind<DayTime>().AsSingle();
        }
    }
}