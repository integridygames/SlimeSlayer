﻿using System;
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
    public class EnemyFactory : IFactory<EnemyType, EssenceType, Vector3, int, EnemyBase>
    {
        private readonly DiContainer _container;
        private readonly EnemyDataBase _enemyDataBase;
        private readonly EssenceDataBase _essenceDataBase;

        public EnemyFactory(DiContainer container, EnemyDataBase enemyDataBase, EssenceDataBase essenceDataBase)
        {
            _container = container;
            _enemyDataBase = enemyDataBase;
            _essenceDataBase = essenceDataBase;
        }

        public EnemyBase Create(EnemyType enemyType, EssenceType essenceType, Vector3 position, int zoneId)
        {
            var enemyRecord = _enemyDataBase.GetRecordByType(enemyType);
            var enemyView = Object.Instantiate(enemyRecord._enemyViewBasePrefab, position, Quaternion.identity);

            var essenceRecord = _essenceDataBase.GetRecordByType(essenceType);
            enemyView.SetEssenceMaterial(essenceRecord._material);

            return enemyType switch
            {
                EnemyType.CommonEnemy => CreateEnemy<CommonEnemy>(essenceRecord._essenceType, enemyView, zoneId),
                _ => throw new ArgumentOutOfRangeException(nameof(enemyType), enemyType, null)
            };
        }

        private T CreateEnemy<T>(EssenceType essenceType, EnemyViewBase enemyView, int zoneId) where T : EnemyBase
        {
            return _container.Instantiate<T>(new object[] {enemyView, essenceType, zoneId});
        }
    }
}