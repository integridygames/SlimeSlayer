using Game.Gameplay.Views.UI.Screens.Gameplay;
using TegridyCore.FiniteStateMachine;
using Time = UnityEngine.Time;

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
            _pauseView.OnExitButtonPressed += OnExitButtonPressedHandler;
        }

        public override void OnTransitionRemoved()
        {
            _pauseView.OnExitButtonPressed -= OnExitButtonPressedHandler;
        }

        private void OnExitButtonPressedHandler()
        {
            Time.timeScale = 1;
            DoTransition();
        }
    }
}