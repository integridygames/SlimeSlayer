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
        [SerializeField] private List<WeaponSaveData> _weaponsSaveData;
        [SerializeField] private int _currentLeftWeaponIndex;
        [SerializeField] private int _currentRightWeaponIndex;

        public PlayerData()
        {
            _weaponsSaveData = new List<WeaponSaveData>
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
                    _rarityType = RarityType.Common,
                },
                new()
                {
                    _weaponType = WeaponType.Uzi,
                    _rarityType = RarityType.Common,
                },
                new()
                {
                    _weaponType = WeaponType.Shotgun,
                    _rarityType = RarityType.Common,
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

            _currentLeftWeaponIndex = 0;
            _currentRightWeaponIndex = 1;
        }

        public int CurrentLevel
        {
            get => _currentLevel;
            set => _currentLevel = value;
        }


        public int CurrentLeftWeaponIndex => _currentLeftWeaponIndex;

        public int CurrentRightWeaponIndex => _currentRightWeaponIndex;

        public List<WeaponSaveData> WeaponsSaveData => _weaponsSaveData;
    }
}