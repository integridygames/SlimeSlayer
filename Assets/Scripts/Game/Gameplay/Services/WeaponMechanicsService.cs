using System;
using Game.DataBase.Weapon;
using Game.Gameplay.Factories;
using Game.Gameplay.Models.Character.TargetSystem;
using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Views.Bullets;
using Game.Gameplay.Views.Enemy;
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

        public WeaponMechanicsService(TargetsInfo targetsInfo, BulletsPoolFactory bulletsPoolFactory,
            ActiveBulletsContainer activeBulletsContainer)
        {
            _targetsInfo = targetsInfo;
            _bulletsPoolFactory = bulletsPoolFactory;
            _activeBulletsContainer = activeBulletsContainer;
        }

        public bool WeaponHasATarget(Transform shootingPoint)
        {
            foreach (var target in _targetsInfo.Targets)
            {
                if (MathUtils.IsInCone(shootingPoint.position, shootingPoint.forward, target.transform.position,
                        ShootingDistance, ShootingRangeAngle, true))
                {
                    return true;
                }
            }

            return false;
        }

        public void ShootABullet(Transform shootingPoint, BulletType bulletType, int bulletSpeed,
            Action<EnemyView, BulletView> onEnemyCollideHandler)
        {
            var bulletView = _bulletsPoolFactory.GetElement(bulletType);

            bulletView.Rigidbody.position = shootingPoint.position;
            bulletView.Rigidbody.velocity = shootingPoint.forward * bulletSpeed;

            bulletView.SetEnemyCollideHandler(onEnemyCollideHandler);

            _activeBulletsContainer.AddBullet(bulletView);
        }

        public void RecycleBullet(BulletView bulletView)
        {
            _activeBulletsContainer.RemoveBullet(bulletView);
            _bulletsPoolFactory.RecycleElement(bulletView.BulletType, bulletView);
        }
    }
}