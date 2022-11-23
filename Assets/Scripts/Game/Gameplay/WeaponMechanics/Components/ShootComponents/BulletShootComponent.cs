using Game.DataBase.Weapon;
using Game.Gameplay.Services;
using UnityEngine;

namespace Game.Gameplay.WeaponMechanics.Components.ShootComponents
{
    public class BulletShootComponent : IShootComponent
    {
        private readonly WeaponMechanicsService _weaponMechanicsService;

        private readonly ProjectileType _projectileType;
        private readonly WeaponType _weaponType;
        private readonly Transform _shootingPoint;

        public BulletShootComponent(WeaponMechanicsService weaponMechanicsService,
            ProjectileType projectileType,
            WeaponType weaponType, Transform shootingPoint)
        {
            _weaponMechanicsService = weaponMechanicsService;
            _projectileType = projectileType;
            _weaponType = weaponType;
            _shootingPoint = shootingPoint;
        }

        public void Shoot()
        {
            _weaponMechanicsService.ShootBullet(_shootingPoint, _projectileType, _weaponType);
        }
    }
}