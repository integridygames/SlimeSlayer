using Game.Gameplay.Factories;
using Game.Gameplay.Models.Bullets;
using Game.Gameplay.Models.Character.TargetSystem;
using Game.Gameplay.Models.Weapon;
using TegridyUtils;
using UnityEngine;

namespace Game.Gameplay.Services
{
    public class WeaponMechanicsService
    {
        private const int ShootingDistance = 10;
        private const int ShootingRangeAngle = 20;

        private readonly TargetsInfo _targetsInfo;
        private readonly BulletsPoolFactory _bulletsPoolFactory;
        private readonly ActiveBulletsContainer _activeBulletsContainer;

        public WeaponMechanicsService(TargetsInfo targetsInfo, BulletsPoolFactory bulletsPoolFactory, ActiveBulletsContainer activeBulletsContainer)
        {
            _targetsInfo = targetsInfo;
            _bulletsPoolFactory = bulletsPoolFactory;
            _activeBulletsContainer = activeBulletsContainer;
        }

        public bool WeaponHasATarget(Transform shootingPoint)
        {
            foreach (var target in _targetsInfo.Targets)
            {
                if (MathUtils.IsInCone(shootingPoint.position, shootingPoint.forward, target.transform.position, ShootingDistance, ShootingRangeAngle, true))
                {
                    return true;
                }
            }

            return false;
        }

        public void ShootABullet(Transform shootingPoint, WeaponType weaponType, int bulletSpeed)
        {
            var bulletView = _bulletsPoolFactory.GetElement(weaponType);

            bulletView.Rigidbody.position = shootingPoint.position;
            bulletView.Rigidbody.velocity = shootingPoint.forward * bulletSpeed;

            _activeBulletsContainer.AddBullet(bulletView);
        }
    }
}