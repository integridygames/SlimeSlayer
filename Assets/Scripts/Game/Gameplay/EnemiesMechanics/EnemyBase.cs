using System;
using Game.DataBase.Enemies;
using Game.Gameplay.Models.Character;
using Game.Gameplay.Views.Enemy;
using Game.Gameplay.WeaponMechanics;
using TegridyCore;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Gameplay.EnemiesMechanics
{
    public abstract class EnemyBase
    {
        private const float StartHealth = 100;

        private readonly EnemyViewBase _enemyViewBase;
        private readonly EnemyDestructionStates _enemyDestructionStates;
        private readonly CharacterCharacteristicsRepository _characterCharacteristicsRepository;
        private readonly RxField<float> _health;
        private bool _isDead;

        private int _previousDestructionStateIndex;

        protected abstract IEnemyMovementComponent EnemyMovementComponent { get; }
        protected abstract IEnemyDamageComponent EnemyDamageComponent { get; }
        protected abstract IEnemyAttackComponent EnemyAttackComponent { get; }

        public event Action<EnemyBase> OnEnemyDied;

        public bool IsOnAttack => EnemyAttackComponent.IsOnAttack;
        public bool ReadyToAttack() => EnemyAttackComponent.ReadyToAttack();
        public void BeginAttack() => EnemyAttackComponent.BeginAttack();
        public void ProcessAttack() => EnemyAttackComponent.ProcessAttack();

        public Vector3 Position => EnemyMovementComponent.Position;
        public Vector3 Target
        {
            get => EnemyMovementComponent.Target;
            set => EnemyMovementComponent.Target = value;
        }

        public bool IsInCharacterRange { get; set; }

        protected EnemyBase(EnemyViewBase enemyViewBase, EnemyDestructionStates enemyDestructionStates, CharacterCharacteristicsRepository characterCharacteristicsRepository)
        {
            _enemyViewBase = enemyViewBase;
            _enemyDestructionStates = enemyDestructionStates;
            _characterCharacteristicsRepository = characterCharacteristicsRepository;

            _health = StartHealth;
        }

        public void Initialize()
        {
            _enemyViewBase.OnEnemyHit += OnEnemyHitHandler;
        }

        public void Dispose()
        {
            _enemyViewBase.OnEnemyHit -= OnEnemyHitHandler;
        }

        public void UpdateMovementData()
        {
            EnemyMovementComponent.UpdateMovementData();
        }

        public void UpdateMovement()
        {
            EnemyMovementComponent.UpdateMovement();
        }

        public void Destroy()
        {
            Object.Destroy(_enemyViewBase.gameObject);
        }

        private void OnEnemyHitHandler(HitInfo hitInfo)
        {
            HandleDamage(hitInfo);
        }

        private void HandleDamage(HitInfo hitInfo)
        {
            if (_isDead)
            {
                return;
            }

            _health.Value -= hitInfo.Damage;

            if (_health.Value <= 0)
            {
                _isDead = true;
                OnEnemyDied?.Invoke(this);
                return;
            }

            if (_previousDestructionStateIndex == 0)
            {
                _previousDestructionStateIndex = _enemyDestructionStates.Meshes.Length;
            }

            var healthPercent = _health.Value / StartHealth;
            var destructionStateIndex = GetCurrentModelIndex(healthPercent);
            var looseDestructionStatesCount = _previousDestructionStateIndex - destructionStateIndex;

            _enemyViewBase.MeshFilter.mesh = _enemyDestructionStates.Meshes[destructionStateIndex];

            EnemyDamageComponent.Hit(hitInfo, looseDestructionStatesCount);

            _characterCharacteristicsRepository.HandleHealthStealing();
        }

        private int GetCurrentModelIndex(float healthPercent)
        {
            var statesCount = _enemyDestructionStates.Meshes.Length;

            return statesCount - Mathf.Clamp((int)(healthPercent * statesCount), 1, statesCount);
        }
    }
}