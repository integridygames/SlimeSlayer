using System;
using Game.DataBase.Weapon;
using Game.Gameplay.Models;
using Game.Gameplay.Models.Weapon;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Systems.Weapon
{
    public class WeaponCharacteristicsInitializeSystem : IInitializeSystem
    {
        private readonly WeaponsCharacteristics _weaponsCharacteristics;
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
                    CalculateCharacteristic(weaponCharacteristic, record._weaponType);
                }
            }
        }

        private void CalculateCharacteristic(WeaponCharacteristic weaponCharacteristic, WeaponType weaponType)
        {
            var characteristicType = weaponCharacteristic._weaponCharacteristicType;

            var characteristicValue = weaponCharacteristic._startValue;

            if (_applicationData.PlayerData.WeaponsSaveDataByType.TryGetValue(weaponType, out var weaponSaveData))
            {
                for (var i = 0; i < weaponSaveData.Level; i++)
                {
                    characteristicValue += weaponCharacteristic._addition * weaponCharacteristic._additionMultiplier;
                }
            }

            _weaponsCharacteristics.SetCharacteristic(weaponType, characteristicType, characteristicValue);
        }
    }
}