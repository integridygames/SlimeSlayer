using Game.Gameplay.Views.Screens;
using TegridyCore.FiniteStateMachine;

namespace Game.Gameplay.States
{
    public class PauseScreenState : StateBase
    {
        private readonly PauseScreenView _pauseScreenView;

        public PauseScreenState(PauseScreenView pauseScreenView)
        {
            _pauseScreenView = pauseScreenView;
        }

        protected override void OnActivate()
        {
            _pauseScreenView.gameObject.SetActive(true);
        }

        protected override void OnDeactivate()
        {
            _pauseScreenView.gameObject.SetActive(false);
        }
    }
}