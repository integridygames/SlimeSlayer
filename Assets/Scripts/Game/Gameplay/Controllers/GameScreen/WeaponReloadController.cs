using System;
using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Views.SampleScene.Screens;
using Game.Gameplay.WeaponMechanic;
using TegridyCore;
using TegridyCore.Base;
using Zenject;

namespace Game.Gameplay.Controllers.GameScreen
{
    public class WeaponReloadController : ControllerBase<GameScreenView>, IInitializable, IDisposable
    {
        private readonly CurrentCharacterWeaponsData _currentCharacterWeaponsData;

        public WeaponReloadController(GameScreenView controlledEntity, CurrentCharacterWeaponsData currentCharacterWeaponsData) : base(controlledEntity)
        {
            _currentCharacterWeaponsData = currentCharacterWeaponsData;
        }

        public void Initialize()
        {
            _currentCharacterWeaponsData.CurrentWeaponViewLeft.OnUpdate += OnCurrentLeftWeaponViewUpdateHandler;
            _currentCharacterWeaponsData.CurrentWeaponViewRight.OnUpdate += OnCurrentRightWeaponViewUpdateHandler;
        }

        public void Dispose()
        {
            _currentCharacterWeaponsData.CurrentWeaponViewLeft.OnUpdate -= OnCurrentLeftWeaponViewUpdateHandler;
            _currentCharacterWeaponsData.CurrentWeaponViewRight.OnUpdate -= OnCurrentRightWeaponViewUpdateHandler;
        }

        private void OnCurrentLeftWeaponViewUpdateHandler(RxValue<WeaponBase> weaponRxValue)
        {
            if (weaponRxValue.OldValue != null)
            {
                weaponRxValue.OldValue.ReloadProgress.OnUpdate -= OnCurrentLeftChargeUpdate;
            }

            weaponRxValue.NewValue.ReloadProgress.OnUpdate += OnCurrentLeftChargeUpdate;
        }

        private void OnCurrentRightWeaponViewUpdateHandler(RxValue<WeaponBase> weaponRxValue)
        {
            if (weaponRxValue.OldValue != null)
            {
                weaponRxValue.OldValue.ReloadProgress.OnUpdate -= OnCurrentRightChargeUpdate;
            }

            weaponRxValue.NewValue.ReloadProgress.OnUpdate += OnCurrentRightChargeUpdate;
        }

        private void OnCurrentLeftChargeUpdate(RxValue<float> chargeRxValue)
        {
            ControlledEntity.LeftReloadBar.SetReloadProgress(chargeRxValue.NewValue);
        }

        private void OnCurrentRightChargeUpdate(RxValue<float> chargeRxValue)
        {
            ControlledEntity.RightReloadBar.SetReloadProgress(chargeRxValue.NewValue);
        }
    }
}