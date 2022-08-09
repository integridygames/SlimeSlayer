using Game.Gameplay.Models.Level;
using Game.Gameplay.Models.Heap;
using Game.Gameplay.Models.Character.TargetSystem;
using Zenject;

namespace Game.Installers.SampleScene
{
    public class DataInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<LevelInfo>().AsSingle();
            Container.Bind<HeapInfo>().AsSingle();
            Container.Bind<CharacterHandsMovingStats>().AsSingle();
            Container.Bind<TargetsInfo>().AsSingle();
        }
    }
}