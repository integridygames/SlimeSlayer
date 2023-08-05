using Game.Gameplay.States;
using Game.Gameplay.Systems;
using Game.Gameplay.Systems.Abilities;
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
using Game.Gameplay.Systems.Character.IK;
using Game.Gameplay.Systems.Essence;
using Game.Gameplay.Systems.Zone;

namespace Game.Installers.SampleScene
{
    public class SystemsStatesInstaller : MonoInstaller
    {
        private StartScreenState _startScreenState;
        private GameState _gameState;
        private PauseScreenState _pauseScreenState;
        private GunCabinetState _gunCabinetState;
        private EndScreenState _endScreenState;
        private WeaponScreenState _weaponScreenState;
        private StatsScreenState _statsScreenState;
        private CraftScreenState _craftScreenState;

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
            _weaponScreenState = Container.CreateAndBindState<WeaponScreenState>();
            _statsScreenState = Container.CreateAndBindState<StatsScreenState>();
            _craftScreenState = Container.CreateAndBindState<CraftScreenState>();
        }

        private void CreateTransitions()
        {
            Container.CreateAndBindTransition<StartScreenToGameTransition>(_startScreenState, _gameState);

            Container.CreateAndBindTransition<GameToPauseScreenTransition>(_gameState, _pauseScreenState);
            Container.CreateAndBindTransition<PauseScreenToGameTransition>(_pauseScreenState, _gameState);

            Container.CreateAndBindTransition<StartScreenToWeaponTransition>(_startScreenState, _weaponScreenState);
            Container.CreateAndBindTransition<WeaponScreenToStartTransition>(_weaponScreenState, _startScreenState);

            Container.CreateAndBindTransition<StartScreenToStatsTransition>(_startScreenState, _statsScreenState);
            Container.CreateAndBindTransition<StatsScreenToStartTransition>(_statsScreenState, _startScreenState);

            Container.CreateAndBindTransition<StartScreenToCraftTransition>(_startScreenState, _craftScreenState);
            Container.CreateAndBindTransition<CraftScreenToStartTransition>(_craftScreenState, _startScreenState);
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

            var inverseKinematicsSystem = Container.Instantiate<InverseKinematicsSystem>();
            Container.BindUpdateSystem(inverseKinematicsSystem);

            var cameraContainerUpdateSystem = Container.Instantiate<CameraMovementSystem>();
            Container.BindFixedSystem(cameraContainerUpdateSystem);
            var cameraTargetFinderSystem = Container.Instantiate<CameraTargetFinderSystem>();
            Container.BindUpdateSystem(cameraTargetFinderSystem);

            var shootingSystem = Container.Instantiate<ShootingSystem>();
            Container.BindUpdateSystemWithState(shootingSystem, _gameState);

            var bulletsDestroyerSystem = Container.Instantiate<ProjectilesDestroyerSystem>();
            Container.BindUpdateSystemWithState(bulletsDestroyerSystem, _gameState);

            var characterSpawnSystem = Container.Instantiate<CharacterSpawnSystem>();
            Container.BindInitializeSystemWithState(characterSpawnSystem, _startScreenState);

            var characterInputVelocitySystem = Container.Instantiate<CharacterInputVelocitySystem>();
            Container.BindUpdateSystemWithState(characterInputVelocitySystem, _gameState);
            Container.BindUpdateSystemWithState(characterInputVelocitySystem, _endScreenState);
            Container.BindUpdateSystemWithState(characterInputVelocitySystem, _pauseScreenState);
            Container.BindUpdateSystemWithState(characterInputVelocitySystem, _gunCabinetState);

            var characterMovingSystem = Container.Instantiate<CharacterMovingSystem>();
            Container.BindFixedSystemWithState(characterMovingSystem, _gameState);

            var characterEndMoveSystem = Container.Instantiate<CharacterEndMoveSystem>();
            Container.BindDestroySystemWithState(characterEndMoveSystem, _gameState);

            var characterEssenceDestroySystem = Container.Instantiate<CharacterEssenceDestroySystem>();
            Container.BindDestroySystemWithState(characterEssenceDestroySystem, _endScreenState);

            var characterAnimationSystem = Container.Instantiate<CharacterAnimationSystem>();
            Container.BindUpdateSystem(characterAnimationSystem);

            var characterHealthViewMovingSystem = Container.Instantiate<CharacterHealthViewMovingSystem>();
            Container.BindUpdateSystem(characterHealthViewMovingSystem);

            var zonesInitializeSystem = Container.Instantiate<ZonesInitializeSystem>();
            Container.BindInitializeSystemWithState(zonesInitializeSystem, _gameState);

            var spawnTimeoutSystem = Container.Instantiate<SpawnTimeoutSystem>();
            Container.BindUpdateSystemWithState(spawnTimeoutSystem, _gameState);

            var enemiesSpawnSystem = Container.Instantiate<EnemiesSpawnSystem>();
            Container.BindUpdateSystemWithState(enemiesSpawnSystem, _gameState);

            var enemiesMovementSystem = Container.Instantiate<EnemiesMovementSystem>();
            Container.BindFixedSystemWithState(enemiesMovementSystem, _gameState);
            Container.BindUpdateSystemWithState(enemiesMovementSystem, _gameState);

            var enemiesAttackSystem = Container.Instantiate<EnemiesAttackSystem>();
            Container.BindUpdateSystemWithState(enemiesAttackSystem, _gameState);

            var closestEnemiesFinderSystem = Container.Instantiate<EnemiesInRangeFinderSystem>();
            Container.BindUpdateSystemWithState(closestEnemiesFinderSystem, _gameState);

            var closestEssenceMoveToCharacterSystem = Container.Instantiate<ClosestResourceMoveToCharacterSystem>();
            Container.BindUpdateSystemWithState(closestEssenceMoveToCharacterSystem, _gameState);

            var characterRegenerationSystem = Container.Instantiate<CharacterRegenerationSystem>();
            Container.BindUpdateSystemWithState(characterRegenerationSystem, _gameState);

            var abilitiesExecutionSystem = Container.Instantiate<AbilitiesExecutionSystem>();
            Container.BindUpdateSystemWithState(abilitiesExecutionSystem, _gameState);

            CreateTargetSystems();
        }

        private void CreateTargetSystems()
        {
            var handTargetsSetterSystem = Container.Instantiate<HandTargetsSetterSystem>();
            Container.BindInitializeSystem(handTargetsSetterSystem);

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