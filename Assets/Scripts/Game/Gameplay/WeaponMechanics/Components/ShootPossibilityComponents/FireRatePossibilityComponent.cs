using Game.DataBase.Weapon;
using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Services;
using UnityEngine;

namespace Game.Gameplay.WeaponMechanics.Components.ShootPossibilityComponents
{
    public class FireRatePossibilityComponent : IShootPossibilityComponent
    {
        private readonly WeaponsCharacteristics _weaponsCharacteristics;
        private readonly WeaponMechanicsService _weaponMechanicsService;

        private readonly WeaponType _weaponType;
        private readonly Transform _shootingPoint;

        private float _lastShotTime;

        public FireRatePossibilityComponent(WeaponsCharacteristics weaponsCharacteristics,
            WeaponMechanicsService weaponMechanicsService, WeaponType weaponType, Transform shootingPoint)
        {
            _weaponsCharacteristics = weaponsCharacteristics;
            _weaponMechanicsService = weaponMechanicsService;
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

        public void HandleShoot()
        {
            _lastShotTime = Time.time;
        }
    }
}