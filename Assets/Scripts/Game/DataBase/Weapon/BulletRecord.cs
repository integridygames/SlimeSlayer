using System;
using Game.Gameplay.Views.Bullets;

namespace Game.DataBase.Weapon
{
    [Serializable]
    public class BulletRecord
    {
        public ProjectileType _projectileType;
        public ProjectileViewBase _bulletView;
    }
}