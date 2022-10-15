using System.Collections.Generic;

namespace Game.Gameplay.Models.Weapon
{
    public class WeaponsCharacteristics
    {
        private readonly Dictionary<WeaponType, Dictionary<WeaponCharacteristicType, int>> _weaponsCharacteristics = new();

        public int GetCharacteristic(WeaponType weaponType, WeaponCharacteristicType weaponCharacteristicType)
        {
            var characteristicValues = GetOrCreateCharacteristicValues(weaponType);

            return characteristicValues[weaponCharacteristicType];
        }

        public void SetCharacteristic(WeaponType weaponType, WeaponCharacteristicType weaponCharacteristicType, int value)
        {
            var characteristicValues = GetOrCreateCharacteristicValues(weaponType);

            characteristicValues[weaponCharacteristicType] = value;
        }

        private Dictionary<WeaponCharacteristicType, int> GetOrCreateCharacteristicValues(WeaponType weaponType)
        {
            if (_weaponsCharacteristics.TryGetValue(weaponType, out var characteristicValues) == false)
            {
                characteristicValues = new Dictionary<WeaponCharacteristicType, int>();
                _weaponsCharacteristics[weaponType] = characteristicValues;
            }

            return characteristicValues;
        }
    }
}