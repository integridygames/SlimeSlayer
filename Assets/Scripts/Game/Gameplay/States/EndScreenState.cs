using Game.Gameplay.Views.Screens;
using TegridyCore.FiniteStateMachine;

namespace Game.Gameplay.States
{
    public class EndScreenState : StateBase
    {
        private readonly EndScreenView _endScreenView;

        public EndScreenState(EndScreenView endScreenView)
        {
            _endScreenView = endScreenView;
        }

        protected override void OnActivate()
        {
            _endScreenView.gameObject.SetActive(true);
        }

        protected override void OnDeactivate()
        {
            _endScreenView.gameObject.SetActive(false);
        }
    }
}