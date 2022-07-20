using TegridyCore.FiniteStateMachine;

namespace TegridyCore.StateBindings
{
    public class SystemStateBindRecord<TSystem>
    {
        public readonly TSystem System;
        public readonly StateBase StateBase;

        public SystemStateBindRecord(TSystem system, StateBase stateBase)
        {
            System = system;
            StateBase = stateBase;
        }
    }
}