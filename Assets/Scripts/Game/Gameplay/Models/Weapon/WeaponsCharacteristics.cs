using System;
using System.Collections.Generic;
using System.Linq;
using Game.DataBase;
using Game.DataBase.Weapon;

namespace Game.Gameplay.Models.Weapon
{
    public class WeaponsCharacteristics
    {
        private readonly WeaponsDataBase _weaponsDataBase;

        public event Action OnUpdate;

        private readonly Dictionary<string, Dictionary<WeaponCharacteristicType, float>>
            _weaponsCharacteristics = new();

        public WeaponsCharacteristics(WeaponsDataBase weaponsDataBase)
        {
            _weaponsDataBase = weaponsDataBase;
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

            var characteristicValue = CalculateCharacteristicValue(weaponCharacteristicData,
                playerWeaponData._rarityType, playerWeaponData._level);

            SetCharacteristic(playerWeaponData,
                weaponCharacteristicData._weaponCharacteristicType, characteristicValue);
        }

        public void SetCharacteristic(PlayerWeaponData playerWeaponData,
            WeaponCharacteristicType weaponCharacteristicType,
            float value)
        {
            var characteristicValues = GetOrCreateCharacteristicValues(playerWeaponData);

            characteristicValues[weaponCharacteristicType] = value;

            OnUpdate?.Invoke();
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

        public float CalculateCharacteristicValue(WeaponCharacteristicData weaponCharacteristicData,
            RarityType rarityType, int level)
        {
            var weaponCharacteristicValue = weaponCharacteristicData._startValue;

            weaponCharacteristicValue += GetCharacteristicAddition(weaponCharacteristicData, level);

            return weaponCharacteristicValue;
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