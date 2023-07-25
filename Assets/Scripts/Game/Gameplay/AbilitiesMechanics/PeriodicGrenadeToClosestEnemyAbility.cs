using Game.DataBase.Weapon;
using Game.Gameplay.EnemiesMechanics;
using Game.Gameplay.Models.Abilities;
using Game.Gameplay.Models.Character;
using Game.Gameplay.Models.Enemy;
using Game.Gameplay.Services;
using Game.Gameplay.Views.Character;
using UnityEngine;

namespace Game.Gameplay.AbilitiesMechanics
{
    public class PeriodicGrenadeToClosestEnemyAbility : AbilityBase
    {
        private readonly CharacterCharacteristicsRepository _characterCharacteristicsRepository;
        private readonly WeaponMechanicsService _weaponMechanicsService;
        private readonly CharacterView _characterView;
        private readonly ActiveEnemiesContainer _activeEnemiesContainer;

        private float _timeToShoot;
        private float _damage;

        public PeriodicGrenadeToClosestEnemyAbility(
            CharacterCharacteristicsRepository characterCharacteristicsRepository,
            WeaponMechanicsService weaponMechanicsService, CharacterView characterView,
            ActiveEnemiesContainer activeEnemiesContainer)
        {
            _characterCharacteristicsRepository = characterCharacteristicsRepository;
            _weaponMechanicsService = weaponMechanicsService;
            _characterView = characterView;
            _activeEnemiesContainer = activeEnemiesContainer;
        }

        public override void OnStart()
        {
            if (_characterCharacteristicsRepository.TryGetAbilityCharacteristic(
                    AbilityCharacteristicType.PeriodicGrenadeToEnemyDuration, out var duration) == false)
            {
                return;
            }

            _timeToShoot = duration;
        }

        public override void Execute()
        {
            if (_timeToShoot <= 0)
            {
                _characterCharacteristicsRepository.TryGetAbilityCharacteristic(
                    AbilityCharacteristicType.PeriodicGrenadeToEnemyDuration, out _timeToShoot);

                _characterCharacteristicsRepository.TryGetAbilityCharacteristic(
                    AbilityCharacteristicType.PeriodicGrenadeToEnemyDamage, out _damage);

                ThrowGrenadeToRandomEnemy();
            }

            _timeToShoot -= Time.deltaTime;
        }

        private void ThrowGrenadeToRandomEnemy()
        {
            var minDistance = float.MaxValue;
            EnemyBase closestEnemy = null;

            foreach (var activeEnemy in _activeEnemiesContainer.ActiveEnemies)
            {
                var sqrMagnitude = (activeEnemy.Position - _characterView.transform.position).sqrMagnitude;

                if (sqrMagnitude < minDistance)
                {
                    minDistance = sqrMagnitude;
                    closestEnemy = activeEnemy;
                }
            }

            if (closestEnemy != null)
            {
                var characterPosition = _characterView.transform.position;
                _weaponMechanicsService.ShootGrenade(characterPosition,  (closestEnemy.Position - characterPosition).normalized + Vector3.up * 0.3f,
                    ProjectileType.Grenade, _damage, true, 900);
            }
        }
    }
}