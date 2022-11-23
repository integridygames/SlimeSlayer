using Game.Gameplay.States;
using Game.Gameplay.Systems;
using Game.Gameplay.Systems.Camera;
using Game.Gameplay.Transitions;
using TegridyCore.FiniteStateMachine;
using TegridyCore.StateBindings;
using TegridyUtils.Extensions;
using Zenject;
using Game.Gameplay.Systems.Character;
using Game.Gameplay.Systems.Level;
using Game.Gameplay.Systems.Character.TargetSystem;
using Game.Gameplay.Systems.Character.Health;
using Game.Gameplay.Systems.Character.Movement;
using Game.Gameplay.Systems.Weapon;
using Game.Gameplay.Systems.Enemy;
using Game.Gameplay.Systems.Zone;
using Game.Gameplay.Systems.Character.IK;
using Game.Gameplay.Systems.Essence;
using Game.Gameplay.Systems.Zone.ZoneTransit;
using Game.Gameplay.Systems.Zone.ZoneTransitMenu;

namespace Game.Installers.SampleScene
{
    public class SystemsStatesInstaller : MonoInstaller
    {
        private StartScreenState _startScreenState;
        private GameState _gameState;
        private PauseScreenState _pauseScreenState;
        private GunCabinetState _gunCabinetState;
        private EndScreenState _endScreenState;

        public override void InstallBindings()
        {
            CreateStates();
            CreateTransitions();
            CreateStateMachine();
            CreateSystems();
        }

        private void CreateStates()
        {
            _startScreenState = Container.CreateAndBindState<StartScreenState>();
            _gameState = Container.CreateAndBindState<GameState>();
            _pauseScreenState = Container.CreateAndBindState<PauseScreenState>();
            _gunCabinetState = Container.CreateAndBindState<GunCabinetState>();
            _endScreenState = Container.CreateAndBindState<EndScreenState>();
        }

        private void CreateTransitions()
        {
            Container.CreateAndBindTransition<StartScreenToGameTransition>(_startScreenState, _gameState);

            Container.CreateAndBindTransition<GameToPauseScreenTransition>(_gameState, _pauseScreenState);
            Container.CreateAndBindTransition<PauseScreenToGameTransition>(_pauseScreenState, _gameState);

            Container.CreateAndBindTransition<GameToGunCabinetTransition>(_gameState, _gunCabinetState);
            Container.CreateAndBindTransition<GunCabinetToGameTransition>(_gunCabinetState, _gameState);

            Container.CreateAndBindTransition<GameToEndScreenTransition>(_gameState, _endScreenState);
            Container.CreateAndBindTransition<EndScreenToStartScreenTransition>(_endScreenState, _startScreenState);
        }

        private void CreateStateMachine()
        {
            Container.Bind<StateMachine>()
                .AsSingle()
                .NonLazy();

            Container.Bind<SystemStateBindService>()
                .AsSingle();

            Container.Bind<SystemStateBinder>()
                .AsSingle()
                .NonLazy();
        }

        private void CreateSystems()
        {
            var gameInitializeSystem = Container.Instantiate<GameInitializeSystem>();
            Container.BindPreInitializeSystem(gameInitializeSystem);

            var levelInitializeSystem = Container.Instantiate<LevelInitializeSystem>();
            Container.BindPreInitializeSystemWithState(levelInitializeSystem, _startScreenState);

            var levelDestroySystem = Container.Instantiate<LevelDestroySystem>();
            Container.BindDestroySystemWithState(levelDestroySystem, _endScreenState);

            var weaponInitializeSystem = Container.Instantiate<WeaponInitializeSystem>();
            Container.BindInitializeSystem(weaponInitializeSystem);

            var weaponCharacteristicsInitializeSystem = Container.Instantiate<WeaponCharacteristicsInitializeSystem>();
            Container.BindInitializeSystem(weaponCharacteristicsInitializeSystem);

            var zonesInitializeSystem = Container.Instantiate<ZonesInitializeSystem>();
            Container.BindInitializeSystemWithState(zonesInitializeSystem, _startScreenState);

            var zoneTransitInitializeSystem = Container.Instantiate<ZoneTransitInitializeSystem>();
            Container.BindInitializeSystem(zoneTransitInitializeSystem);

            var inverseKinematicsSystem = Container.Instantiate<InverseKinematicsSystem>();
            Container.BindUpdateSystem(inverseKinematicsSystem);

            var cameraContainerUpdateSystem = Container.Instantiate<CameraMovementSystem>();
            Container.BindFixedSystem(cameraContainerUpdateSystem);

            var shootingSystem = Container.Instantiate<ShootingSystem>();
            Container.BindUpdateSystemWithState(shootingSystem, _gameState);

            var bulletsDestroyerSystem = Container.Instantiate<ProjectilesDestroyerSystem>();
            Container.BindUpdateSystemWithState(bulletsDestroyerSystem, _gameState);

            var currentZoneCatchSystem = Container.Instantiate<CurrentZoneCatchSystem>();
            Container.BindUpdateSystemWithState(currentZoneCatchSystem, _gameState);

            var zoneTransitFindingNearestSystem = Container.Instantiate<ZoneTransitFindingNearestSystem>();
            Container.BindUpdateSystemWithState(zoneTransitFindingNearestSystem, _gameState);

            var zoneTransitMenuPositioningSystem = Container.Instantiate<ZoneTransitMenuPositioningSystem>();
            Container.BindUpdateSystemWithState(zoneTransitMenuPositioningSystem, _gameState);

            var zoneTransitMenuChangingVisualSystem = Container.Instantiate<ZoneTransitMenuChangingVisualSystem>();
            Container.BindUpdateSystemWithState(zoneTransitMenuChangingVisualSystem, _gameState);

            var zoneTransitInteractionCheckingSystem = Container.Instantiate<ZoneTransitInteractionCheckingSystem>();
            Container.BindUpdateSystemWithState(zoneTransitInteractionCheckingSystem, _gameState);

            var characterSpawnSystem = Container.Instantiate<CharacterSpawnSystem>();
            Container.BindInitializeSystemWithState(characterSpawnSystem, _startScreenState);

            var characterInputVelocitySystem = Container.Instantiate<CharacterInputVelocitySystem>();
            Container.BindUpdateSystemWithState(characterInputVelocitySystem, _gameState);
            Container.BindUpdateSystemWithState(characterInputVelocitySystem, _endScreenState);
            Container.BindUpdateSystemWithState(characterInputVelocitySystem, _pauseScreenState);
            Container.BindUpdateSystemWithState(characterInputVelocitySystem, _gunCabinetState);

            var zoneTransitOpeningSystem = Container.Instantiate<ZoneTransitOpeningSystem>();
            Container.BindUpdateSystemWithState(zoneTransitOpeningSystem, _gameState);

            var characterMovingSystem = Container.Instantiate<CharacterMovingSystem>();
            Container.BindFixedSystemWithState(characterMovingSystem, _gameState);

            var zoneTransitCharacterEssenceTransferingSystem =
                Container.Instantiate<ZoneTransitCharacterEssenceTransferingSystem>();
            Container.BindUpdateSystemWithState(zoneTransitCharacterEssenceTransferingSystem, _gameState);

            var characterEndMoveSystem = Container.Instantiate<CharacterEndMoveSystem>();
            Container.BindDestroySystemWithState(characterEndMoveSystem, _gameState);

            var characterEssenceDestroySystem = Container.Instantiate<CharacterEssenceDestroySystem>();
            Container.BindDestroySystemWithState(characterEssenceDestroySystem, _endScreenState);

            var characterAnimationSystem = Container.Instantiate<CharacterAnimationSystem>();
            Container.BindUpdateSystem(characterAnimationSystem);

            var characterHealthViewMovingSystem = Container.Instantiate<CharacterHealthViewMovingSystem>();
            Container.BindUpdateSystem(characterHealthViewMovingSystem);

            var enemiesSpawnSystem = Container.Instantiate<EnemiesSpawnSystem>();
            Container.BindInitializeSystemWithState(enemiesSpawnSystem, _gameState);
            Container.BindUpdateSystemWithState(enemiesSpawnSystem, _gameState);

            CreateTargetSystems();
        }

        private void CreateTargetSystems()
        {
            var targetsInitializeSystem = Container.Instantiate<TargetsInitializeSystem>();
            Container.BindInitializeSystem(targetsInitializeSystem);

            var handTargetsSetterSystem = Container.Instantiate<HandTargetsSetterSystem>();
            Container.BindInitializeSystem(handTargetsSetterSystem);

            var enemiesFinderSystem = Container.Instantiate<EnemiesFinderSystem>();
            Container.BindUpdateSystemWithState(enemiesFinderSystem, _gameState);

            var nearestHeapFinderSystem = Container.Instantiate<NearestHeapFinderSystem>();
            Container.BindUpdateSystemWithState(nearestHeapFinderSystem, _gameState);

            var characterDirectionFinderSystem = Container.Instantiate<CharacterDirectionFinderSystem>();
            Container.BindUpdateSystemWithState(characterDirectionFinderSystem, _gameState);

            var characterRotationSystem = Container.Instantiate<CharacterRotationSystem>();
            Container.BindFixedSystemWithState(characterRotationSystem, _gameState);

            var handsTargetsMoverSystem = Container.Instantiate<HandsTargetsMoverSystem>();
            Container.BindFixedSystemWithState(handsTargetsMoverSystem, _gameState);
        }
    }
}