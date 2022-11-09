using Game.Gameplay.Views.Enemy;
using System;
using System.Collections.Generic;
using Game.DataBase.Essence;
using Game.Gameplay.EnemiesMechanics;
using UnityEngine;

namespace Game.Gameplay.Models.Enemy 
{
    public class ActiveEnemiesContainer
    {
        public event Action<EssenceType, EnemyBase> OnEnemyDied;

        private readonly List<EnemyBase> _activeEnemies = new();

        public IReadOnlyList<EnemyBase> ActiveEnemies => _activeEnemies;

        public void AddEnemy(EnemyBase enemy)
        {
            enemy.OnEnemyDied += OnEnemyDied;

            _activeEnemies.Add(enemy);
        }

        public void RemoveEnemy(EnemyBase enemy)
        {
            enemy.OnEnemyDied -= OnEnemyDied;

            _activeEnemies.Remove(enemy);
        }
    }   
}