using System;
using Game.DataBase.Enemies;
using Game.Gameplay.Models.Enemy;
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

        public ISpawnerCommand CreateCommand(EnemyQueueType queueType)
        {
            return queueType switch
            {
                EnemyQueueType.Enemy => _container.Resolve<EnemySpawnGroupCommand>(),
                EnemyQueueType.SyncWait => _container.Resolve<SyncWaitCommand>(),
                _ => throw new ArgumentOutOfRangeException(nameof(queueType), queueType, null)
            };
        }
    }
}