using System;
using Game.DataBase.Enemies;
using Game.DataBase.GameResource;
using Game.Gameplay.EnemiesMechanics;
using Game.Gameplay.Factories;
using UnityEngine;

namespace Game.Gameplay.TrashArchitecture.Commands
{
    public class EnemySpawnGroupCommand : ISpawnerCommand
    {
        public event Action<ISpawnerCommand> OnEnd;

        private readonly EnemyGroupSpawnSettings _enemyGroupSpawnSettings;
        private readonly EnemyFactory _enemyFactory;
        public int QueueIndex { get; }

        private int _currentSpawnIndex;
        private int _diedEnemiesCount;

        private bool _onDelay;

        private float _delayTimer = 0.0f;

        public EnemySpawnGroupCommand(EnemyGroupSpawnSettings enemyGroupSpawnSettings, int queueIndex, EnemyFactory enemyFactory)
        {
            _enemyGroupSpawnSettings = enemyGroupSpawnSettings;
            _enemyFactory = enemyFactory;
            QueueIndex = queueIndex;

            _currentSpawnIndex = 0;
            StartDelay();
        }

        private void Spawn()
        {
            var enemyBase = _enemyFactory.Create(_enemyGroupSpawnSettings.GroupInfo.EnemyType, GameResourceType.Essence);
            enemyBase.OnEnemyDied += OnEnemyDiedHandler;

            _currentSpawnIndex++;
        }

        private void OnEnemyDiedHandler(EnemyBase enemyBase)
        {
            enemyBase.OnEnemyDied -= OnEnemyDiedHandler;
            _diedEnemiesCount++;

            if (_currentSpawnIndex == _diedEnemiesCount)
            {
                OnEnd?.Invoke(this);
            }
        }

        private void StartDelay()
        {
            _delayTimer = _enemyGroupSpawnSettings.GroupInfo.SpawnDelay;
            _onDelay = true;
        }

        public void Execute()
        {
            if (_currentSpawnIndex >= _enemyGroupSpawnSettings.GroupInfo.Count)
            {
                return;
            }

            if (_onDelay)
            {
                _delayTimer -= Time.deltaTime;
                if (_delayTimer > 0.0f)
                {
                    return;
                }

                _onDelay = false;
            }

            Spawn();
            StartDelay();
        }
    }
}