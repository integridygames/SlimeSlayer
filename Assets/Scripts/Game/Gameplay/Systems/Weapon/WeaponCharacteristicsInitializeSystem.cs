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
            foreach (var playerWeaponData in _applicationData.PlayerData.WeaponsSaveData)
            {
                var weaponRecord = _weaponsDataBase.GetRecordByType(playerWeaponData._weaponType);
                var weaponCharacteristics = weaponRecord.GetWeaponCharacteristics(playerWeaponData._rarityType);

                foreach (var weaponCharacteristicData in weaponCharacteristics)
                {
                    var characteristicValue =
                        _weaponsCharacteristics.CalculateCharacteristicValue(weaponCharacteristicData,
                            playerWeaponData._rarityType, playerWeaponData._level);
                    _weaponsCharacteristics.SetCharacteristic(playerWeaponData,
                        weaponCharacteristicData._weaponCharacteristicType, characteristicValue);
                }
            }
        }
    }
}