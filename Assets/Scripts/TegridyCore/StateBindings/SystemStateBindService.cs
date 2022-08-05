using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using TegridyCore.Base;
using TegridyCore.FiniteStateMachine;
using TegridyCore.RequiredUnityComponents;

namespace TegridyCore.StateBindings
{
    [UsedImplicitly]
    public class SystemStateBindService : IDisposable
    {
        private readonly SystemManager _systemManager;
        private readonly StateMachine _stateMachine;
        private readonly CoroutineStarter _coroutineStarter;

        private readonly Dictionary<StateBase, List<IPreInitializeSystem>> _boundPreInitSystems =
            new Dictionary<StateBase, List<IPreInitializeSystem>>();

        private readonly Dictionary<StateBase, List<IInitializeSystem>> _boundInitSystems =
            new Dictionary<StateBase, List<IInitializeSystem>>();

        private readonly Dictionary<StateBase, List<ICoroutineSystem>> _boundCoroutineSystems =
            new Dictionary<StateBase, List<ICoroutineSystem>>();

        private readonly Dictionary<StateBase, List<IUpdateSystem>> _boundUpdateSystems =
            new Dictionary<StateBase, List<IUpdateSystem>>();

        private readonly Dictionary<StateBase, List<IFixedUpdateSystem>> _boundFixedSystems =
            new Dictionary<StateBase, List<IFixedUpdateSystem>>();

        private readonly Dictionary<StateBase, List<IDestroySystem>> _boundDestroySystems =
            new Dictionary<StateBase, List<IDestroySystem>>();

        public SystemStateBindService(SystemManager systemManager, StateMachine stateMachine,
            CoroutineStarter coroutineStarter)
        {
            _systemManager = systemManager;
            _stateMachine = stateMachine;
            _coroutineStarter = coroutineStarter;

            _stateMachine.OnStateActivate += OnStateActivate;
            _stateMachine.OnStateDeactivate += OnStateDeactivate;
        }

        private void OnStateActivate(StateBase state, bool smoothTransition)
        {
            if (!smoothTransition)
            {
                if (_boundPreInitSystems.TryGetValue(state, out var preInitSystems))
                {
                    for (var index = 0; index < preInitSystems.Count; index++)
                    {
                        preInitSystems[index].PreInitialize();
                    }
                }

                if (_boundInitSystems.TryGetValue(state, out var initSystems))
                {
                    for (var index = 0; index < initSystems.Count; index++)
                    {
                        initSystems[index].Initialize();
                    }
                }

                if (_boundCoroutineSystems.TryGetValue(state, out var coroutineSystems))
                {
                    for (var index = 0; index < coroutineSystems.Count; index++)
                    {
                        _coroutineStarter.StartCoroutine(coroutineSystems[index].Start());
                    }
                }
            }

            if (_boundUpdateSystems.TryGetValue(state, out var updateSystems))
            {
                for (var index = 0; index < updateSystems.Count; index++)
                {
                    var updateSystem = updateSystems[index];
                    if (!_systemManager.ContainsUpdateSystem(updateSystem))
                        _systemManager.AddUpdateSystem(updateSystem);
                }
            }

            if (_boundFixedSystems.TryGetValue(state, out var fixedSystems))
            {
                for (var index = 0; index < fixedSystems.Count; index++)
                {
                    var fixedSystem = fixedSystems[index];
                    if (!_systemManager.ContainsFixedSystem(fixedSystem))
                        _systemManager.AddFixedSystem(fixedSystem);
                }
            }
        }

        private void OnStateDeactivate(StateBase state)
        {
            if (_boundDestroySystems.TryGetValue(state, out var destroySystems))
            {
                for (var index = 0; index < destroySystems.Count; index++)
                {
                    destroySystems[index].Destroy();
                }
            }

            if (_boundUpdateSystems.TryGetValue(state, out var updateSystems))
            {
                for (var index = 0; index < updateSystems.Count; index++)
                {
                    _systemManager.RemoveUpdateSystem(updateSystems[index]);
                }
            }

            if (_boundFixedSystems.TryGetValue(state, out var fixedSystems))
            {
                for (var index = 0; index < fixedSystems.Count; index++)
                {
                    _systemManager.RemoveFixedSystem(fixedSystems[index]);
                }
            }
        }

        public void BindPreInitSystem(IPreInitializeSystem system, StateBase state)
        {
            if (!_boundPreInitSystems.TryGetValue(state, out var preInitializeSystems))
            {
                _boundPreInitSystems[state] = new List<IPreInitializeSystem> {system};
                return;
            }

            preInitializeSystems.Add(system);
        }

        public void BindInitSystem(IInitializeSystem system, StateBase state)
        {
            if (!_boundInitSystems.TryGetValue(state, out var initSystems))
            {
                _boundInitSystems[state] = new List<IInitializeSystem> {system};
                return;
            }

            initSystems.Add(system);
        }

        public void BindCoroutineSystem(ICoroutineSystem system, StateBase state)
        {
            if (!_boundCoroutineSystems.TryGetValue(state, out var coroutineSystems))
            {
                _boundCoroutineSystems[state] = new List<ICoroutineSystem> {system};
                return;
            }

            coroutineSystems.Add(system);
        }

        public void BindUpdateSystem(IUpdateSystem system, StateBase state)
        {
            if (_boundUpdateSystems.TryGetValue(state, out var updateSystems))
            {
                updateSystems.Add(system);
            }
            else
            {
                _boundUpdateSystems[state] = new List<IUpdateSystem> {system};
            }

            if (!state.IsActive)
            {
                _systemManager.RemoveUpdateSystem(system);
            }
        }

        public void BindFixedSystem(IFixedUpdateSystem system, StateBase state)
        {
            if (_boundFixedSystems.TryGetValue(state, out var updateSystems))
            {
                updateSystems.Add(system);
            }
            else
            {
                _boundFixedSystems[state] = new List<IFixedUpdateSystem> {system};
            }

            if (!state.IsActive)
            {
                _systemManager.RemoveFixedSystem(system);
            }
        }

        public void BindDestroySystem(IDestroySystem system, StateBase state)
        {
            if (!_boundDestroySystems.TryGetValue(state, out var destroySystems))
            {
                _boundDestroySystems[state] = new List<IDestroySystem> {system};
                return;
            }

            destroySystems.Add(system);
        }

        public void Dispose()
        {
            _stateMachine.OnStateActivate -= OnStateActivate;
            _stateMachine.OnStateDeactivate -= OnStateDeactivate;
        }
    }
}