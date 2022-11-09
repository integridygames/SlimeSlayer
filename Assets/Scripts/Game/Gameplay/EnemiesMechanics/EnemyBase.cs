using System;
using Game.DataBase.Essence;
using UnityEngine;

namespace Game.Gameplay.EnemiesMechanics
{
    public abstract class EnemyBase
    {
        public event Action<EssenceType, EnemyBase> OnEnemyDied;

        protected abstract IEnemyMovementComponent EnemyMovementComponent { get; }
        protected abstract IEnemyAttackComponent EnemyAttackComponent { get; }
        protected abstract IEnemyDamageComponent EnemyDamageComponent { get; }

        public abstract Vector3 Position { get; }
    }
}