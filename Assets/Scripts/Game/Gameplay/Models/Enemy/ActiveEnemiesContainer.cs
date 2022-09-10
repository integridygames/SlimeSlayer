using Game.Gameplay.Utils.Essences;
using Game.Gameplay.Views.Enemy;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Models.Enemy 
{
    public class ActiveEnemiesContainer
    {
        public event Action<int, Vector3, EssenceType, EnemyView> OnEnemyDied;

        private readonly List<EnemyView> _activeEnemies = new();

        public IReadOnlyList<EnemyView> ActiveEnemies => _activeEnemies;

        public void AddEnemy(EnemyView enemyView)
        {
            enemyView.OnEnemyDied += OnEnemyDied;

            _activeEnemies.Add(enemyView);
        }

        public void RemoveEnemy(EnemyView enemyView)
        {
            enemyView.OnEnemyDied -= OnEnemyDied;

            _activeEnemies.Remove(enemyView);
        }
    }   
}