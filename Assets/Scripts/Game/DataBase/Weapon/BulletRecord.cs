using System;
using Game.Gameplay.Views.Bullets;

namespace Game.DataBase.Weapon
{
    [Serializable]
    public class BulletRecord
    {
        public BulletType _bulletType;
        public BulletView _bulletView;
    }
}