using Game.DataBase;
using Game.DataBase.FX;
using Game.DataBase.Weapon;
using Game.Gameplay.Services;
using Game.Gameplay.Utils.Layers;
using Game.Gameplay.Views.Bullets;
using Game.Gameplay.Views.Enemy;
using Game.Gameplay.Views.Weapons;
using UnityEngine;

namespace Game.Gameplay.WeaponMechanics.Components.ShootComponents
{
    public class GrenadeShootComponent : IShootComponent
    {
        private readonly GrenadeLauncherView _grenadeLauncherView;
        private readonly WeaponMechanicsService _weaponMechanicsService;

        private readonly WeaponType _weaponType;
        private readonly RarityType _rarityType;
        private readonly ProjectileType _projectileType;
        private readonly Transform _shootingPoint;

        public GrenadeShootComponent(GrenadeLauncherView grenadeLauncherView,
            WeaponMechanicsService weaponMechanicsService,
            ProjectileType projectileType, WeaponType weaponType,
            RarityType rarityType, Transform shootingPoint)
        {
            _grenadeLauncherView = grenadeLauncherView;
            _weaponMechanicsService = weaponMechanicsService;
            _weaponType = weaponType;
            _rarityType = rarityType;
            _projectileType = projectileType;
            _shootingPoint = shootingPoint;
        }

        public void Shoot(Vector3 direction)
        {
            _grenadeLauncherView.EmitMuzzleFlash();

            var grenadeView = _weaponMechanicsService.ShootGrenade(_shootingPoint, direction, _projectileType, _weaponType, _rarityType);
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

                var damage = _weaponMechanicsService.GetDamage(_weaponType, _rarityType);
                var enemyPosition = enemyView.transform.position;

                var impulseDirection = enemyPosition - grenadePosition;
                impulseDirection.y = 0;

                enemyView.InvokeHit(new HitInfo(damage, impulseDirection.normalized, enemyPosition));
            }
        }
    }
}