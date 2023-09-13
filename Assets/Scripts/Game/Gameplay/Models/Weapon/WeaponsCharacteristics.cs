using System;
using System.Collections.Generic;
using System.Linq;
using Game.DataBase.Weapon;
using Game.Services;

namespace Game.Gameplay.Models.Weapon
{
    public class WeaponsCharacteristics
    {
        public event Action OnUpdate;

        private readonly WeaponsDataBase _weaponsDataBase;
        private readonly ApplicationData _applicationData;

        private readonly Dictionary<string, Dictionary<WeaponCharacteristicType, float>>
            _weaponsCharacteristics = new();

        public WeaponsCharacteristics(WeaponsDataBase weaponsDataBase, ApplicationData applicationData)
        {
            _weaponsDataBase = weaponsDataBase;
            _applicationData = applicationData;
        }

        public float GetCharacteristic(PlayerWeaponData playerWeaponData,
            WeaponCharacteristicType weaponCharacteristicType)
        {
            var characteristicValues = GetOrCreateCharacteristicValues(playerWeaponData);

            if (characteristicValues.ContainsKey(weaponCharacteristicType) == false)
            {
                FindAndCalculateCharacteristic(playerWeaponData, weaponCharacteristicType);
            }

            return characteristicValues[weaponCharacteristicType];
        }

        private void FindAndCalculateCharacteristic(PlayerWeaponData playerWeaponData,
            WeaponCharacteristicType weaponCharacteristicType)
        {
            var weaponRecord = _weaponsDataBase.GetRecordByType(playerWeaponData._weaponType);
            var weaponCharacteristics = weaponRecord.GetWeaponCharacteristics(playerWeaponData._rarityType);
            var weaponCharacteristicData =
                weaponCharacteristics.First(x => x._weaponCharacteristicType == weaponCharacteristicType);

            var characteristicValue = CalculateCharacteristicValue(weaponCharacteristicData, playerWeaponData._level);

            SetCharacteristic(playerWeaponData,
                weaponCharacteristicData._weaponCharacteristicType, characteristicValue);
        }

        private Dictionary<WeaponCharacteristicType, float> GetOrCreateCharacteristicValues(
            PlayerWeaponData playerWeaponData)
        {
            if (_weaponsCharacteristics.TryGetValue(playerWeaponData._guid, out var characteristicValues) == false)
            {
                characteristicValues = new Dictionary<WeaponCharacteristicType, float>();
                _weaponsCharacteristics[playerWeaponData._guid] = characteristicValues;
            }

            return characteristicValues;
        }

        public void UpdateCharacteristics(PlayerWeaponData playerWeaponData)
        {
            var weaponRecord = _weaponsDataBase.GetRecordByType(playerWeaponData._weaponType);

            foreach (var weaponCharacteristicData in weaponRecord.GetWeaponCharacteristics(playerWeaponData._rarityType))
            {
                var currentWeaponCharacteristic =
                    CalculateCharacteristicValue(weaponCharacteristicData, playerWeaponData._level);

                SetCharacteristic(playerWeaponData,
                    weaponCharacteristicData._weaponCharacteristicType, currentWeaponCharacteristic);
            }
        }

        public float CalculateCharacteristicValue(WeaponCharacteristicData weaponCharacteristicData, int level)
        {
            var weaponCharacteristicValue = weaponCharacteristicData._startValue;

            weaponCharacteristicValue += GetCharacteristicAddition(weaponCharacteristicData, level);

            return weaponCharacteristicValue;
        }

        private void SetCharacteristic(PlayerWeaponData playerWeaponData,
            WeaponCharacteristicType weaponCharacteristicType,
            float value)
        {
            var characteristicValues = GetOrCreateCharacteristicValues(playerWeaponData);

            characteristicValues[weaponCharacteristicType] = value;

            SaveLoadDataService.Save(_applicationData.PlayerData);

            OnUpdate?.Invoke();
        }

        public float GetCharacteristicAddition(WeaponCharacteristicData weaponCharacteristicData, int level)
        {
            float result = 0;

            for (var i = 0; i < level; i++)
            {
                result += weaponCharacteristicData._addition * weaponCharacteristicData._additionMultiplier;
            }

            return result;
        }
    }
}