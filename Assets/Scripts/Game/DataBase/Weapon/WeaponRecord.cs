using System;
using System.Collections.Generic;
using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Views.Bullets;
using Game.Gameplay.Views.Weapons;

namespace Game.DataBase.Weapon
{
    [Serializable]
    public class WeaponRecord
    {
        public WeaponType _weaponType;
        public WeaponViewBase _weaponPrefab;
        public BulletView _bulletView;
        public List<WeaponCharacteristic> _weaponCharacteristics;
    }
}