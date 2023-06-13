using Game.DataBase.Weapon;
using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Views.Weapon;
using TegridyCore;
using UnityEngine;

namespace Game.Gameplay.WeaponMechanics.Components.ReloadComponents
{
    public class CommonReloadComponent : IReloadComponent
    {
        private readonly WeaponsCharacteristics _weaponsCharacteristics;
        private readonly PlayerWeaponData _playerWeaponData;


        private readonly ReloadBarView _leftReloadBarView;
        private readonly ReloadBarView _rightReloadBarView;

        private readonly RxField<float> _reloadProgress = 1;
        public IReadonlyRxField<float> ReloadProgress => _reloadProgress;
        public RxField<int> CurrentCharge { get; } = new();

        private bool _isInitialized;

        public CommonReloadComponent(WeaponsCharacteristics weaponsCharacteristics, PlayerWeaponData playerWeaponData)
        {
            _weaponsCharacteristics = weaponsCharacteristics;
            _playerWeaponData = playerWeaponData;
        }

        public bool NeedReload()
        {
            return CurrentCharge.Value == 0;
        }

        public void Reload()
        {
            var reloadTime =
                _weaponsCharacteristics.GetCharacteristic(_playerWeaponData,
                    WeaponCharacteristicType.ReloadTime);

            _reloadProgress.Value += Time.deltaTime / reloadTime;

            if (_reloadProgress.Value >= 1)
            {
                UpdateCharge();
            }
        }

        private void UpdateCharge()
        {
            _reloadProgress.Value = 0;

            var charge =
                (int) _weaponsCharacteristics.GetCharacteristic(_playerWeaponData,
                    WeaponCharacteristicType.Charge);
            CurrentCharge.Value = charge;
        }
    }
}