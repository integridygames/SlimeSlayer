using System;
using System.Collections.Generic;
using Game.DataBase.Weapon;
using UnityEngine;

namespace Game.DataBase
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField] private int _currentLevel;
        [SerializeField] private List<WeaponData> _weaponsSaveData;
        [SerializeField] private int _currentLeftWeaponIndex;
        [SerializeField] private int _currentRightWeaponIndex;
        [SerializeField] private int _currentCoinsCount;

        public PlayerData()
        {
            _weaponsSaveData = new List<WeaponData>
            {
                new()
                {
                    _weaponType = WeaponType.Glock,
                    _rarityType = RarityType.Common,
                },
                new()
                {
                    _weaponType = WeaponType.Glock,
                    _rarityType = RarityType.Common,
                },
                new()
                {
                    _weaponType = WeaponType.Scar,
                    _rarityType = RarityType.Epic,
                },
                new()
                {
                    _weaponType = WeaponType.Uzi,
                    _rarityType = RarityType.Common,
                },
                new()
                {
                    _weaponType = WeaponType.Shotgun,
                    _rarityType = RarityType.Uncommon,
                },
                new()
                {
                    _weaponType = WeaponType.GrenadeLauncher,
                    _rarityType = RarityType.Common,
                },
                new()
                {
                    _weaponType = WeaponType.MiniGun,
                    _rarityType = RarityType.Common,
                },
                new()
                {
                    _weaponType = WeaponType.SniperRiffle,
                    _rarityType = RarityType.Common,
                },
            };

            _currentLeftWeaponIndex = 2;
            _currentRightWeaponIndex = 4;

            _currentCoinsCount = 1;
        }

        public int CurrentLevel
        {
            get => _currentLevel;
            set => _currentLevel = value;
        }


        public int CurrentLeftWeaponIndex
        {
            get => _currentLeftWeaponIndex;
            set => _currentLeftWeaponIndex = value;
        }

        public int CurrentRightWeaponIndex
        {
            get => _currentRightWeaponIndex;
            set => _currentRightWeaponIndex = value;
        }

        public List<WeaponData> WeaponsSaveData => _weaponsSaveData;

        public int CurrentCoinsCount
        {
            get => _currentCoinsCount;
            set => _currentCoinsCount = value;
        }
    }
}