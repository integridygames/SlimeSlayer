using Game.Gameplay.Views.UI.Screens.Gameplay;
using TegridyCore.FiniteStateMachine;

namespace Game.Gameplay.Transitions
{
    public class GameToStartScreenTransition : TransitionBase
    {
        private readonly PauseView _pauseView;

        public GameToStartScreenTransition(StateBase stateFrom, StateBase stateTo, PauseView pauseView) : base(stateFrom, stateTo)
        {
            _pauseView = pauseView;
        }

        public override void OnTransitionAdded()
        {
            _pauseView.OnExitButtonPressed += DoTransition;
        }

        public override void OnTransitionRemoved()
        {
            _pauseView.OnExitButtonPressed -= DoTransition;
        }
    }
}