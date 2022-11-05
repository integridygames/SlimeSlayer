using Game.DataBase.Weapon;
using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Services;
using UnityEngine;

namespace Game.Gameplay.WeaponMechanic.WeaponComponents.ShootComponents
{
    public class GrenadeShootComponent : IShootComponent
    {
        private readonly WeaponMechanicsService _weaponMechanicsService;
        private readonly WeaponsCharacteristics _weaponsCharacteristics;

        private readonly WeaponType _weaponType;
        private readonly ProjectileType _projectileType;
        private readonly Transform _shootingPoint;

        public GrenadeShootComponent(WeaponMechanicsService weaponMechanicsService,
            WeaponsCharacteristics weaponsCharacteristics, ProjectileType projectileType, WeaponType weaponType,
            Transform shootingPoint)
        {
            _weaponMechanicsService = weaponMechanicsService;
            _weaponsCharacteristics = weaponsCharacteristics;
            _weaponType = weaponType;
            _projectileType = projectileType;
            _shootingPoint = shootingPoint;
        }

        public void Shoot()
        {
            _weaponMechanicsService.ShootGrenade(_shootingPoint, _projectileType);
        }
    }
}