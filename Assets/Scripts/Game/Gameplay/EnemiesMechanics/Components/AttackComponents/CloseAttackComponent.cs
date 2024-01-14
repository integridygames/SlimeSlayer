using System;
using Game.Gameplay.Models.Character;
using Game.Gameplay.Views.Enemy;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.EnemiesMechanics.Components.AttackComponents
{
    public class CloseAttackComponent : IEnemyAttackComponent, IInitializable, IDisposable
    {
        private const float MinDistanceToAttack = 2f;

        private readonly EnemyViewBase _enemyViewBase;
        private readonly Transform _playerTransform;
        private readonly Transform _enemyTransform;
        private readonly CharacterCharacteristicsRepository _characterCharacteristicsRepository;
        private readonly float _damage;

        public CloseAttackComponent(EnemyViewBase enemyViewBase, Transform playerTransform, Transform enemyTransform,
            CharacterCharacteristicsRepository characterCharacteristicsRepository, float damage)
        {
            _enemyViewBase = enemyViewBase;
            _playerTransform = playerTransform;
            _enemyTransform = enemyTransform;
            _characterCharacteristicsRepository = characterCharacteristicsRepository;
            _damage = damage;
        }

        public bool IsOnAttack { get; private set; }

        public bool ReadyToAttack()
        {
            return Vector3.Distance(_playerTransform.position, _enemyTransform.position) <= MinDistanceToAttack;
        }

        public void BeginAttack()
        {
            IsOnAttack = true;

            _enemyViewBase.SetAttackAnimation(true);
        }

        public void ProcessAttack()
        {
        }

        public void Initialize()
        {
            _enemyViewBase.OnEnemyAttackCompleted += OnEnemyAttackCompleted;
            _enemyViewBase.OnEnemyAttack += OnEnemyAttacked;
        }

        public void Dispose()
        {
            _enemyViewBase.OnEnemyAttackCompleted -= OnEnemyAttackCompleted;
            _enemyViewBase.OnEnemyAttack -= OnEnemyAttacked;
        }

        private void OnEnemyAttackCompleted()
        {
            _enemyViewBase.SetAttackAnimation(false);
            IsOnAttack = false;
        }

        private void OnEnemyAttacked()
        {
            _characterCharacteristicsRepository.RemoveHealth(_damage);
        }
    }
}