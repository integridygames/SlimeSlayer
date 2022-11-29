using Game.DataBase.FX;
using Game.DataBase.Weapon;
using Game.Gameplay.Services;
using Game.Gameplay.Utils.Layers;
using Game.Gameplay.Views.Bullets;
using Game.Gameplay.Views.Enemy;
using UnityEngine;

namespace Game.Gameplay.WeaponMechanics.Components.ShootComponents
{
    public class GrenadeShootComponent : IShootComponent
    {
        private readonly WeaponMechanicsService _weaponMechanicsService;

        private readonly WeaponType _weaponType;
        private readonly ProjectileType _projectileType;
        private readonly Transform _shootingPoint;

        public GrenadeShootComponent(WeaponMechanicsService weaponMechanicsService,
            ProjectileType projectileType, WeaponType weaponType,
            Transform shootingPoint)
        {
            _weaponMechanicsService = weaponMechanicsService;
            _weaponType = weaponType;
            _projectileType = projectileType;
            _shootingPoint = shootingPoint;
        }

        public void Shoot()
        {
            var grenadeView = _weaponMechanicsService.ShootGrenade(_shootingPoint, _projectileType, _weaponType);
            grenadeView.OnRecycle += OnGrenadeRecycleHandler;
        }

        private void OnGrenadeRecycleHandler(GrenadeView grenadeView)
        {
            grenadeView.OnRecycle -= OnGrenadeRecycleHandler;

            var grenadePosition = grenadeView.transform.position;

            _weaponMechanicsService.DoExplosion(RecyclableParticleType.GrenadeExplosion, grenadePosition);
            var grenadeTargets = Physics.OverlapSphere(grenadePosition, 3, (int)Layers.Enemy);

            foreach (var grenadeTarget in grenadeTargets)
            {
                var enemyView = grenadeTarget.GetComponentInParent<EnemyViewBase>();

                if (enemyView == null)
                {
                    return;
                }

                var damage = _weaponMechanicsService.GetDamage(_weaponType);
                var impulseDirection = enemyView.transform.position - grenadePosition;
                impulseDirection.y = 0;

                enemyView.InvokeHit(new HitInfo(damage, impulseDirection.normalized));
            }
        }
    }
}