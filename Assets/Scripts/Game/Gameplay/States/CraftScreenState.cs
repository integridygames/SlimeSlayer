using Game.Gameplay.Views.UI.Screens.Weapons;
using TegridyCore.FiniteStateMachine;

namespace Game.Gameplay.States
{
    public class CraftScreenState : StateBase
    {
        private readonly CraftScreenView _craftScreenView;

        public CraftScreenState(CraftScreenView craftScreenView)
        {
            _craftScreenView = craftScreenView;
        }

        protected override void OnActivate()
        {
            _craftScreenView.gameObject.SetActive(true);
        }

        protected override void OnDeactivate()
        {
            _craftScreenView.gameObject.SetActive(false);
        }
    }
}