namespace Game.Gameplay.EnemiesMechanics.Components.AttackComponents
{
    public class ImpulseAttackComponent : IEnemyAttackComponent
    {
        public bool ReadyToAttack()
        {
            return false;
        }

        public void BeginAttack()
        {

        }

        public void ProcessAttack()
        {

        }

        public bool IsOnAttack { get; private set; }
    }
}