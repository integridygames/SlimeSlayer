namespace Game.Gameplay.EnemiesMechanics
{
    public interface IEnemyAttackComponent
    {
        bool ReadyToAttack();

        void BeginAttack();

        void ProcessAttack();

        bool IsOnAttack { get; }
    }
}