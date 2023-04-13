using Game.Gameplay.Views.Screens;
using TegridyCore.FiniteStateMachine;

namespace Game.Gameplay.Transitions
{
    public class StatsScreenToStartTransition : TransitionBase
    {
        private readonly StatsScreenView _statsScreenView;
        protected override bool SmoothTransition => true;

        public StatsScreenToStartTransition(StateBase stateFrom, StateBase stateTo, StatsScreenView statsScreenView) :
            base(stateFrom, stateTo)
        {
            _statsScreenView = statsScreenView;
        }

        public override void OnTransitionAdded()
        {
            _statsScreenView.CloseButton.OnReleased += DoTransition;
        }

        public override void OnTransitionRemoved()
        {
            _statsScreenView.CloseButton.OnReleased -= DoTransition;
        }
    }
}