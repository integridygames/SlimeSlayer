using System;
using Game.Gameplay.Views.Bullets;
using TegridyUtils.Attributes;

namespace Game.DataBase.Weapon
{
    [Serializable]
    public class BulletRecord
    {
        [ArrayKey]
        public ProjectileType _projectileType;
        public ProjectileViewBase _bulletView;
    }
}