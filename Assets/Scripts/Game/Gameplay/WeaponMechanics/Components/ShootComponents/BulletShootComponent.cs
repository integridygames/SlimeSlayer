using Game.DataBase;
using Game.DataBase.Weapon;
using Game.Gameplay.Services;
using Game.Gameplay.Views.Weapons;
using UnityEngine;

namespace Game.Gameplay.WeaponMechanics.Components.ShootComponents
{
    public class BulletShootComponent : IShootComponent
    {
        private readonly WeaponViewBase _weaponViewBase;
        private readonly WeaponMechanicsService _weaponMechanicsService;

        private readonly ProjectileType _projectileType;
        private readonly PlayerWeaponData _playerWeaponData;
        private readonly WeaponType _weaponType;
        private readonly RarityType _rarityType;
        private readonly Transform _shootingPoint;

        public BulletShootComponent(WeaponViewBase weaponViewBase, WeaponMechanicsService weaponMechanicsService,
            ProjectileType projectileType, PlayerWeaponData playerWeaponData, Transform shootingPoint)
        {
            _weaponViewBase = weaponViewBase;
            _weaponMechanicsService = weaponMechanicsService;
            _projectileType = projectileType;
            _playerWeaponData = playerWeaponData;
            _shootingPoint = shootingPoint;
        }

        public void Shoot(Vector3 direction)
        {
            _weaponViewBase.EmitMuzzleFlash();
            _weaponMechanicsService.ShootBullet(_shootingPoint, direction, _projectileType, _playerWeaponData);
        }
    }
}