using TegridyCore;
using TegridyCore.Base;
using TegridyCore.FiniteStateMachine;
using TegridyCore.StateBindings;
using Zenject;

namespace TegridyUtils.Extensions
{
    public static class ZenjectExtensions
    {
        public static void BindPreInitializeSystem<TSystem>(this DiContainer container, TSystem system)
            where TSystem : IPreInitializeSystem
        {
            container.Bind<IPreInitializeSystem>()
                .To<TSystem>()
                .FromInstance(system)
                .WhenInjectedInto<SystemManager>();
        }
        
        public static void BindInitializeSystem<TSystem>(this DiContainer container, TSystem system)
            where TSystem : IInitializeSystem
        {
            container.Bind<IInitializeSystem>()
                .To<TSystem>()
                .FromInstance(system)
                .WhenInjectedInto<SystemManager>();
        }
        
        public static void BindCoroutineSystem<TSystem>(this DiContainer container, TSystem system)
            where TSystem : ICoroutineSystem
        {
            container.Bind<ICoroutineSystem>()
                .To<TSystem>()
                .FromInstance(system)
                .WhenInjectedInto<SystemManager>();
        }

        public static void BindUpdateSystem<TSystem>(this DiContainer container, TSystem system)
            where TSystem : IUpdateSystem
        {
            container.Bind<IUpdateSystem>()
                .To<TSystem>()
                .FromInstance(system)
                .WhenInjectedInto<SystemManager>();
        }

        public static void BindFixedSystem<TSystem>(this DiContainer container, TSystem system)
            where TSystem : IFixedUpdateSystem
        {
            container.Bind<IFixedUpdateSystem>()
                .To<TSystem>()
                .FromInstance(system)
                .WhenInjectedInto<SystemManager>();
        }

        public static TState CreateAndBindState<TState>(this DiContainer container)
            where TState : StateBase
        {
            var state = container.Instantiate<TState>();

            container.Bind<StateBase>()
                .To<TState>()
                .FromInstance(state)
                .AsSingle()
                .WhenInjectedInto<StateMachine>();

            return state;
        }

        public static void BindPreInitializeSystemWithState(this DiContainer container, IPreInitializeSystem system,
            StateBase state)
        {
            var systemStateBindRecord = new SystemStateBindRecord<IPreInitializeSystem>(system, state);

            container.Bind<SystemStateBindRecord<IPreInitializeSystem>>()
                .FromInstance(systemStateBindRecord)
                .WhenInjectedInto<SystemStateBinder>();
        }

        public static void BindInitializeSystemWithState(this DiContainer container, IInitializeSystem system,
            StateBase state)
        {
            var systemStateBindRecord = new SystemStateBindRecord<IInitializeSystem>(system, state);

            container.Bind<SystemStateBindRecord<IInitializeSystem>>()
                .FromInstance(systemStateBindRecord)
                .WhenInjectedInto<SystemStateBinder>();
        }

        public static void BindCoroutineSystemWithState(this DiContainer container, ICoroutineSystem system,
            StateBase state)
        {
            var systemStateBindRecord = new SystemStateBindRecord<ICoroutineSystem>(system, state);

            container.Bind<SystemStateBindRecord<ICoroutineSystem>>()
                .FromInstance(systemStateBindRecord)
                .WhenInjectedInto<SystemStateBinder>();
        }

        public static void BindUpdateSystemWithState(this DiContainer container, IUpdateSystem system,
            StateBase state)
        {
            var systemStateBindRecord = new SystemStateBindRecord<IUpdateSystem>(system, state);

            container.Bind<SystemStateBindRecord<IUpdateSystem>>()
                .FromInstance(systemStateBindRecord)
                .WhenInjectedInto<SystemStateBinder>();
        }

        public static void BindFixedSystemWithState(this DiContainer container, IFixedUpdateSystem system,
            StateBase state)
        {
            var systemStateBindRecord = new SystemStateBindRecord<IFixedUpdateSystem>(system, state);

            container.Bind<SystemStateBindRecord<IFixedUpdateSystem>>()
                .FromInstance(systemStateBindRecord)
                .WhenInjectedInto<SystemStateBinder>();
        }

        public static void BindDestroySystemWithState(this DiContainer container, IDestroySystem system,
            StateBase state)
        {
            var systemStateBindRecord = new SystemStateBindRecord<IDestroySystem>(system, state);

            container.Bind<SystemStateBindRecord<IDestroySystem>>()
                .FromInstance(systemStateBindRecord)
                .WhenInjectedInto<SystemStateBinder>();
        }

        public static void CreateAndBindTransition<TTransition>(this DiContainer container, StateBase startState,
            StateBase endState)
            where TTransition : TransitionBase
        {
            container.Bind<TransitionBase>()
                .To<TTransition>()
                .AsSingle()
                .WithArguments(startState, endState)
                .WhenInjectedInto<StateMachine>();
        }
    }
}