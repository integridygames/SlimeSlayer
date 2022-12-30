using System;
using Game.DataBase.Essence;
using Game.Gameplay.Views.Enemy;
using Game.Gameplay.WeaponMechanics;
using TegridyCore;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Gameplay.EnemiesMechanics
{
    public abstract class EnemyBase
    {
        private readonly EnemyViewBase _enemyViewBase;
        private readonly EssenceType _essenceType;
        private readonly RxField<float> _health = 25;

        protected abstract IEnemyMovementComponent EnemyMovementComponent { get; }
        protected abstract IEnemyDamageComponent EnemyDamageComponent { get; }
        protected abstract IEnemyAttackComponent EnemyAttackComponent { get; }

        public event Action<EssenceType, EnemyBase> OnEnemyDied;
        public int ZoneId { get; }
        public float MaxDistanceToPlayer { get; } = 50f;
        public EnemyViewBase EnemyViewBase => _enemyViewBase;

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

        protected EnemyBase(EnemyViewBase enemyViewBase, EssenceType essenceType, int zoneId)
        {
            _enemyViewBase = enemyViewBase;
            _essenceType = essenceType;
            ZoneId = zoneId;
        }

        public void Initialize()
        {
            _enemyViewBase.OnEnemyHit += OnEnemyHitHandler;
        }

        public void Dispose()
        {
            _enemyViewBase.OnEnemyHit -= OnEnemyHitHandler;
        }

        public void UpdateMovement()
        {
            EnemyMovementComponent.UpdateMovement();
        }

        public void Remove()
        {
            Object.Destroy(_enemyViewBase.gameObject);
        }

        private void OnEnemyHitHandler(HitInfo hitInfo)
        {
            _health.Value -= hitInfo.Damage;

            if (_health.Value <= 0)
            {
                OnEnemyDied?.Invoke(_essenceType, this);
            }

            EnemyDamageComponent.Hit(hitInfo);
        }
    }
}