using System;
using Game.DataBase.Enemies;
using Game.Gameplay.TrashArchitecture.Commands;
using Zenject;

namespace Game.Gameplay.TrashArchitecture
{
    public class SpawnerCommandFactory
    {
        private readonly DiContainer _container;

        public SpawnerCommandFactory(DiContainer container)
        {
            _container = container;
        }

        public ISpawnerCommand CreateCommand(EnemyGroupSpawnSettings enemyGroupSpawnSettings, int queueIndex)
        {
            return enemyGroupSpawnSettings.QueueType switch
            {
                EnemyQueueType.Enemy => _container.Instantiate<EnemySpawnGroupCommand>(new object[] { enemyGroupSpawnSettings, queueIndex }),
                EnemyQueueType.SyncWait => _container.Instantiate<SyncWaitCommand>(new object[] { enemyGroupSpawnSettings, queueIndex }),
                _ => throw new ArgumentOutOfRangeException(nameof(enemyGroupSpawnSettings.QueueType), enemyGroupSpawnSettings.QueueType, null)
            };
        }
    }
}