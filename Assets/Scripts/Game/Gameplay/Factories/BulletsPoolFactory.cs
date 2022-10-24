using Game.DataBase.Weapon;
using Game.Gameplay.Views.Bullets;
using Game.Gameplay.Views.Container;
using UnityEngine;

namespace Game.Gameplay.Factories 
{
    public class BulletsPoolFactory : PoolFactoryBase<BulletView, BulletType>
    {
        private readonly PoolContainerView _poolContainerView;
        private readonly BulletsDataBase _bulletsDataBase;

        public BulletsPoolFactory(PoolContainerView poolContainerView, BulletsDataBase bulletsDataBase)
        {
            _poolContainerView = poolContainerView;
            _bulletsDataBase = bulletsDataBase;
        }

        protected override BulletView CreateNewElement(BulletType key)
        {
            var weaponRecord = _bulletsDataBase.GetRecordByType(key);

            return Object.Instantiate(weaponRecord._bulletView, _poolContainerView.transform);
        }
    }
}