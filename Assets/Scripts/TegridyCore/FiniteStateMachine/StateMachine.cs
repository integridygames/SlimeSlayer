using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace TegridyCore.FiniteStateMachine
{
    [UsedImplicitly]
    public class StateMachine : IDisposable
    {
        public event Action<StateBase, bool> OnStateActivate;
        public event Action<StateBase> OnStateDeactivate;

        public StateBase CurrentState { get; private set; }

        private readonly Dictionary<Type, TransitionBase> _typeToTransition = new Dictionary<Type, TransitionBase>();
        private readonly List<TransitionBase> _transitions = new List<TransitionBase>();
        private readonly List<StateBase> _states = new List<StateBase>();

        public StateMachine(List<StateBase> states, List<TransitionBase> transitions)
        {
            _states.AddRange(states);
            
            foreach (var transition in transitions)
            {
                AddTransition(transition);
            }
        }

        public void ForceDoTransition<TTransition>() where TTransition : TransitionBase
        {
            var type = typeof(TTransition);
            var transition = _typeToTransition[type];
            transition.DoTransition();
        }

        public void Start()
        {
            ForceSet(_states.First());
        }

        public void AddTransition(TransitionBase transition)
        {
            _transitions.Add(transition);
            _typeToTransition.Add(transition.GetType(), transition);

            transition.OnTransitionAdded();
            transition.OnTransitionComplete += OnTransitionCompleteHandler;
        }

        public void AddState(StateBase state)
        {
            _states.Add(state);
        }

        public void ForceSet(StateBase startState)
        {
            foreach (var state in _states)
            {
                if (state == startState) continue;

                state.Deactivate();
            }

            startState.Activate();
            SetNewState(startState, false);
        }

        public void Dispose()
        {
            foreach (var transition in _transitions)
            {
                transition.OnTransitionComplete -= OnTransitionCompleteHandler;
                transition.OnTransitionRemoved();
            }
            
            _transitions.Clear();
            _typeToTransition.Clear();
        }

        private void OnTransitionCompleteHandler(StateBase state, bool smoothTransition)
        {
            SetNewState(state, smoothTransition);
        }

        private void SetNewState(StateBase state, bool smoothTransition)
        {
            if (CurrentState != null)
                OnStateDeactivate?.Invoke(CurrentState);

            CurrentState = state;

            OnStateActivate?.Invoke(CurrentState, smoothTransition);
        }
    }
}