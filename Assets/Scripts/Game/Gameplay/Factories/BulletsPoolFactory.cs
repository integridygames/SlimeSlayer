using Game.DataBase.Weapon;
using Game.Gameplay.Views.Bullets;
using Game.Gameplay.Views.Pool;
using UnityEngine;

namespace Game.Gameplay.Factories 
{
    public class BulletsPoolFactory : PoolFactoryBase<ProjectileViewBase, ProjectileType>
    {
        private readonly PoolContainerView _poolContainerView;
        private readonly ProjectileDataBase _projectileDataBase;

        public BulletsPoolFactory(PoolContainerView poolContainerView, ProjectileDataBase projectileDataBase)
        {
            _poolContainerView = poolContainerView;
            _projectileDataBase = projectileDataBase;
        }

        protected override ProjectileViewBase CreateNewElement(ProjectileType key)
        {
            var weaponRecord = _projectileDataBase.GetRecordByType(key);

            return Object.Instantiate(weaponRecord._bulletView, _poolContainerView.transform);
        }
    }
}