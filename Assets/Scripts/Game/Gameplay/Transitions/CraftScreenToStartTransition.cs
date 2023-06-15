using Game.Gameplay.Views.UI.Screens.Weapons;
using TegridyCore.FiniteStateMachine;

namespace Game.Gameplay.Transitions
{
    public class CraftScreenToStartTransition : TransitionBase
    {
        private readonly CraftScreenView _craftScreenView;

        protected override bool SmoothTransition => true;

        public CraftScreenToStartTransition(StateBase stateFrom, StateBase stateTo, CraftScreenView craftScreenView) : base(stateFrom, stateTo)
        {
            _craftScreenView = craftScreenView;
        }

        public override void OnTransitionAdded()
        {
            _craftScreenView.OnCloseButtonPressed += DoTransition;
        }

        public override void OnTransitionRemoved()
        {
            _craftScreenView.OnCloseButtonPressed -= DoTransition;
        }
    }
}