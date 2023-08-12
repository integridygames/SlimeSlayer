using System;
using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Views.UI.Screens;
using Game.Gameplay.WeaponMechanics;
using TegridyCore;
using TegridyCore.Base;
using Zenject;

namespace Game.Gameplay.Controllers.GameScreen
{
    public class WeaponReloadController : ControllerBase<GameScreenView>, IInitializable, IDisposable
    {
        private readonly CharacterWeaponsRepository _characterWeaponsRepository;

        public WeaponReloadController(GameScreenView controlledEntity,
            CharacterWeaponsRepository characterWeaponsRepository) : base(controlledEntity)
        {
            _characterWeaponsRepository = characterWeaponsRepository;
        }

        public void Initialize()
        {
            _characterWeaponsRepository.CurrentWeaponViewLeft.OnUpdate += OnCurrentLeftWeaponViewUpdateHandler;
            _characterWeaponsRepository.CurrentWeaponViewRight.OnUpdate += OnCurrentRightWeaponViewUpdateHandler;
        }

        public void Dispose()
        {
            _characterWeaponsRepository.CurrentWeaponViewLeft.OnUpdate -= OnCurrentLeftWeaponViewUpdateHandler;
            _characterWeaponsRepository.CurrentWeaponViewRight.OnUpdate -= OnCurrentRightWeaponViewUpdateHandler;
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
            ControlledEntity.LeftReloadBar.gameObject.SetActive(chargeRxValue.NewValue != 0);
            ControlledEntity.LeftReloadBar.SetProgress(chargeRxValue.NewValue);
        }

        private void OnCurrentRightChargeUpdate(RxValue<float> chargeRxValue)
        {
            ControlledEntity.RightReloadBar.gameObject.SetActive(chargeRxValue.NewValue != 0);
            ControlledEntity.RightReloadBar.SetProgress(chargeRxValue.NewValue);
        }
    }
}