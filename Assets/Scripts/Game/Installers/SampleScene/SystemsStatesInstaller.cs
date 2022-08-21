using Game.Gameplay.States;
using Game.Gameplay.Systems;
using Game.Gameplay.Transitions;
using TegridyCore.FiniteStateMachine;
using TegridyCore.StateBindings;
using TegridyUtils.Extensions;
using Zenject;
using Game.Gameplay.Systems.Character;
using Game.Gameplay.Systems.Level;
using Game.Gameplay.Systems.Character.TargetSystem;
using Game.Gameplay.Systems.CameraContainer;
using Game.Gameplay.Systems.Character.Movement;

namespace Game.Installers.SampleScene
{
    public class SystemsStatesInstaller : MonoInstaller
    {
        private StartScreenState _startScreenState;
        private GameState _gameState;

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
        }

        private void CreateTransitions()
        {
            Container.CreateAndBindTransition<StartScreenToGameTransition>(_startScreenState, _gameState);
            Container.CreateAndBindTransition<GameToStartScreenTransition>(_gameState, _startScreenState);
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

            var levelInitialzieSystem = Container.Instantiate<LevelInitializeSystem>();
            Container.BindInitializeSystem(levelInitialzieSystem);
            var cameraContainerInitializeSystem = Container.Instantiate<CameraContainerInitializeSystem>();
            Container.BindInitializeSystem(cameraContainerInitializeSystem);

            var inverseKinematicsSystem = Container.Instantiate<InverseKinematicsSystem>();
            Container.BindInitializeSystem(inverseKinematicsSystem);
            Container.BindUpdateSystem(inverseKinematicsSystem);
            
            var cameraContainerUpdateSystem = Container.Instantiate<CameraContainerUpdateSystem>();
            Container.BindUpdateSystem(cameraContainerUpdateSystem);

            CreateTargetSystems();
        }

        private void CreateTargetSystems()
        {
            var targetsInitializeSystem = Container.Instantiate<TargetsInitializeSystem>();
            Container.BindInitializeSystem(targetsInitializeSystem);
            
            var handTargetsSetterSystem = Container.Instantiate<HandTargetsSetterSystem>();
            Container.BindInitializeSystem(handTargetsSetterSystem);
            
            var enemiesFinderSystem = Container.Instantiate<EnemiesFinderSystem>();
            Container.BindUpdateSystem(enemiesFinderSystem);
            
            var nearestHeapFinderSystem = Container.Instantiate<NearestHeapFinderSystem>();
            Container.BindUpdateSystem(nearestHeapFinderSystem);
            
            var characterToNearestHeapMoverSystem = Container.Instantiate<CharacterRotatorToNearestHeapSystem>();
            Container.BindFixedSystem(characterToNearestHeapMoverSystem);
            
            var handsTargetsMoverSystem = Container.Instantiate<HandsTargetsMoverSystem>();
            Container.BindFixedSystem(handsTargetsMoverSystem);

            var characterInputVelocitySystem = Container.Instantiate<CharacterInputVelocitySystem>();
            Container.BindUpdateSystemWithState(characterInputVelocitySystem, _gameState);

            var characterMovingSystem = Container.Instantiate<CharacterMovingSystem>();
            Container.BindFixedSystemWithState(characterMovingSystem, _gameState);

            var characterAnimationSystem = Container.Instantiate<CharacterAnimationSystem>();
            Container.BindUpdateSystemWithState(characterAnimationSystem, _gameState);
        }
    }
}