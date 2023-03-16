using Game.DataBase.FX;
using Game.DataBase.Weapon;
using Game.Gameplay.Services;
using UnityEngine;

namespace Game.Gameplay.WeaponMechanics.Components.ShootComponents
{
    public class ParticlesShootComponent : IShootComponent
    {
        private readonly RecyclableParticleType _particleType;
        private readonly WeaponType _weaponType;
        private readonly Transform _shootingPoint;
        private readonly WeaponMechanicsService _weaponMechanicsService;

        public ParticlesShootComponent(RecyclableParticleType particleType, WeaponType weaponType,
            Transform shootingPoint, WeaponMechanicsService weaponMechanicsService)
        {
            _particleType = particleType;
            _weaponType = weaponType;
            _shootingPoint = shootingPoint;
            _weaponMechanicsService = weaponMechanicsService;
        }

        public void Shoot(Vector3 direction)
        {
            _weaponMechanicsService.ShootFX(_shootingPoint, direction, _particleType, _weaponType);
        }
    }
}