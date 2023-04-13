using Game.Gameplay.Views.Screens;
using TegridyCore.FiniteStateMachine;

namespace Game.Gameplay.Transitions
{
    public class EndScreenToStartScreenTransition : TransitionBase
    {
        private readonly EndScreenView _endScreenView;

        public EndScreenToStartScreenTransition(StateBase stateFrom, StateBase stateTo, EndScreenView endScreenView) : base(stateFrom, stateTo)
        {
            _endScreenView = endScreenView;
        }

        public override void OnTransitionAdded()
        {
            _endScreenView.CompleteButton.OnReleased += DoTransition;
        }

        public override void OnTransitionRemoved()
        {
            _endScreenView.CompleteButton.OnReleased -= DoTransition;
        }
    }
}