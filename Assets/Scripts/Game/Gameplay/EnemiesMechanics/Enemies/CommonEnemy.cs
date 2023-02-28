using Game.DataBase.Enemies;
using Game.Gameplay.EnemiesMechanics.Components.AttackComponents;
using Game.Gameplay.EnemiesMechanics.Components.DamageComponents;
using Game.Gameplay.EnemiesMechanics.Components.MovementComponents;
using Game.Gameplay.Models.Character;
using Game.Gameplay.Views.Character;
using Game.Gameplay.Views.Enemy;

namespace Game.Gameplay.EnemiesMechanics.Enemies
{
    public sealed class CommonEnemy : EnemyBase
    {
        protected override IEnemyMovementComponent EnemyMovementComponent { get; }
        protected override IEnemyDamageComponent EnemyDamageComponent { get; }
        protected override IEnemyAttackComponent EnemyAttackComponent { get; }

        public CommonEnemy(CommonEnemyView commonEnemyView, CharacterView characterView,
            CharacterHealthData characterHealthData, EnemyDestructionStates enemyDestructionStates) : base(commonEnemyView, enemyDestructionStates)
        {
            EnemyMovementComponent = new SmoothForwardMovementComponent(commonEnemyView.Rigidbody);
            EnemyDamageComponent = new ImpulseFromPositionDamageComponent(commonEnemyView.Rigidbody);
            EnemyAttackComponent = new ImpulseAttackComponent(commonEnemyView, commonEnemyView.Rigidbody, characterView,
                characterHealthData);
        }
    }
}