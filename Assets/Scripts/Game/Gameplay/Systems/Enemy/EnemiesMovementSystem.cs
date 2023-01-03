using Game.Gameplay.EnemiesMechanics;
using Game.Gameplay.Models.Enemy;
using Game.Gameplay.Models.Zone;
using Game.Gameplay.Views.Character;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Systems.Enemy
{
    public class EnemiesMovementSystem : IFixedUpdateSystem
    {
        private readonly ActiveEnemiesContainer _activeEnemiesContainer;
        private readonly SpawnZonesDataContainer _spawnZonesDataContainer;
        private readonly CharacterView _characterView;

        public EnemiesMovementSystem(ActiveEnemiesContainer activeEnemiesContainer,
            SpawnZonesDataContainer spawnZonesDataContainer, CharacterView characterView)
        {
            _activeEnemiesContainer = activeEnemiesContainer;
            _spawnZonesDataContainer = spawnZonesDataContainer;
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

                var spawnZoneData = _spawnZonesDataContainer.SpawnZonesData[activeEnemy.ZoneId];

                if (IsPlayerNear(activeEnemy))
                {
                    MoveToPlayer(activeEnemy);
                }
                else
                {
                    MoveToRandomPoint(activeEnemy, spawnZoneData);
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

        private static void MoveToRandomPoint(EnemyBase activeEnemy, SpawnZoneData spawnZoneData)
        {
            if (spawnZoneData.InBoundsOfSpawn(activeEnemy.Target) &&
                Vector3.Distance(activeEnemy.Target, activeEnemy.Position) >= 5)
            {
                activeEnemy.UpdateMovement();
                return;
            }

            var randomPoint = spawnZoneData.GetRandomPoint();
            randomPoint.y = activeEnemy.Position.y;

            activeEnemy.Target = randomPoint;
        }
    }
}