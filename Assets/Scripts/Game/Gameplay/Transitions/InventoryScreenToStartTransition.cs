using Game.Gameplay.Views.Screens;
using TegridyCore.FiniteStateMachine;

namespace Game.Gameplay.Transitions
{
    public class InventoryScreenToStartTransition : TransitionBase
    {
        private readonly InventoryScreenView _inventoryScreenView;
        protected override bool SmoothTransition => true;

        public InventoryScreenToStartTransition(StateBase stateFrom, StateBase stateTo,
            InventoryScreenView inventoryScreenView) : base(stateFrom, stateTo)
        {
            _inventoryScreenView = inventoryScreenView;
        }

        public override void OnTransitionAdded()
        {
            _inventoryScreenView.CloseButton.OnReleased += DoTransition;
        }

        public override void OnTransitionRemoved()
        {
            _inventoryScreenView.CloseButton.OnReleased -= DoTransition;
        }
    }
}