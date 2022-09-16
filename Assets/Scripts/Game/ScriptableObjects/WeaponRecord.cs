using System;
using Game.Gameplay.Utils.Weapons;
using Game.Gameplay.Views.Bullets;
using Game.Gameplay.Views.Weapons;
using Game.ScriptableObjects.Substructure;

namespace Game.ScriptableObjects
{
    [Serializable]
    public class WeaponRecord : Record<WeaponType, BulletView>
    {
        public WeaponViewBase _weaponPrefab;
    }
}