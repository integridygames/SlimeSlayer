using System.Collections.Generic;
using JetBrains.Annotations;
using TegridyCore.Base;

namespace TegridyCore.StateBindings
{
    [UsedImplicitly]
    public class SystemStateBinder
    {
        private readonly SystemStateBindService _systemStateBindService;
        
        private readonly List<SystemStateBindRecord<IPreInitializeSystem>> _preInitializeSystemStateBindRecords;
        private readonly List<SystemStateBindRecord<IInitializeSystem>> _initializeSystemStateBindRecords;
        private readonly List<SystemStateBindRecord<ICoroutineSystem>> _coroutineSystemStateBindRecords;
        private readonly List<SystemStateBindRecord<IUpdateSystem>> _updateSystemStateBindRecords;
        private readonly List<SystemStateBindRecord<IFixedUpdateSystem>> _fixedSystemStateBindRecords;
        private readonly List<SystemStateBindRecord<IDestroySystem>> _destroySystemStateBindRecords;

        public SystemStateBinder(SystemStateBindService systemStateBindService,
            List<SystemStateBindRecord<IPreInitializeSystem>> preInitializeSystemStateBindRecords,
            List<SystemStateBindRecord<IInitializeSystem>> initializeSystemStateBindRecords,
            List<SystemStateBindRecord<ICoroutineSystem>> coroutineSystemStateBindRecords,
            List<SystemStateBindRecord<IUpdateSystem>> updateSystemStateBindRecords,
            List<SystemStateBindRecord<IFixedUpdateSystem>> fixedSystemStateBindRecords,
            List<SystemStateBindRecord<IDestroySystem>> destroySystemStateBindRecords
        )
        {
            _systemStateBindService = systemStateBindService;

            _preInitializeSystemStateBindRecords = preInitializeSystemStateBindRecords;
            _initializeSystemStateBindRecords = initializeSystemStateBindRecords;
            _coroutineSystemStateBindRecords = coroutineSystemStateBindRecords;
            _updateSystemStateBindRecords = updateSystemStateBindRecords;
            _fixedSystemStateBindRecords = fixedSystemStateBindRecords;
            _destroySystemStateBindRecords = destroySystemStateBindRecords;

            Bind();
        }

        private void Bind()
        {
            foreach (var bindRecord in _preInitializeSystemStateBindRecords)
            {
                var system = bindRecord.System;
                var state = bindRecord.StateBase;
                
                _systemStateBindService.BindPreInitSystem(system, state);
            }
            
            foreach (var bindRecord in _initializeSystemStateBindRecords)
            {
                var system = bindRecord.System;
                var state = bindRecord.StateBase;
                
                _systemStateBindService.BindInitSystem(system, state);
            }
            
            foreach (var bindRecord in _coroutineSystemStateBindRecords)
            {
                var system = bindRecord.System;
                var state = bindRecord.StateBase;
                
                _systemStateBindService.BindCoroutineSystem(system, state);
            }
            
            foreach (var bindRecord in _updateSystemStateBindRecords)
            {
                var system = bindRecord.System;
                var state = bindRecord.StateBase;
                
                _systemStateBindService.BindUpdateSystem(system, state);
            }
            
            foreach (var bindRecord in _fixedSystemStateBindRecords)
            {
                var system = bindRecord.System;
                var state = bindRecord.StateBase;
                
                _systemStateBindService.BindFixedSystem(system, state);
            }
            
            foreach (var bindRecord in _destroySystemStateBindRecords)
            {
                var system = bindRecord.System;
                var state = bindRecord.StateBase;
                
                _systemStateBindService.BindDestroySystem(system, state);
            }
        }
    }
}