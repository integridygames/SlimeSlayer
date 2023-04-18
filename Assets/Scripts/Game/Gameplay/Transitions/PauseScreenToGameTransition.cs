using Game.Gameplay.Views.UI.Screens;
using TegridyCore.FiniteStateMachine;

namespace Game.Gameplay.Transitions
{
    public class PauseScreenToGameTransition : TransitionBase
    {
        private readonly PauseScreenView _pauseScreenView;
        protected override bool SmoothTransition => true;

        public PauseScreenToGameTransition(StateBase stateFrom, StateBase stateTo, PauseScreenView pauseScreenView) : base(stateFrom, stateTo)
        {
            _pauseScreenView = pauseScreenView;
        }

        public override void OnTransitionAdded()
        {
            _pauseScreenView.CloseButton.OnReleased += DoTransition;
        }

        public override void OnTransitionRemoved()
        {
            _pauseScreenView.CloseButton.OnReleased -= DoTransition;
        }
    }
}