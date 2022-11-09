using UnityEngine;

namespace Game.Gameplay.EnemiesMechanics.Enemies
{
    public class CommonEnemy : EnemyBase
    {
        private IEnemyMovementComponent _enemyMovementComponent;
        private IEnemyAttackComponent _enemyAttackComponent;
        private IEnemyDamageComponent _enemyDamageComponent;
        private Vector3 _position;

        protected override IEnemyMovementComponent EnemyMovementComponent => _enemyMovementComponent;

        protected override IEnemyAttackComponent EnemyAttackComponent => _enemyAttackComponent;

        protected override IEnemyDamageComponent EnemyDamageComponent => _enemyDamageComponent;

        public override Vector3 Position => _position;
    }
}