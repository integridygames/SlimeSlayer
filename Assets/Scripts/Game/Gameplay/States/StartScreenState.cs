using Game.Gameplay.Views.UI.Screens;
using TegridyCore.FiniteStateMachine;

namespace Game.Gameplay.States
{
    public class StartScreenState : StateBase
    {
        private readonly StartScreenView _startScreenView;

        public StartScreenState(StartScreenView startScreenView)
        {
            _startScreenView = startScreenView;
        }

        protected override void OnActivate()
        {
            _startScreenView.gameObject.SetActive(true);
        }

        protected override void OnDeactivate()
        {
            _startScreenView.gameObject.SetActive(false);
        }
    }
}