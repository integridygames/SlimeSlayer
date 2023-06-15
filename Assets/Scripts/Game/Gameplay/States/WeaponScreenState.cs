using Game.Gameplay.Views.UI.Screens.Weapons;
using TegridyCore.FiniteStateMachine;

namespace Game.Gameplay.States
{
    public class WeaponScreenState : StateBase
    {
        private readonly WeaponScreenView _weaponScreenView;

        public WeaponScreenState(WeaponScreenView weaponScreenView)
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