using Game.Gameplay.Services;
using Game.Gameplay.Views.UI.Screens.Gameplay;
using TegridyCore.FiniteStateMachine;
using Time = UnityEngine.Time;

namespace Game.Gameplay.Transitions
{
    public class GameToStartScreenTransition : TransitionBase
    {
        private readonly PauseView _pauseView;
        private readonly LevelDestroyService _levelDestroyService;

        public GameToStartScreenTransition(StateBase stateFrom, StateBase stateTo, PauseView pauseView, LevelDestroyService levelDestroyService) : base(stateFrom, stateTo)
        {
            _pauseView = pauseView;
            _levelDestroyService = levelDestroyService;
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
            _levelDestroyService.DestroyLevel();
            Time.timeScale = 1;
            DoTransition();
        }
    }
}