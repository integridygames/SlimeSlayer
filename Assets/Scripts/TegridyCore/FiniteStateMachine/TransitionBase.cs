using System;

namespace TegridyCore.FiniteStateMachine
{
    public abstract class TransitionBase
    {
        private StateBase StateFrom { get; }
        private StateBase StateTo { get; }

        /// <summary>
        /// when smoothTransition is true - it doesn't call PreInitialize and Initialize systems 
        /// </summary>
        protected virtual bool SmoothTransition { get; } = false;

        public event Action<StateBase, bool> OnTransitionComplete;

        private Func<bool> _condition;

        protected TransitionBase(StateBase stateFrom, StateBase stateTo)
        {
            StateFrom = stateFrom;
            StateTo = stateTo;
        }

        public void SetCondition(Func<bool> condition)
        {
            _condition = condition;
        }

        public void DoTransition()
        {
            if (!StateFrom.IsActive || _condition != null && !_condition.Invoke())
                return;

            StateFrom.Deactivate();
            StateTo.Activate();
            OnTransitionComplete?.Invoke(StateTo, SmoothTransition);
        }

        public virtual void OnTransitionAdded()
        {
        }

        public virtual void OnTransitionRemoved()
        {
        }
    }
}