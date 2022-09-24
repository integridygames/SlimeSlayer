using System;
using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Views.Bullets;
using Game.Gameplay.Views.Weapons;

namespace Game.ScriptableObjects.Weapon
{
    [Serializable]
    public class WeaponRecord
    {
        public WeaponType _weaponType;
        public WeaponViewBase _weaponPrefab;
        public BulletView _bulletView;
    }
}