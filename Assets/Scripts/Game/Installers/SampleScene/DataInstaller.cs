using Game.Gameplay.Models.Bullets;
using Game.Gameplay.Models.Character;
using Game.Gameplay.Models.Level;
using Game.Gameplay.Models.Heap;
using Game.Gameplay.Models.Character.TargetSystem;
using Game.Gameplay.Models.Raycast;
using Game.Gameplay.Models.Weapon;
using Zenject;
using Game.Gameplay.Models.Essence;
using Game.Gameplay.Models.Enemy;
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
            Container.Bind<WeaponsInfo>().AsSingle();
            Container.Bind<ActiveBulletsContainer>().AsSingle();
            Container.Bind<CharacterHealthData>().AsSingle();
            Container.Bind<ActiveEssencesContainer>().AsSingle();
            Container.Bind<ActiveEnemiesContainer>().AsSingle();
            Container.Bind<CharacterEssencesData>().AsSingle();
            Container.Bind<ZonesInfo>().AsSingle();
            Container.Bind<ZoneTransitInfo>().AsSingle();
            Container.Bind<ZoneTransitInteractionInfo>().AsSingle();
        }
    }
}