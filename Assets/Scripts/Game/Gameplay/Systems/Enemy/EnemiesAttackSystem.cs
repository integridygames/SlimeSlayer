using Game.Gameplay.Models.Enemy;
using TegridyCore.Base;

namespace Game.Gameplay.Systems.Enemy
{
    public class EnemiesAttackSystem : IUpdateSystem
    {
        private readonly ActiveEnemiesContainer _activeEnemiesContainer;

        public EnemiesAttackSystem(ActiveEnemiesContainer activeEnemiesContainer)
        {
            _activeEnemiesContainer = activeEnemiesContainer;
        }

        public void Update()
        {
            foreach (var activeEnemy in _activeEnemiesContainer.ActiveEnemies)
            {
                if (activeEnemy.IsOnAttack)
                {
                    activeEnemy.ProcessAttack();
                    continue;
                }

                if (activeEnemy.ReadyToAttack())
                {
                    activeEnemy.BeginAttack();
                }
            }
        }
    }
}