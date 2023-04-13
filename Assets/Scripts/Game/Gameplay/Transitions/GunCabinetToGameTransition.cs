using Game.Gameplay.Views.Screens;
using TegridyCore.FiniteStateMachine;

namespace Game.Gameplay.Transitions
{
    public class GunCabinetToGameTransition : TransitionBase
    {
        private readonly GunCabinetScreenView _gunCabinetScreenView;
        protected override bool SmoothTransition => true;

        public GunCabinetToGameTransition(StateBase stateFrom, StateBase stateTo, GunCabinetScreenView gunCabinetScreenView) : base(stateFrom, stateTo)
        {
            _gunCabinetScreenView = gunCabinetScreenView;
        }

        public override void OnTransitionAdded()
        {
            _gunCabinetScreenView.CloseButton.OnReleased += DoTransition;
        }

        public override void OnTransitionRemoved()
        {
            _gunCabinetScreenView.CloseButton.OnReleased -= DoTransition;
        }
    }
}