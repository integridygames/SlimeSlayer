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
            Container.Bind<CharacterStats>().AsSingle();
            Container.Bind<CurrentCharacterWeaponsData>().AsSingle();
            Container.Bind<ActiveProjectilesContainer>().AsSingle();
            Container.Bind<CharacterHealthData>().AsSingle();
            Container.Bind<ActiveEssencesContainer>().AsSingle();
            Container.Bind<ActiveEnemiesContainer>().AsSingle();
            Container.Bind<GameResourceData>().AsSingle();
            Container.Bind<SpawnZonesDataContainer>().AsSingle();
            Container.Bind<CameraStats>().AsSingle();
            Container.Bind<ActiveCoinsContainer>().AsSingle();
            Container.Bind<WeaponsCharacteristics>().AsSingle();
        }
    }
}