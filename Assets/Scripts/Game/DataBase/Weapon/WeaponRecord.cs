using System;
using System.Collections.Generic;
using System.Linq;
using Game.Gameplay.Views.Weapons;
using TegridyUtils.Attributes;
using UnityEngine;

namespace Game.DataBase.Weapon
{
    [Serializable]
    public class WeaponRecord
    {
        [ArrayKey]
        public WeaponType _weaponType;

        public WeaponViewBase _weaponPrefab;
        public Sprite _weaponSprite;

        [SerializeField]
        [ArrayWithKey]
        private List<WeaponCharacteristicWithRarity> _weaponCharacteristicsWithRarity;

        public List<WeaponCharacteristicData> GetWeaponCharacteristics(RarityType rarityType)
        {
            return _weaponCharacteristicsWithRarity.First(x => x._rarityType == rarityType)._weaponCharacteristics;
        }

        [Serializable]
        private class WeaponCharacteristicWithRarity
        {
            [ArrayKey]
            public RarityType _rarityType;
            [ArrayWithKey]
            public List<WeaponCharacteristicData> _weaponCharacteristics;
        }
    }
}