using System;
using System.Collections.Generic;
using Game.DataBase.Character;
using Game.DataBase.Weapon;
using UnityEngine;

namespace Game.DataBase
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField] private int _currentLevel;
        [SerializeField] private List<PlayerCharacteristicData> _playerCharacteristicsData;
        [SerializeField] private List<PlayerWeaponData> _weaponsSaveData;
        [SerializeField] private string _currentLeftWeaponGuid;
        [SerializeField] private string _currentRightWeaponGuid;
        [SerializeField] private int _currentCoinsCount;

        public PlayerData()
        {
            _weaponsSaveData = new List<PlayerWeaponData>
            {
                new(WeaponType.Glock, RarityType.Common),
                new(WeaponType.Glock, RarityType.Common),
                new(WeaponType.Glock, RarityType.Common),
                new(WeaponType.Glock, RarityType.Common),
                new(WeaponType.Shotgun, RarityType.Epic),
                new(WeaponType.Shotgun, RarityType.Epic),
                new(WeaponType.Shotgun, RarityType.Epic),
                new(WeaponType.Shotgun, RarityType.Epic),
                new(WeaponType.Scar, RarityType.Epic),
                new(WeaponType.Uzi, RarityType.Common),
                new(WeaponType.Shotgun, RarityType.Uncommon),
                new(WeaponType.GrenadeLauncher, RarityType.Common),
                new(WeaponType.MiniGun, RarityType.Common),
                new(WeaponType.SniperRiffle, RarityType.Common),
                new(WeaponType.Scar, RarityType.Legendary),
            };

            _currentLeftWeaponGuid = _weaponsSaveData[0]._guid;
            _currentRightWeaponGuid = _weaponsSaveData[1]._guid;
            _weaponsSaveData[0]._equipped = true;
            _weaponsSaveData[1]._equipped = true;

            _currentCoinsCount = 250;

            _playerCharacteristicsData = new List<PlayerCharacteristicData>
            {
                new(CharacterCharacteristicType.MaxHealth, false),
                new(CharacterCharacteristicType.BaseDamage, false),
                new(CharacterCharacteristicType.Regeneration, false),
                new(CharacterCharacteristicType.Speed, false),
                new(CharacterCharacteristicType.HealthSteal, false),
                new(CharacterCharacteristicType.AttackRange, false),
                new(CharacterCharacteristicType.Shield, true),
                new(CharacterCharacteristicType.ShieldRepairing, true),
            };
        }

        public int CurrentLevel
        {
            get => _currentLevel;
            set => _currentLevel = value;
        }


        public string CurrentLeftWeaponGuid
        {
            get => _currentLeftWeaponGuid;
            set => _currentLeftWeaponGuid = value;
        }

        public string CurrentRightWeaponGuid
        {
            get => _currentRightWeaponGuid;
            set => _currentRightWeaponGuid = value;
        }

        public List<PlayerWeaponData> WeaponsSaveData => _weaponsSaveData;

        public int CurrentCoinsCount
        {
            get => _currentCoinsCount;
            set => _currentCoinsCount = value;
        }

        public IReadOnlyList<PlayerCharacteristicData> PlayerCharacteristicsData => _playerCharacteristicsData;
    }
}