using Game.Gameplay.Views.UI.Screens;
using TegridyCore.FiniteStateMachine;

namespace Game.Gameplay.States
{
    public class GunCabinetState : StateBase
    {
        private readonly GunCabinetScreenView _gunCabinetScreenView;

        public GunCabinetState(GunCabinetScreenView gunCabinetScreenView)
        {
            _gunCabinetScreenView = gunCabinetScreenView;
        }

        protected override void OnActivate()
        {
            _gunCabinetScreenView.gameObject.SetActive(true);
        }

        protected override void OnDeactivate()
        {
            _gunCabinetScreenView.gameObject.SetActive(false);
        }
    }
}