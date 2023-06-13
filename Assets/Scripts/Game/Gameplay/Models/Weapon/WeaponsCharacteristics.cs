using System;
using System.Collections.Generic;
using Game.DataBase;
using Game.DataBase.Weapon;

namespace Game.Gameplay.Models.Weapon
{
    public class WeaponsCharacteristics
    {
        public event Action OnUpdate;

        private readonly Dictionary<string, Dictionary<WeaponCharacteristicType, float>>
            _weaponsCharacteristics = new();

        public float GetCharacteristic(PlayerWeaponData playerWeaponData,
            WeaponCharacteristicType weaponCharacteristicType)
        {
            var characteristicValues = GetOrCreateCharacteristicValues(playerWeaponData);

            return characteristicValues[weaponCharacteristicType];
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