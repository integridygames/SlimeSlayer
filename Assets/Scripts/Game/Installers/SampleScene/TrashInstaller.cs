using Game.Gameplay.TrashArchitecture;
using Zenject;

namespace Game.Installers.SampleScene
{
    public class TrashInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SpawnerCommandFactory>().AsSingle();
            Container.Bind<SpawnerRepository>().AsSingle();
        }
    }
}