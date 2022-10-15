using Game.DataBase.Weapon;
using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Views.Bullets;
using Game.Gameplay.Views.Container;
using UnityEngine;

namespace Game.Gameplay.Factories 
{
    public class BulletsPoolFactory : PoolFactoryBase<BulletView, WeaponType>
    {
        private readonly PoolContainerView _poolContainerView;
        private readonly WeaponsDataBase _weaponsDataBase;

        public BulletsPoolFactory(PoolContainerView poolContainerView, WeaponsDataBase weaponsDataBase)
        {
            _poolContainerView = poolContainerView;
            _weaponsDataBase = weaponsDataBase;
        }

        protected override BulletView CreateNewElement(WeaponType key)
        {
            var weaponRecord = _weaponsDataBase.GetRecordByType(key);

            return Object.Instantiate(weaponRecord._bulletView, _poolContainerView.transform);
        }
    }
}