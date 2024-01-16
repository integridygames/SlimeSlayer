using Game.Gameplay.EnemiesMechanics;
using Game.Gameplay.Models.Enemy;
using Game.Gameplay.Views.Character;
using TegridyCore.Base;

namespace Game.Gameplay.Systems.Enemy
{
    public class EnemiesMovementSystem : IFixedUpdateSystem, IUpdateSystem
    {
        private readonly ActiveEnemiesContainer _activeEnemiesContainer;
        private readonly CharacterView _characterView;

        public EnemiesMovementSystem(ActiveEnemiesContainer activeEnemiesContainer, CharacterView characterView)
        {
            _activeEnemiesContainer = activeEnemiesContainer;
            _characterView = characterView;
        }

        public void Update()
        {
            foreach (var activeEnemy in _activeEnemiesContainer.ActiveEnemies)
            {
                activeEnemy.UpdateMovementData();
            }
        }

        public void FixedUpdate()
        {
            foreach (var activeEnemy in _activeEnemiesContainer.ActiveEnemies)
            {
                MoveToPlayer(activeEnemy);
            }
        }

        private void MoveToPlayer(EnemyBase activeEnemy)
        {
            activeEnemy.Target = _characterView.transform.position;
            activeEnemy.UpdateMovement();
        }
    }
}