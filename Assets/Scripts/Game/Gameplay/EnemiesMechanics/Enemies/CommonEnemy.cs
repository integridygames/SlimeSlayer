using Game.Gameplay.Views.Enemy;
using UnityEngine;

namespace Game.Gameplay.EnemiesMechanics.Enemies
{
    public class CommonEnemy : EnemyBase
    {
        private readonly CommonEnemyView _commonEnemyView;

        protected override IEnemyMovementComponent EnemyMovementComponent { get; }
        protected override IEnemyAttackComponent EnemyAttackComponent { get; }
        protected override IEnemyDamageComponent EnemyDamageComponent { get; }

        public override Vector3 Position { get; }

        public CommonEnemy(CommonEnemyView commonEnemyView)
        {
            _commonEnemyView = commonEnemyView;
        }
    }
}