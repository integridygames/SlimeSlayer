using System;
using Game.DataBase.FX;
using Game.DataBase.Weapon;
using Game.Gameplay.Factories;
using Game.Gameplay.Models.Character.TargetSystem;
using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Views.Bullets;
using Game.Gameplay.Views.Enemy;
using Game.Gameplay.Views.FX;
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
        private readonly ActiveProjectilesContainer _activeProjectilesContainer;
        private readonly RecyclableParticlesPoolFactory _recyclableParticlesPoolFactory;

        public WeaponMechanicsService(TargetsInfo targetsInfo, BulletsPoolFactory bulletsPoolFactory,
            ActiveProjectilesContainer activeProjectilesContainer, RecyclableParticlesPoolFactory recyclableParticlesPoolFactory)
        {
            _targetsInfo = targetsInfo;
            _bulletsPoolFactory = bulletsPoolFactory;
            _activeProjectilesContainer = activeProjectilesContainer;
            _recyclableParticlesPoolFactory = recyclableParticlesPoolFactory;
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

        public void ShootBullet(Transform shootingPoint, ProjectileType projectileType, int bulletSpeed,
            Action<EnemyViewBase, BulletView> onEnemyCollideHandler)
        {
            if (_bulletsPoolFactory.GetElement(projectileType) is BulletView bulletView)
            {
                bulletView.transform.position = shootingPoint.position;
                bulletView.Rigidbody.velocity = shootingPoint.forward * bulletSpeed;

                bulletView.SetEnemyCollideHandler(onEnemyCollideHandler);

                _activeProjectilesContainer.AddProjectile(bulletView);
                return;
            }

            Debug.LogError($"{nameof(WeaponMechanicsService)}.{nameof(ShootBullet)} wrong bulletType: {projectileType}");
        }

        public GrenadeView ShootGrenade(Transform shootingPoint, ProjectileType projectileType)
        {
            if (_bulletsPoolFactory.GetElement(projectileType) is GrenadeView grenadeView)
            {
                grenadeView.transform.position = shootingPoint.position;
                grenadeView.Rigidbody.AddForce(shootingPoint.forward * 1400f);

                _activeProjectilesContainer.AddProjectile(grenadeView);

                return grenadeView;
            }

            Debug.LogError($"{nameof(WeaponMechanicsService)}.{nameof(ShootGrenade)} wrong bulletType: {projectileType}");
            return null;
        }

        public void DoExplosion(RecyclableParticleType particleType, Vector3 position)
        {
            if (_recyclableParticlesPoolFactory.GetElement(particleType) is ExplosionView explosionView)
            {
                explosionView.transform.position = position;

                explosionView.Play();

                explosionView.OnParticleSystemStopped += OnParticleSystemStoppedHandler;
                return;
            }

            Debug.LogError($"{nameof(WeaponMechanicsService)}.{nameof(DoExplosion)} wrong particleType: {particleType}");
        }

        public void RecycleProjectile(ProjectileViewBase projectileViewBase)
        {
            projectileViewBase.Recycle();
            _activeProjectilesContainer.RemoveProjectile(projectileViewBase);
            _bulletsPoolFactory.RecycleElement(projectileViewBase.ProjectileType, projectileViewBase);
        }

        public void ShootFX(RecyclableParticleType recyclableParticleType, Transform shootingPoint, Action<EnemyViewBase> enemyCollideHandler)
        {
            if (_recyclableParticlesPoolFactory.GetElement(recyclableParticleType) is CommonShootFxView projectileView)
            {
                var particlesTransform = projectileView.transform;

                particlesTransform.position = shootingPoint.position;
                particlesTransform.rotation = shootingPoint.rotation;

                projectileView.Play();
                projectileView.SetEnemyCollideHandler(enemyCollideHandler);

                projectileView.OnParticleSystemStopped += OnParticleSystemStoppedHandler;
                return;
            }

            Debug.LogError($"{nameof(WeaponMechanicsService)}.{nameof(ShootFX)} wrong particleType: {recyclableParticleType}");
        }

        private void OnParticleSystemStoppedHandler(RecyclableParticleView recyclableParticleView)
        {
            recyclableParticleView.OnParticleSystemStopped -= OnParticleSystemStoppedHandler;

            _recyclableParticlesPoolFactory.RecycleElement(recyclableParticleView.ParticleType, recyclableParticleView);
        }
    }
}