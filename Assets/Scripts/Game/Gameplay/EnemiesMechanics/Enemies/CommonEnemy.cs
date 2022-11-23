using Game.DataBase.Essence;
using Game.Gameplay.EnemiesMechanics.Components.AttackComponents;
using Game.Gameplay.EnemiesMechanics.Components.DamageComponents;
using Game.Gameplay.EnemiesMechanics.Components.MovementComponents;
using Game.Gameplay.Views.Enemy;

namespace Game.Gameplay.EnemiesMechanics.Enemies
{
    public class CommonEnemy : EnemyBase
    {
        private readonly CommonEnemyView _commonEnemyView;

        protected override IEnemyMovementComponent EnemyMovementComponent { get; }
        protected override IEnemyDamageComponent EnemyDamageComponent { get; }
        protected override IEnemyAttackComponent EnemyAttackComponent { get; }

        public CommonEnemy(CommonEnemyView commonEnemyView, EssenceType essenceType) : base(commonEnemyView, essenceType)
        {
            _commonEnemyView = commonEnemyView;

            EnemyMovementComponent = new ImpulseMovementComponent(commonEnemyView.Rigidbody);
            EnemyDamageComponent = new ImpulseDamageComponent(commonEnemyView.Rigidbody);
            EnemyAttackComponent = new ImpulseAttackComponent();
        }
    }
}