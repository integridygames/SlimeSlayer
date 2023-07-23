using Game.DataBase.Weapon;
using Game.Gameplay.Models.Character;
using Game.Gameplay.Services;
using Game.Gameplay.Views.Weapons;
using UnityEngine;

namespace Game.Gameplay.WeaponMechanics.Components.ShootComponents
{
    public class GrenadeShootComponent : IShootComponent
    {
        private readonly GrenadeLauncherView _grenadeLauncherView;
        private readonly WeaponMechanicsService _weaponMechanicsService;
        private readonly WeaponsCharacteristicsRepository _weaponsCharacteristicsRepository;

        private readonly ProjectileType _projectileType;
        private readonly PlayerWeaponData _playerWeaponData;
        private readonly Transform _shootingPoint;

        public GrenadeShootComponent(GrenadeLauncherView grenadeLauncherView,
            WeaponMechanicsService weaponMechanicsService, WeaponsCharacteristicsRepository weaponsCharacteristicsRepository,
            ProjectileType projectileType, PlayerWeaponData playerWeaponData, Transform shootingPoint)
        {
            _grenadeLauncherView = grenadeLauncherView;
            _weaponMechanicsService = weaponMechanicsService;
            _weaponsCharacteristicsRepository = weaponsCharacteristicsRepository;
            _projectileType = projectileType;
            _playerWeaponData = playerWeaponData;
            _shootingPoint = shootingPoint;
        }

        public void Shoot(Vector3 direction)
        {
            _grenadeLauncherView.EmitMuzzleFlash();

            var damage = _weaponsCharacteristicsRepository.GetDamage(_playerWeaponData);
            _weaponMechanicsService.ShootGrenade(_shootingPoint.position, direction, _projectileType, damage, true);
        }
    }
}