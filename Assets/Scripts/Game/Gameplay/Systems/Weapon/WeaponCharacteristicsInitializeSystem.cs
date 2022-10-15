using System.Collections.Generic;
using Game.DataBase.Weapon;
using Game.Gameplay.Models;
using Game.Gameplay.Models.Weapon;
using TegridyCore.Base;

namespace Game.Gameplay.Systems.Weapon
{
    public class WeaponCharacteristicsInitializeSystem : IInitializeSystem
    {
        private readonly Dictionary<WeaponType, Dictionary<WeaponCharacteristicType, int>> _weaponsCharacteristics;
        private readonly ApplicationData _applicationData;
        private readonly WeaponsDataBase _weaponsDataBase;

        public WeaponCharacteristicsInitializeSystem(CurrentCharacterWeaponsData currentCharacterWeaponsData,
            ApplicationData applicationData, WeaponsDataBase weaponsDataBase)
        {
            _weaponsCharacteristics = currentCharacterWeaponsData.WeaponsCharacteristics;
            _applicationData = applicationData;
            _weaponsDataBase = weaponsDataBase;
        }

        public void Initialize()
        {
            foreach (var record in _weaponsDataBase.Records)
            {
                foreach (var weaponCharacteristic in record._weaponCharacteristics)
                {
                    FillConcreteCharacteristic(record._weaponType, weaponCharacteristic);
                }
            }
        }

        private void FillConcreteCharacteristic(WeaponType weaponType, WeaponCharacteristic weaponCharacteristic)
        {
            var characteristicValues = GetOrCreateCharacteristicValues(weaponType);

            CalculateCharacteristic(characteristicValues, weaponCharacteristic, weaponType);
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

        private void CalculateCharacteristic(IDictionary<WeaponCharacteristicType, int> characteristicValues,
            WeaponCharacteristic weaponCharacteristic, WeaponType weaponType)
        {
            var characteristicType = weaponCharacteristic._weaponCharacteristicType;

            var characteristicValue = characteristicValues[characteristicType] = weaponCharacteristic._startValue;

            if (_applicationData.PlayerData.WeaponsSaveDataByType.TryGetValue(weaponType, out var weaponSaveData))

                for (var i = 0; i < weaponSaveData.Level; i++)
                {
                    characteristicValues[characteristicType] =
                        (int) (weaponCharacteristic._multiplier * characteristicValue);
                }
        }
    }
}