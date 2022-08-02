using Game.Gameplay.Models.Level;
using Game.Gameplay.Models.Heap;
using Zenject;

namespace Game.Installers.SampleScene
{
    public class DataInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<LevelInfo>().AsSingle();
            Container.Bind<HeapInfo>().AsSingle();
            Container.Bind<RaycastInfo>().AsSingle();
        }
    }
}