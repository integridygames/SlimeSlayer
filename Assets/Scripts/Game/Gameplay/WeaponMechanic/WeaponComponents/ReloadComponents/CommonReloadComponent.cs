using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Views.Weapon;
using TegridyCore;
using UnityEngine;

namespace Game.Gameplay.WeaponMechanic.WeaponComponents.ReloadComponents
{
    public class CommonReloadComponent : IReloadComponent
    {
        private readonly WeaponsCharacteristics _weaponsCharacteristics;
        private readonly WeaponType _weaponType;

        private readonly ReloadBarView _leftReloadBarView;
        private readonly ReloadBarView _rightReloadBarView;

        private readonly RxField<float> _reloadProgress = 1;
        public IReadonlyRxField<float> ReloadProgress => _reloadProgress;
        public RxField<int> CurrentCharge { get; } = new();

        private bool _isInitialized;

        public CommonReloadComponent(WeaponsCharacteristics weaponsCharacteristics, WeaponType weaponType)
        {
            _weaponsCharacteristics = weaponsCharacteristics;
            _weaponType = weaponType;
        }

        public bool NeedReload()
        {
            return CurrentCharge.Value == 0;
        }

        public void Reload()
        {
            var reloadTime = _weaponsCharacteristics.GetCharacteristic(_weaponType, WeaponCharacteristicType.ReloadTime);
            _reloadProgress.Value += Time.deltaTime / reloadTime;

            if (_reloadProgress.Value >= 1)
            {
                UpdateCharge();
            }
        }

        private void UpdateCharge()
        {
            _reloadProgress.Value = 0;

            var charge = _weaponsCharacteristics.GetCharacteristic(_weaponType, WeaponCharacteristicType.Charge);
            CurrentCharge.Value = charge;
        }
    }
}