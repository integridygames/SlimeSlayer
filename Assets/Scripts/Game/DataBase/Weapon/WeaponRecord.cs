using System;
using System.Collections.Generic;
using Game.Gameplay.Views.Weapons;
using UnityEngine;

namespace Game.DataBase.Weapon
{
    [Serializable]
    public class WeaponRecord
    {
        public WeaponType _weaponType;
        public WeaponViewBase _weaponPrefab;
        public Sprite _weaponSprite;
        public List<WeaponCharacteristicData> _weaponCharacteristics;
    }
}