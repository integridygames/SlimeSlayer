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
        [SerializeField] private List<PlayerWeaponData> _weaponsSaveData;
        [SerializeField] private int _currentLeftWeaponIndex;
        [SerializeField] private int _currentRightWeaponIndex;
        [SerializeField] private int _currentCoinsCount;

        public PlayerData()
        {
            _weaponsSaveData = new List<PlayerWeaponData>
            {
                new(WeaponType.Glock, RarityType.Common),
                new(WeaponType.Glock, RarityType.Common),
                new(WeaponType.Scar, RarityType.Epic),
                new(WeaponType.Uzi, RarityType.Common),
                new(WeaponType.Shotgun, RarityType.Uncommon),
                new(WeaponType.GrenadeLauncher, RarityType.Common),
                new(WeaponType.MiniGun, RarityType.Common),
                new(WeaponType.SniperRiffle, RarityType.Common),
            };

            _currentLeftWeaponIndex = 2;
            _currentRightWeaponIndex = 4;

            _currentCoinsCount = 100;
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

        public List<PlayerWeaponData> WeaponsSaveData => _weaponsSaveData;

        public int CurrentCoinsCount
        {
            get => _currentCoinsCount;
            set => _currentCoinsCount = value;
        }
    }
}