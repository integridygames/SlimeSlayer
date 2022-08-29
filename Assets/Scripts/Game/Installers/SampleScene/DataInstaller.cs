using Game.Gameplay.Models.Character;
using Game.Gameplay.Models.Level;
using Game.Gameplay.Models.Heap;
using Game.Gameplay.Models.Character.TargetSystem;
using Game.Gameplay.Models.Raycast;
using Zenject;
using Game.Gameplay.Models.Zone;

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
            Container.Bind<MouseRaycastInfo>().AsSingle();
            Container.Bind<CharacterMovingData>().AsSingle();
            Container.Bind<CharacterStats>().AsSingle();
            Container.Bind<ZonesInfo>().AsSingle();
        }
    }
}