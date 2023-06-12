using System.Collections.Generic;
using Game.DataBase;
using Game.DataBase.Weapon;

namespace Game.Gameplay.Models.Weapon
{
    public class WeaponsCharacteristics
    {
        private readonly Dictionary<(WeaponType, RarityType), Dictionary<WeaponCharacteristicType, float>>
            _weaponsCharacteristics = new();

        public float GetCharacteristic(WeaponType weaponType, RarityType rarityType,
            WeaponCharacteristicType weaponCharacteristicType)
        {
            var characteristicValues = GetOrCreateCharacteristicValues(weaponType, rarityType);

            return characteristicValues[weaponCharacteristicType];
        }

        public void SetCharacteristic(WeaponType weaponType, RarityType rarityType,
            WeaponCharacteristicType weaponCharacteristicType,
            float value)
        {
            var characteristicValues = GetOrCreateCharacteristicValues(weaponType, rarityType);

            characteristicValues[weaponCharacteristicType] = value;
        }

        private Dictionary<WeaponCharacteristicType, float> GetOrCreateCharacteristicValues(WeaponType weaponType,
            RarityType rarityType)
        {
            if (_weaponsCharacteristics.TryGetValue((weaponType, rarityType), out var characteristicValues) == false)
            {
                characteristicValues = new Dictionary<WeaponCharacteristicType, float>();
                _weaponsCharacteristics[(weaponType, rarityType)] = characteristicValues;
            }

            return characteristicValues;
        }
    }
}