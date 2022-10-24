using System;
using System.Collections.Generic;
using Game.Gameplay.Views.Weapons;

namespace Game.DataBase.Weapon
{
    [Serializable]
    public class WeaponRecord
    {
        public WeaponType _weaponType;
        public WeaponViewBase _weaponPrefab;
        public List<WeaponCharacteristic> _weaponCharacteristics;
    }
}