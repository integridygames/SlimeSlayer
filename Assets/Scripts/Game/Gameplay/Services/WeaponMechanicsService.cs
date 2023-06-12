using Game.DataBase;
using Game.DataBase.FX;
using Game.DataBase.Weapon;
using Game.Gameplay.EnemiesMechanics;
using Game.Gameplay.Factories;
using Game.Gameplay.Models.Enemy;
using Game.Gameplay.Models.Weapon;
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
        private readonly WeaponsCharacteristics _weaponsCharacteristics;

        public WeaponMechanicsService(BulletsPoolFactory bulletsPoolFactory,
            ActiveProjectilesContainer activeProjectilesContainer,
            RecyclableParticlesPoolFactory recyclableParticlesPoolFactory,
            CurrentCharacterWeaponsData currentCharacterWeaponsData,
            ActiveEnemiesContainer activeEnemiesContainer)
        {
            _bulletsPoolFactory = bulletsPoolFactory;
            _activeProjectilesContainer = activeProjectilesContainer;
            _recyclableParticlesPoolFactory = recyclableParticlesPoolFactory;
            _activeEnemiesContainer = activeEnemiesContainer;
            _weaponsCharacteristics = currentCharacterWeaponsData.WeaponsCharacteristics;
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
            WeaponType weaponType, RarityType rarityType)
        {
            if (_bulletsPoolFactory.GetElement(projectileType) is BulletView bulletView)
            {
                bulletView.transform.position = shootingPoint.position;

                bulletView.Initialize(weaponType, rarityType, direction, BulletSpeed);
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

            enemyView.InvokeHit(new HitInfo(GetDamage(bulletView.WeaponType, bulletView.RarityType),
                bulletView.Direction,
                enemyView.transform.position));

            RecycleProjectile(bulletView);
        }

        public GrenadeView ShootGrenade(Transform shootingPoint, Vector3 direction, ProjectileType projectileType,
            WeaponType weaponType, RarityType rarityType)
        {
            if (_bulletsPoolFactory.GetElement(projectileType) is GrenadeView grenadeView)
            {
                grenadeView.transform.position = shootingPoint.position;

                grenadeView.Initialize(weaponType, rarityType, direction, 1800f);
                grenadeView.Shoot();

                _activeProjectilesContainer.AddProjectile(grenadeView);

                return grenadeView;
            }

            Debug.LogError(
                $"{nameof(WeaponMechanicsService)}.{nameof(ShootGrenade)} wrong bulletType: {projectileType}");
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
            WeaponType weaponType, RarityType rarityType)
        {
            if (_recyclableParticlesPoolFactory.GetElement(recyclableParticleType) is CommonShootFxView projectileView)
            {
                projectileView.Initialize(weaponType, rarityType);

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

            var impulseDirection = enemyPosition - position;
            impulseDirection.y = 0;

            enemyView.InvokeHit(new HitInfo(GetDamage(commonShootFx.WeaponType, commonShootFx.RarityType),
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

        public float GetDamage(WeaponType weaponType, RarityType rarityType)
        {
            return _weaponsCharacteristics.GetCharacteristic(weaponType, rarityType, WeaponCharacteristicType.Attack);
        }
    }
}