using Game.Gameplay.Models.Abilities;
using Game.Gameplay.Models.Camera;
using Game.Gameplay.Models.Character;
using Game.Gameplay.Models.Level;
using Game.Gameplay.Models.Heap;
using Game.Gameplay.Models.Raycast;
using Game.Gameplay.Models.Weapon;
using Zenject;
using Game.Gameplay.Models.Enemy;
using Game.Gameplay.Models.GameResources;
using Game.Gameplay.Models.Zone;

namespace Game.Installers.SampleScene
{
    public class DataInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<LevelInfo>().AsSingle();
            Container.Bind<HeapInfo>().AsSingle();
            Container.Bind<MouseRaycastInfo>().AsSingle();
            Container.Bind<CharacterMovementData>().AsSingle();
            Container.Bind<CharacterConstantStats>().AsSingle();
            Container.Bind<CharacterWeaponsRepository>().AsSingle();
            Container.Bind<ActiveProjectilesContainer>().AsSingle();
            Container.Bind<CharacterCharacteristicsRepository>().AsSingle();
            Container.Bind<ActiveEssencesContainer>().AsSingle();
            Container.Bind<ActiveEnemiesContainer>().AsSingle();
            Container.Bind<GameResourceData>().AsSingle();
            Container.Bind<SpawnZonesDataContainer>().AsSingle();
            Container.Bind<CameraStats>().AsSingle();
            Container.Bind<ActiveCoinsContainer>().AsSingle();
            Container.Bind<WeaponsCharacteristics>().AsSingle();
            Container.Bind<CharacterCharacteristics>().AsSingle();
            Container.Bind<WeaponsCharacteristicsRepository>().AsSingle();
            Container.Bind<AbilitiesRepository>().AsSingle();
            Container.Bind<AbilityTmpCharacteristics>().AsSingle();
        }
    }
}