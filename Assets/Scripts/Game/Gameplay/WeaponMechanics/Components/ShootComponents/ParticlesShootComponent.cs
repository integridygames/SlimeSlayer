using Game.DataBase.FX;
using Game.DataBase.Weapon;
using Game.Gameplay.Models.Character;
using Game.Gameplay.Services;
using Game.Gameplay.Views.Weapons;
using UnityEngine;

namespace Game.Gameplay.WeaponMechanics.Components.ShootComponents
{
    public class ParticlesShootComponent : IShootComponent
    {
        private readonly ShotgunView _shotgunView;
        private readonly RecyclableParticleType _particleType;
        private readonly PlayerWeaponData _playerWeaponData;
        private readonly WeaponsCharacteristicsRepository _weaponsCharacteristicsRepository;
        private readonly Transform _shootingPoint;
        private readonly WeaponMechanicsService _weaponMechanicsService;

        public ParticlesShootComponent(ShotgunView shotgunView, RecyclableParticleType particleType,
            PlayerWeaponData playerWeaponData, WeaponsCharacteristicsRepository weaponsCharacteristicsRepository,
            Transform shootingPoint, WeaponMechanicsService weaponMechanicsService)
        {
            _shotgunView = shotgunView;
            _particleType = particleType;
            _playerWeaponData = playerWeaponData;
            _weaponsCharacteristicsRepository = weaponsCharacteristicsRepository;
            _shootingPoint = shootingPoint;
            _weaponMechanicsService = weaponMechanicsService;
        }

        public void Shoot(Vector3 direction)
        {
            _shotgunView.EmitMuzzleFlash();

            var damage = _weaponsCharacteristicsRepository.GetDamage(_playerWeaponData);
            _weaponMechanicsService.ShootFX(_shootingPoint, direction, _particleType, damage);
        }
    }
}