﻿using Game.DataBase.FX;
using Game.DataBase.Weapon;
using Game.Gameplay.EnemiesMechanics;
using Game.Gameplay.Factories;
using Game.Gameplay.Models.Abilities;
using Game.Gameplay.Models.Character;
using Game.Gameplay.Models.Enemy;
using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Utils.Layers;
using Game.Gameplay.Views.Bullets;
using Game.Gameplay.Views.Enemy;
using Game.Gameplay.Views.FX;
using Game.Gameplay.WeaponMechanics;
using TegridyUtils;
using UnityEngine;

namespace Game.Gameplay.Services
{
    public class WeaponMechanicsService
    {
        private const int BulletSpeed = 15;
        private const int ShootingDistance = 10;
        private const int ShootingRangeAngle = 20;

        private readonly BulletsPoolFactory _bulletsPoolFactory;
        private readonly ActiveProjectilesContainer _activeProjectilesContainer;
        private readonly RecyclableParticlesPoolFactory _recyclableParticlesPoolFactory;
        private readonly ActiveEnemiesContainer _activeEnemiesContainer;
        private readonly CharacterCharacteristicsRepository _characterCharacteristicsRepository;

        public WeaponMechanicsService(BulletsPoolFactory bulletsPoolFactory,
            ActiveProjectilesContainer activeProjectilesContainer,
            RecyclableParticlesPoolFactory recyclableParticlesPoolFactory,
            ActiveEnemiesContainer activeEnemiesContainer,
            CharacterCharacteristicsRepository characterCharacteristicsRepository)
        {
            _bulletsPoolFactory = bulletsPoolFactory;
            _activeProjectilesContainer = activeProjectilesContainer;
            _recyclableParticlesPoolFactory = recyclableParticlesPoolFactory;
            _activeEnemiesContainer = activeEnemiesContainer;
            _characterCharacteristicsRepository = characterCharacteristicsRepository;
        }

        public bool TryGetWeaponTarget(Transform shootingPoint, out EnemyBase currentTarget)
        {
            currentTarget = null;

            foreach (var activeEnemy in _activeEnemiesContainer.ActiveEnemies)
            {
                if (MathUtils.IsInCone(shootingPoint.position, shootingPoint.forward, activeEnemy.Position,
                        ShootingDistance, ShootingRangeAngle, true))
                {
                    currentTarget = activeEnemy;

                    return true;
                }
            }

            return false;
        }

        public void ShootBullet(Transform shootingPoint, Vector3 direction, ProjectileType projectileType,
            float damage, bool canBeMultiple = false)
        {
            if (_bulletsPoolFactory.GetElement(projectileType) is BulletView bulletView)
            {
                bulletView.transform.position = shootingPoint.position;

                bulletView.Initialize(direction, damage, BulletSpeed, canBeMultiple);
                bulletView.Shoot();

                bulletView.OnEnemyCollide += OnBulletEnemyCollideHandler;

                _activeProjectilesContainer.AddProjectile(bulletView);
                return;
            }

            Debug.LogError(
                $"{nameof(WeaponMechanicsService)}.{nameof(ShootBullet)} wrong bulletType: {projectileType}");
        }

        private void OnBulletEnemyCollideHandler(BulletView bulletView, EnemyViewBase enemyView)
        {
            bulletView.OnEnemyCollide -= OnBulletEnemyCollideHandler;

            enemyView.InvokeHit(new HitInfo(bulletView.Damage,
                bulletView.Direction,
                enemyView.transform.position));

            RecycleProjectile(bulletView);
        }

        public GrenadeView ShootGrenade(Vector3 shootingPosition, Vector3 direction, ProjectileType projectileType,
            float damage, bool canBeMultiple = false, float force = 1800f)
        {
            if (_bulletsPoolFactory.GetElement(projectileType) is GrenadeView grenadeView)
            {
                grenadeView.transform.position = shootingPosition;

                grenadeView.Initialize(direction, damage, force, canBeMultiple);
                grenadeView.Shoot();

                _activeProjectilesContainer.AddProjectile(grenadeView);

                grenadeView.OnRecycle += OnGrenadeRecycleHandler;

                return grenadeView;
            }

            Debug.LogError(
                $"{nameof(WeaponMechanicsService)}.{nameof(ShootGrenade)} wrong bulletType: {projectileType}");
            return null;
        }

        private void OnGrenadeRecycleHandler(GrenadeView grenadeView)
        {
            grenadeView.OnRecycle -= OnGrenadeRecycleHandler;

            var grenadePosition = grenadeView.transform.position;

            DoExplosion(RecyclableParticleType.GrenadeExplosion, grenadePosition, grenadeView.Damage);

            if (grenadeView.CanBeMultiple &&
                _characterCharacteristicsRepository.TryGetAbilityCharacteristic(
                    AbilityCharacteristicType.MultipleGrenadesCount, out var value))
            {
                var grenadesCount = (int) value;
                for (var i = 1; i <= grenadesCount; i++)
                {
                    var angle = 360 / i;
                    var direction = Quaternion.Euler(0f, angle, 0f) * new Vector3(0, 0.1f, 1);

                    ShootGrenade(grenadeView.transform.position, direction,
                        ProjectileType.Grenade, grenadeView.Damage);
                }
            }
        }

        public void DoExplosion(RecyclableParticleType particleType, Vector3 position, float damage)
        {
            if (_recyclableParticlesPoolFactory.GetElement(particleType) is ExplosionView explosionView)
            {
                explosionView.transform.position = position;

                explosionView.Play();

                explosionView.OnParticleSystemStopped += OnParticleSystemStoppedHandler;

                var explosionTargets = Physics.OverlapSphere(position, 3, (int) Layers.Enemy);

                foreach (var grenadeTarget in explosionTargets)
                {
                    var enemyView = grenadeTarget.GetComponentInParent<EnemyViewBase>();
                    var enemyPosition = enemyView.transform.position;
                    var impulseDirection = GetImpulseDirection(enemyPosition, position);

                    enemyView.InvokeHit(new HitInfo(damage, impulseDirection.normalized,
                        enemyPosition));
                }

                return;
            }

            Debug.LogError(
                $"{nameof(WeaponMechanicsService)}.{nameof(DoExplosion)} wrong particleType: {particleType}");
        }

        public void RecycleProjectile(ProjectileViewBase projectileViewBase)
        {
            if (projectileViewBase.gameObject.activeSelf == false)
            {
                return;
            }

            projectileViewBase.Recycle();
            _activeProjectilesContainer.RemoveProjectile(projectileViewBase);
            _bulletsPoolFactory.RecycleElement(projectileViewBase.ProjectileType, projectileViewBase);
        }

        public void ShootFX(Transform shootingPoint, Vector3 direction, RecyclableParticleType recyclableParticleType,
            float damage)
        {
            if (_recyclableParticlesPoolFactory.GetElement(recyclableParticleType) is CommonShootFxView projectileView)
            {
                projectileView.Initialize(damage);

                var particlesTransform = projectileView.transform;

                particlesTransform.position = shootingPoint.position;
                particlesTransform.rotation = Quaternion.LookRotation(direction);

                projectileView.Play();

                projectileView.OnEnemyCollide += OnFXEnemyCollideHandler;
                projectileView.OnParticleSystemStopped += OnFXEnemyCollideStoppedHandler;
                return;
            }

            Debug.LogError(
                $"{nameof(WeaponMechanicsService)}.{nameof(ShootFX)} wrong particleType: {recyclableParticleType}");
        }

        private void OnFXEnemyCollideHandler(CommonShootFxView commonShootFx, EnemyViewBase enemyView, Vector3 position)
        {
            var enemyPosition = enemyView.transform.position;

            var impulseDirection = GetImpulseDirection(enemyPosition, position);

            enemyView.InvokeHit(new HitInfo(commonShootFx.Damage,
                impulseDirection.normalized, enemyPosition));
        }

        private void OnFXEnemyCollideStoppedHandler(RecyclableParticleView recyclableParticleView)
        {
            if (recyclableParticleView is CommonShootFxView commonShootFxView)
            {
                commonShootFxView.OnEnemyCollide -= OnFXEnemyCollideHandler;
            }

            recyclableParticleView.OnParticleSystemStopped -= OnFXEnemyCollideStoppedHandler;
            _recyclableParticlesPoolFactory.RecycleElement(recyclableParticleView.ParticleType, recyclableParticleView);
        }

        private void OnParticleSystemStoppedHandler(RecyclableParticleView recyclableParticleView)
        {
            recyclableParticleView.OnParticleSystemStopped -= OnParticleSystemStoppedHandler;

            _recyclableParticlesPoolFactory.RecycleElement(recyclableParticleView.ParticleType, recyclableParticleView);
        }

        private static Vector3 GetImpulseDirection(Vector3 targetPosition, Vector3 sourcePosition)
        {
            var impulseDirection = targetPosition - sourcePosition;
            impulseDirection.y = 0;
            return impulseDirection;
        }
    }
}