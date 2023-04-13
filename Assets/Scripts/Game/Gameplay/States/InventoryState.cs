using Game.Gameplay.Views.Screens;
using TegridyCore.FiniteStateMachine;

namespace Game.Gameplay.States
{
    public class InventoryState : StateBase
    {
        private readonly InventoryScreenView _inventoryScreenView;

        public InventoryState(InventoryScreenView inventoryScreenView)
        {
            _inventoryScreenView = inventoryScreenView;
        }

        protected override void OnActivate()
        {
            _inventoryScreenView.gameObject.SetActive(true);
        }

        protected override void OnDeactivate()
        {
            _inventoryScreenView.gameObject.SetActive(false);
        }
    }
}