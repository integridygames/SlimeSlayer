using System;
using Game.Gameplay.Utils.Weapons;
using Game.Gameplay.Views.Bullets;
using Game.Gameplay.Views.Weapons;

namespace Game.ScriptableObjects
{
    [Serializable]
    public class WeaponRecord
    {
        public WeaponType _weaponType;
        public WeaponViewBase _weaponViewPrefab;
        public BulletView _bulletViewPrefab;
    }
}