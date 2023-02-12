using Game.Gameplay.EnemiesMechanics;
using Game.Gameplay.Models.Enemy;
using Game.Gameplay.Views.Character;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Systems.Enemy
{
    public class EnemiesMovementSystem : IFixedUpdateSystem
    {
        private readonly ActiveEnemiesContainer _activeEnemiesContainer;
        private readonly CharacterView _characterView;

        public EnemiesMovementSystem(ActiveEnemiesContainer activeEnemiesContainer, CharacterView characterView)
        {
            _activeEnemiesContainer = activeEnemiesContainer;
            _characterView = characterView;
        }

        public void FixedUpdate()
        {
            foreach (var activeEnemy in _activeEnemiesContainer.ActiveEnemies)
            {
                if (activeEnemy.IsOnAttack)
                {
                    continue;
                }

                if (IsPlayerNear(activeEnemy))
                {
                    MoveToPlayer(activeEnemy);
                }
            }
        }

        private bool IsPlayerNear(EnemyBase activeEnemy)
        {
            return Vector3.Distance(activeEnemy.Position, _characterView.transform.position) < 10;
        }

        private void MoveToPlayer(EnemyBase activeEnemy)
        {
            activeEnemy.Target = _characterView.transform.position;
            activeEnemy.UpdateMovement();
        }
    }
}