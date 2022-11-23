using System;
using Game.DataBase.Essence;
using Game.Gameplay.Views.Enemy;
using TegridyCore;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Gameplay.EnemiesMechanics
{
    public abstract class EnemyBase
    {
        private readonly EnemyViewBase _enemyViewBase;
        private readonly EssenceType _essenceType;
        private readonly RxField<float> _health = 5;

        protected abstract IEnemyMovementComponent EnemyMovementComponent { get; }
        protected abstract IEnemyDamageComponent EnemyDamageComponent { get; }
        protected abstract IEnemyAttackComponent EnemyAttackComponent { get; }

        public Vector3 Position => EnemyMovementComponent.Position;
        public IReadonlyRxField<float> Health => _health;

        public event Action<EssenceType, EnemyBase> OnEnemyDied;

        protected EnemyBase(EnemyViewBase enemyViewBase, EssenceType essenceType)
        {
            _enemyViewBase = enemyViewBase;
            _essenceType = essenceType;
        }

        public void Initialize()
        {
            _enemyViewBase.OnEnemyHit += OnEnemyHitHandler;
        }

        public void Dispose()
        {
            _enemyViewBase.OnEnemyHit -= OnEnemyHitHandler;
        }

        public void Remove()
        {
            Object.Destroy(_enemyViewBase.gameObject);
        }

        private void OnEnemyHitHandler(Vector3 hitPosition, float damage)
        {
            _health.Value -= damage;

            if (_health.Value <= 0)
            {
                OnEnemyDied?.Invoke(_essenceType, this);
            }

            EnemyDamageComponent.Hit(Position - hitPosition);
        }
    }
}