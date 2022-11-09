using System;
using Game.DataBase.Enemies;
using Game.DataBase.Essence;
using Game.Gameplay.EnemiesMechanics;
using Game.Gameplay.EnemiesMechanics.Enemies;
using Game.Gameplay.Views.Enemy;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Game.Gameplay.Factories
{
    public class EnemyFactory : IFactory<EnemyType, EssenceType, Vector3, EnemyBase>
    {
        private readonly DiContainer _container;
        private readonly EnemyDataBase _enemyDataBase;

        public EnemyFactory(DiContainer container, EnemyDataBase enemyDataBase)
        {
            _container = container;
            _enemyDataBase = enemyDataBase;
        }

        public EnemyBase Create(EnemyType enemyType, EssenceType essenceType, Vector3 position)
        {
            var enemyRecord = _enemyDataBase.GetRecordByType(enemyType);
            var enemyView = Object.Instantiate(enemyRecord._enemyViewBasePrefab, position, Quaternion.identity);

            enemyView.SetEssenceType(essenceType);

            switch (enemyType)
            {
                case EnemyType.CommonEnemy:
                    return CreateEnemy<CommonEnemy>(enemyView);
                default:
                    throw new ArgumentOutOfRangeException(nameof(enemyType), enemyType, null);
            }
        }

        private T CreateEnemy<T>(EnemyViewBase enemyView) where T : EnemyBase
        {
            return _container.Instantiate<T>(new object[] {enemyView});
        }
    }
}