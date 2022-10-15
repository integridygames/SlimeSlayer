using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Services;
using UnityEngine;

namespace Game.Gameplay.WeaponMechanic.WeaponComponents.ShootComponents
{
    public class BulletShootComponent : IShootComponent
    {
        private const int BulletSpeed = 15;

        private readonly WeaponMechanicsService _weaponMechanicsService;
        private readonly WeaponsCharacteristics _weaponsCharacteristics;
        private readonly WeaponType _weaponType;
        private readonly Transform _shootingPoint;

        private float _lastShotTime;

        public BulletShootComponent(WeaponMechanicsService weaponMechanicsService,
            WeaponsCharacteristics weaponsCharacteristics, WeaponType weaponType,
            Transform shootingPoint)
        {
            _weaponMechanicsService = weaponMechanicsService;
            _weaponsCharacteristics = weaponsCharacteristics;
            _weaponType = weaponType;
            _shootingPoint = shootingPoint;
        }

        public bool CanShoot()
        {
            if (_weaponMechanicsService.WeaponHasATarget(_shootingPoint) == false)
            {
                return false;
            }

            var fireRate = _weaponsCharacteristics.GetCharacteristic(_weaponType, WeaponCharacteristicType.FireRate);

            return Time.time - _lastShotTime >= BulletsPerSecond(fireRate);
        }

        private static float BulletsPerSecond(float fireRate)
        {
            return 1f / fireRate;
        }

        public void Shoot()
        {
            _weaponMechanicsService.ShootABullet(_shootingPoint, _weaponType, BulletSpeed);

            _lastShotTime = Time.time;
        }
    }
}