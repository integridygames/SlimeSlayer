using System;
using System.Collections.Generic;
using System.Linq;
using Game.Gameplay.Models.Weapon;
using UnityEngine;

namespace Game.DataBase
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField] private int _currentLevel;
        [SerializeField] private List<WeaponSaveData> _weaponsSaveData;

        public PlayerData()
        {
            _weaponsSaveData = new List<WeaponSaveData>();
        }

        public int CurrentLevel
        {
            get => _currentLevel;
            set => _currentLevel = value;
        }

        private Dictionary<WeaponType, WeaponSaveData> _weaponsSaveDataByType;

        public Dictionary<WeaponType, WeaponSaveData> WeaponsSaveDataByType =>
            _weaponsSaveDataByType ??= _weaponsSaveData.ToDictionary(x => x.WeaponType);
    }
}