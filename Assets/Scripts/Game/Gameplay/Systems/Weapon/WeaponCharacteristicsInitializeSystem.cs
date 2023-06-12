using System;
using System.Linq;
using Game.DataBase;
using Game.DataBase.Weapon;
using Game.Gameplay.Models;
using Game.Gameplay.Models.Weapon;
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

                    var level = weaponSaveData?._level ?? 0;

                    foreach (var weaponCharacteristic in weaponRecord.GetWeaponCharacteristics(rarity))
                    {
                        var characteristicValue =
                            _weaponsCharacteristics.CalculateCharacteristicValue(weaponCharacteristic, rarity, level);

                        _weaponsCharacteristics.SetCharacteristic(weaponRecord._weaponType, rarity,
                            weaponCharacteristic._weaponCharacteristicType, characteristicValue);
                    }
                }
            }
        }
    }
}