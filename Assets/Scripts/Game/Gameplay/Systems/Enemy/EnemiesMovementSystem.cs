using Game.Gameplay.EnemiesMechanics;
using Game.Gameplay.Models.Enemy;
using TegridyCore.Base;

namespace Game.Gameplay.Systems.Enemy
{
    public class EnemiesMovementSystem : IFixedUpdateSystem, IUpdateSystem
    {
        private readonly ActiveEnemiesContainer _activeEnemiesContainer;

        public EnemiesMovementSystem(ActiveEnemiesContainer activeEnemiesContainer)
        {
            _activeEnemiesContainer = activeEnemiesContainer;
        }

        public void Update()
        {
            foreach (var activeEnemy in _activeEnemiesContainer.ActiveEnemies)
            {
                activeEnemy.UpdateMovement();
            }
        }

        public void FixedUpdate()
        {
            foreach (var activeEnemy in _activeEnemiesContainer.ActiveEnemies)
            {
                MoveToPlayer(activeEnemy);
            }
        }

        private static void MoveToPlayer(EnemyBase activeEnemy)
        {
            activeEnemy.UpdateMovement();
        }
    }
}