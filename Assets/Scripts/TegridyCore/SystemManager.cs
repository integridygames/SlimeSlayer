using System.Collections.Generic;
using JetBrains.Annotations;
using TegridyCore.Base;
using TegridyCore.RequiredUnityComponents;

namespace TegridyCore
{
    [UsedImplicitly]
    public class SystemManager
    {
        private readonly List<IPreInitializeSystem> _preInitializeSystems;
        private readonly List<IInitializeSystem> _initializeSystems;
        private readonly List<ICoroutineSystem> _coroutineSystems;
        private readonly List<IUpdateSystem> _updateSystems;
        private readonly List<IFixedSystem> _fixedSystems;
        private readonly List<IDestroySystem> _destroySystems;
        private readonly CoroutineStarter _coroutineStarter;

        public SystemManager(List<IPreInitializeSystem> preInitializeSystems, List<IInitializeSystem> initializeSystems,
            List<ICoroutineSystem> coroutineSystems, List<IUpdateSystem> updateSystems,
            List<IFixedSystem> fixedSystems, List<IDestroySystem> destroySystems,
            CoroutineStarter coroutineStarter)
        {
            _preInitializeSystems = preInitializeSystems;
            _initializeSystems = initializeSystems;
            _coroutineSystems = coroutineSystems;
            _updateSystems = updateSystems;
            _fixedSystems = fixedSystems;
            _destroySystems = destroySystems;
            _coroutineStarter = coroutineStarter;
        }

        public void AddInitSystem(IInitializeSystem initSystem)
        {
            if (_initializeSystems.Contains(initSystem)) return;

            _initializeSystems.Add(initSystem);
        }

        public bool ContainsUpdateSystem(IUpdateSystem updateSystem)
        {
            return _updateSystems.Contains(updateSystem);
        }

        public void AddUpdateSystem(IUpdateSystem updateSystem)
        {
            _updateSystems.Add(updateSystem);
        }

        public void RemoveUpdateSystem(IUpdateSystem updateSystem)
        {
            _updateSystems.Remove(updateSystem);
        }

        public bool ContainsFixedSystem(IFixedSystem fixedSystem)
        {
            return _fixedSystems.Contains(fixedSystem);
        }

        public void AddFixedSystem(IFixedSystem fixedSystem)
        {
            _fixedSystems.Add(fixedSystem);
        }

        public void RemoveFixedSystem(IFixedSystem fixedSystem)
        {
            _fixedSystems.Remove(fixedSystem);
        }

        public void AddDestroySystem(IDestroySystem destroySystem)
        {
            if (_destroySystems.Contains(destroySystem)) return;

            _destroySystems.Add(destroySystem);
        }

        public void PreInitialize()
        {
            for (var index = 0; index < _preInitializeSystems.Count; index++)
            {
                _preInitializeSystems[index].PreInitialize();
            }
        }

        public void Initialize()
        {
            for (var index = 0; index < _initializeSystems.Count; index++)
            {
                _initializeSystems[index].Initialize();
            }
        }

        public void StartCoroutines()
        {
            for (var index = 0; index < _coroutineSystems.Count; index++)
            {
                _coroutineStarter.StartCoroutine(_coroutineSystems[index].Start());
            }
        }

        public void Update()
        {
            for (var index = 0; index < _updateSystems.Count; index++)
            {
                _updateSystems[index].Update();
            }
        }

        public void FixedUpdate()
        {
            for (var index = 0; index < _fixedSystems.Count; index++)
            {
                _fixedSystems[index].FixedUpdate();
            }
        }

        public void Destroy()
        {
            for (var index = 0; index < _destroySystems.Count; index++)
            {
                _destroySystems[index].Destroy();
            }
        }
    }
}