using System;
using System.Linq;
using Game.DataBase;
using Game.DataBase.Weapon;
using Game.Gameplay.Models;
using Game.Gameplay.Models.Weapon;
using JetBrains.Annotations;
using TegridyCore.Base;

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
            var rarities = Enum.GetValues(typeof(RarityType));

            foreach (var weaponRecord in _weaponsDataBase.Records)
            {
                foreach (RarityType rarity in rarities)
                {
                    var weaponSaveData = _applicationData.PlayerData.WeaponsSaveData.FirstOrDefault(x =>
                        x._weaponType == weaponRecord._weaponType && x._rarityType == rarity);

                    foreach (var weaponCharacteristic in weaponRecord._weaponCharacteristics)
                    {
                        CalculateCharacteristicValue(weaponCharacteristic, weaponRecord._weaponType, rarity,
                            weaponSaveData);
                    }
                }
            }
        }

        private void CalculateCharacteristicValue(WeaponCharacteristicData weaponCharacteristicData, WeaponType weaponType,
            RarityType rarityType,
            [CanBeNull] WeaponData weaponSaveData)
        {
            var weaponCharacteristicValue = weaponCharacteristicData._startValue;

            if (weaponSaveData != null)
            {
                for (var i = 0; i < weaponSaveData._level; i++)
                {
                    weaponCharacteristicValue +=
                        weaponCharacteristicData._addition * weaponCharacteristicData._additionMultiplier;
                }
            }

            _weaponsCharacteristics.SetCharacteristic(weaponType, rarityType,
                weaponCharacteristicData._weaponCharacteristicType, weaponCharacteristicValue);
        }
    }
}