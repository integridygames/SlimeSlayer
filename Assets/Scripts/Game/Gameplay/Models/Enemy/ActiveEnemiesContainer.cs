using System;
using System.Collections.Generic;
using Game.Gameplay.EnemiesMechanics;

namespace Game.Gameplay.Models.Enemy 
{
    public class ActiveEnemiesContainer
    {
        public event Action<EnemyBase> OnEnemyDied;

        private readonly List<EnemyBase> _activeEnemies = new();

        public IReadOnlyList<EnemyBase> ActiveEnemies => _activeEnemies;

        public void AddEnemy(EnemyBase enemy)
        {
            enemy.Initialize();

            enemy.OnEnemyDied += OnEnemyDied;

            _activeEnemies.Add(enemy);
        }

        public void RemoveEnemy(EnemyBase enemy)
        {
            enemy.Dispose();

            enemy.OnEnemyDied -= OnEnemyDied;

            _activeEnemies.Remove(enemy);
        }
    }   
}