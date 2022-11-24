using System;
using System.Collections.Generic;
using Game.DataBase.Essence;
using Game.Gameplay.EnemiesMechanics;

namespace Game.Gameplay.Models.Enemy 
{
    public class ActiveEnemiesContainer
    {
        public event Action<EssenceType, EnemyBase> OnEnemyDied;
        public event Action<int> OnLastInZoneEnemyDied;

        private readonly List<EnemyBase> _activeEnemies = new();

        public IReadOnlyList<EnemyBase> ActiveEnemies => _activeEnemies;

        public Dictionary<int, int> EnemiesCountByZoneId = new();

        public void AddEnemy(EnemyBase enemy, int zoneId)
        {
            enemy.Initialize();

            enemy.OnEnemyDied += OnEnemyDied;

            if (EnemiesCountByZoneId.ContainsKey(zoneId) == false)
            {
                EnemiesCountByZoneId[zoneId] = 0;
            }

            EnemiesCountByZoneId[zoneId]++;

            _activeEnemies.Add(enemy);
        }

        public void RemoveEnemy(EnemyBase enemy, int zoneId)
        {
            enemy.Dispose();

            enemy.OnEnemyDied -= OnEnemyDied;

            EnemiesCountByZoneId[zoneId]--;

            if (EnemiesCountByZoneId[zoneId] == 0)
            {
                OnLastInZoneEnemyDied?.Invoke(zoneId);
            }

            _activeEnemies.Remove(enemy);
        }
    }   
}