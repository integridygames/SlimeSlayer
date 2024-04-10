using Game.DataBase.Weapon;
using Game.Gameplay.Models.Abilities;
using Game.Gameplay.Models.Character;
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
        private readonly CharacterCharacteristicsRepository _characterCharacteristicsRepository;


        private readonly ProgressBarView _leftReloadBarView;
        private readonly ProgressBarView _rightReloadBarView;

        private readonly RxField<float> _reloadProgress = 1;
        public IReadonlyRxField<float> ReloadProgress => _reloadProgress;
        public RxField<int> CurrentCharge { get; } = new();

        private bool _isInitialized;

        public CommonReloadComponent(WeaponsCharacteristics weaponsCharacteristics, PlayerWeaponData playerWeaponData,
            CharacterCharacteristicsRepository characterCharacteristicsRepository)
        {
            _weaponsCharacteristics = weaponsCharacteristics;
            _playerWeaponData = playerWeaponData;
            _characterCharacteristicsRepository = characterCharacteristicsRepository;
        }

        public bool NeedReload()
        {
            return CurrentCharge.Value == 0;
        }

        public void Reload()
        {
            var reloadSpeed =
                _weaponsCharacteristics.GetCharacteristic(_playerWeaponData,
                    WeaponCharacteristicType.ReloadSpeed);

            _characterCharacteristicsRepository.TryGetAbilityCharacteristic(
                AbilityCharacteristicType.ReloadBoostPercent, out float boostPercent);
            
            reloadSpeed += reloadSpeed * boostPercent;

            _reloadProgress.Value += Time.deltaTime * reloadSpeed;

            if (_reloadProgress.Value >= 1)
            {
                UpdateCharge();
            }
        }

        public void Reset()
        {
            UpdateCharge();
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