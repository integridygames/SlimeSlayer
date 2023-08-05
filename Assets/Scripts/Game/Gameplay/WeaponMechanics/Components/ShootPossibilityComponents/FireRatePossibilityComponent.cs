using Game.DataBase.Weapon;
using Game.Gameplay.EnemiesMechanics;
using Game.Gameplay.Models.Abilities;
using Game.Gameplay.Models.Character;
using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Services;
using UnityEngine;

namespace Game.Gameplay.WeaponMechanics.Components.ShootPossibilityComponents
{
    public class FireRatePossibilityComponent : IShootPossibilityComponent
    {
        private readonly WeaponsCharacteristics _weaponsCharacteristics;
        private readonly WeaponMechanicsService _weaponMechanicsService;
        private readonly PlayerWeaponData _playerWeaponData;

        private readonly Transform _shootingPoint;
        private readonly CharacterCharacteristicsRepository _characterCharacteristicsRepository;

        private float _lastShotTime;

        public FireRatePossibilityComponent(WeaponsCharacteristics weaponsCharacteristics,
            WeaponMechanicsService weaponMechanicsService, PlayerWeaponData playerWeaponData,
            Transform shootingPoint, CharacterCharacteristicsRepository characterCharacteristicsRepository)
        {
            _weaponsCharacteristics = weaponsCharacteristics;
            _weaponMechanicsService = weaponMechanicsService;
            _playerWeaponData = playerWeaponData;
            _shootingPoint = shootingPoint;
            _characterCharacteristicsRepository = characterCharacteristicsRepository;
        }

        public bool TryToGetTargetCollider(out EnemyBase target)
        {
            if (_weaponMechanicsService.TryGetWeaponTarget(_shootingPoint, out target) == false)
            {
                return false;
            }

            var fireRate = _weaponsCharacteristics.GetCharacteristic(_playerWeaponData, WeaponCharacteristicType.FireRate);
            _characterCharacteristicsRepository.TryGetAbilityCharacteristic(AbilityCharacteristicType.SpeedFireRate,
                out float abilityFireRate);

            fireRate += abilityFireRate;

            return Time.time - _lastShotTime >= BulletsPerSecond(fireRate);
        }

        private static float BulletsPerSecond(float fireRate)
        {
            return 1f / fireRate;
        }

        public void HandleShoot()
        {
            _lastShotTime = Time.time;
        }
    }
}