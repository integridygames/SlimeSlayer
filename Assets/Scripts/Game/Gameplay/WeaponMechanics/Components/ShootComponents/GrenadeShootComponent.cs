using Game.DataBase.Weapon;
using Game.Gameplay.Services;
using Game.Gameplay.Views.Weapons;
using UnityEngine;

namespace Game.Gameplay.WeaponMechanics.Components.ShootComponents
{
    public class GrenadeShootComponent : IShootComponent
    {
        private readonly GrenadeLauncherView _grenadeLauncherView;
        private readonly WeaponMechanicsService _weaponMechanicsService;

        private readonly ProjectileType _projectileType;
        private readonly PlayerWeaponData _playerWeaponData;
        private readonly Transform _shootingPoint;

        public GrenadeShootComponent(GrenadeLauncherView grenadeLauncherView,
            WeaponMechanicsService weaponMechanicsService,
            ProjectileType projectileType, PlayerWeaponData playerWeaponData, Transform shootingPoint)
        {
            _grenadeLauncherView = grenadeLauncherView;
            _weaponMechanicsService = weaponMechanicsService;
            _projectileType = projectileType;
            _playerWeaponData = playerWeaponData;
            _shootingPoint = shootingPoint;
        }

        public void Shoot(Vector3 direction)
        {
            _grenadeLauncherView.EmitMuzzleFlash();
            _weaponMechanicsService.ShootGrenade(_shootingPoint.position, direction, _projectileType, _playerWeaponData, true);
        }
    }
}