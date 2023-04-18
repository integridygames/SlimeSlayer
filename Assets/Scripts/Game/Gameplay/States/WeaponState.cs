using Game.Gameplay.Views.UI.Screens;
using TegridyCore.FiniteStateMachine;

namespace Game.Gameplay.States
{
    public class WeaponState : StateBase
    {
        private readonly WeaponScreenView _weaponScreenView;

        public WeaponState(WeaponScreenView weaponScreenView)
        {
            _weaponScreenView = weaponScreenView;
        }

        protected override void OnActivate()
        {
            _weaponScreenView.gameObject.SetActive(true);
        }

        protected override void OnDeactivate()
        {
            _weaponScreenView.gameObject.SetActive(false);
        }
    }
}