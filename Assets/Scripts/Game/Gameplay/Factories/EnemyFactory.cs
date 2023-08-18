using System;
using Game.DataBase.Enemies;
using Game.DataBase.GameResource;
using Game.Gameplay.EnemiesMechanics;
using Game.Gameplay.EnemiesMechanics.Enemies;
using Game.Gameplay.Models.Level;
using Game.Gameplay.Views.Enemy;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Game.Gameplay.Factories
{
    public class EnemyFactory : IFactory<EnemyType, GameResourceType, Vector3, EnemyBase>
    {
        private readonly DiContainer _container;
        private readonly EnemyDataBase _enemyDataBase;
        private readonly LevelInfo _levelInfo;

        public EnemyFactory(DiContainer container, EnemyDataBase enemyDataBase, LevelInfo levelInfo)
        {
            _container = container;
            _enemyDataBase = enemyDataBase;
            _levelInfo = levelInfo;
        }

        public EnemyBase Create(EnemyType enemyType, GameResourceType gameResourceType, Vector3 position)
        {
            var enemyRecord = _enemyDataBase.GetRecordByType(enemyType);
            var enemyView = Object.Instantiate(enemyRecord._enemyViewBasePrefab, position, Quaternion.identity, _levelInfo.CurrentLevelView.Value.SpawnRoot);

            return enemyType switch
            {
                EnemyType.CommonEnemy => CreateEnemy<CommonEnemy>(enemyView, enemyRecord._enemyDestructionStates),
                _ => throw new ArgumentOutOfRangeException(nameof(enemyType), enemyType, null)
            };
        }

        private T CreateEnemy<T>(EnemyViewBase enemyView, EnemyDestructionStates enemyDestructionStates) where T : EnemyBase
        {
            return _container.Instantiate<T>(new object[] {enemyView, enemyDestructionStates});
        }
    }
}