using System;
using Game.DataBase.Enemies;
using Game.DataBase.GameResource;
using Game.Gameplay.EnemiesMechanics;
using Game.Gameplay.EnemiesMechanics.Enemies;
using Game.Gameplay.Models.Enemy;
using Game.Gameplay.Models.Level;
using Game.Gameplay.Views.Enemy;
using TegridyUtils.Extensions;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Game.Gameplay.Factories
{
    public class EnemyFactory : IFactory<EnemyType, GameResourceType, EnemyBase>
    {
        private readonly DiContainer _container;
        private readonly EnemyDataBase _enemyDataBase;
        private readonly LevelInfo _levelInfo;
        private readonly ActiveEnemiesContainer _activeEnemiesContainer;

        public EnemyFactory(DiContainer container, EnemyDataBase enemyDataBase, LevelInfo levelInfo, ActiveEnemiesContainer activeEnemiesContainer)
        {
            _container = container;
            _enemyDataBase = enemyDataBase;
            _levelInfo = levelInfo;
            _activeEnemiesContainer = activeEnemiesContainer;
        }

        public EnemyBase Create(EnemyType enemyType, GameResourceType gameResourceType)
        {
            var spawnPoint = _levelInfo.CurrentLevelView.Value.EnemySpawnPoints.GetRandomElement();

            var enemyRecord = _enemyDataBase.GetRecordByType(enemyType);
            var enemyView = Object.Instantiate(enemyRecord._enemyViewBasePrefab, spawnPoint.position, Quaternion.identity, _levelInfo.CurrentLevelView.Value.SpawnRoot);

            return enemyType switch
            {
                EnemyType.Zombie => CreateEnemy<Zombie>(enemyView),
                EnemyType.FireDemon => CreateEnemy<FireDemon>(enemyView),
                _ => throw new ArgumentOutOfRangeException(nameof(enemyType), enemyType, null)
            };
        }

        private T CreateEnemy<T>(EnemyViewBase enemyView) where T : EnemyBase
        {
            var enemyBase = _container.Instantiate<T>(new object[] {enemyView});
            _activeEnemiesContainer.AddEnemy(enemyBase);
            return enemyBase;
        }
    }
}