using System.Collections.Generic;
using Game.DataBase.Enemies;
using Game.Gameplay.Models.Level;
using Game.Gameplay.Views.UI.Screens.Gameplay;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.TrashArchitecture
{
    /// <summary>
    /// Гад обжект отвечающий за спавн врагов
    /// </summary>
    public class EnemiesSpawner : IInitializeSystem, IUpdateSystem
    {
        private readonly GameScreenView _gameScreenView;
        private readonly SpawnerRepository _spawnerRepository;
        private readonly SpawnerCommandFactory _spawnerCommandFactory;
        private readonly EnemiesSpawnSettingsDataBase _enemiesSpawnSettingsDataBase;
        private readonly LevelInfo _levelInfo;

        public EnemiesSpawner(GameScreenView gameScreenView, SpawnerRepository spawnerRepository, SpawnerCommandFactory spawnerCommandFactory, EnemiesSpawnSettingsDataBase enemiesSpawnSettingsDataBase)
        {
            _gameScreenView = gameScreenView;
            _spawnerRepository = spawnerRepository;
            _spawnerCommandFactory = spawnerCommandFactory;
            _enemiesSpawnSettingsDataBase = enemiesSpawnSettingsDataBase;
        }

        public void Initialize()
        {
            InitializeSpawner();
        }

        private void InitializeSpawner()
        {
            _spawnerRepository.Refresh();
            InitializeGroupsForSpawn();
        }

        public void InitializeGroupsForSpawn()
        {
            _spawnerRepository.CurrentSpawnCommandByQueueIndex.Clear();

            for (var queueIndex = 0; queueIndex < _spawnerRepository.CurrentWave.EnemyQueues.Count; queueIndex++)
            {
                _spawnerRepository.CurrentSpawnCommandByQueueIndex[queueIndex] = new Queue<ISpawnerCommand>();

                foreach (var enemyGroupSpawnSettings in _spawnerRepository.CurrentWave.EnemyQueues[queueIndex].EnemyGroupsSettings)
                {
                    var spawnerCommand = _spawnerCommandFactory.CreateCommand(enemyGroupSpawnSettings, queueIndex);
                    _spawnerRepository.CurrentSpawnCommandByQueueIndex[queueIndex].Enqueue(spawnerCommand);
                }
            }
        }

        private void NextWave()
        {
            _spawnerRepository.CurrentWaveIndex++;

            if (_spawnerRepository.CurrentWaveIndex >= _enemiesSpawnSettingsDataBase.EnemyWaves.Count)
            {
                _spawnerRepository.Win();
                return;
            }

            _gameScreenView.TimeToNextWave.gameObject.SetActive(true);
            _spawnerRepository.SpawnedQueuesCount = 0;
            InitializeGroupsForSpawn();
        }

        private bool AllWaveSpawned()
        {
            return _spawnerRepository.SpawnedQueuesCount == _spawnerRepository.CurrentWave.EnemyQueues.Count;
        }

        private bool AllQueueSpawned(int queueIndex)
        {
            return _spawnerRepository.CurrentSpawnCommandByQueueIndex[queueIndex].Count == 0;
        }

        public void Update()
        {
            if (_spawnerRepository.AllWaveCompleted)
            {
                return;
            }

            if (_spawnerRepository.TimeToNextWave > 0)
            {
                UpdateTimeToNextWave();
                return;
            }

            TrySpawn();

            if (AllWaveSpawned() == false) return;
            
            _spawnerRepository.TimeToNextWave = _spawnerRepository.CurrentWave.PauseTime;
            _gameScreenView.TimeToNextWave.text = _spawnerRepository.TimeToNextWave.ToString("0");

            NextWave();
        }

        private void UpdateTimeToNextWave()
        {
            _gameScreenView.TimeToNextWave.text = _spawnerRepository.TimeToNextWave.ToString("0");
            _spawnerRepository.TimeToNextWave -= Time.deltaTime;

            if (_spawnerRepository.TimeToNextWave > 0) return;

            _gameScreenView.TimeToNextWave.gameObject.SetActive(false);
        }

        private void TrySpawn()
        {
            for (var queueIndex = 0; queueIndex < _spawnerRepository.CurrentWave.EnemyQueues.Count; queueIndex++)
            {
                ExecuteCommands(queueIndex);
            }

            for (var queueIndex = 0; queueIndex < _spawnerRepository.CurrentWave.EnemyQueues.Count; queueIndex++)
            {
                TryCompleteCommands(queueIndex);
            }
        }

        private void ExecuteCommands(int queueIndex)
        {
            if (_spawnerRepository.CurrentSpawnCommandByQueueIndex[queueIndex].Count == 0)
            {
                return;
            }

            var spawnerCommand = _spawnerRepository.CurrentSpawnCommandByQueueIndex[queueIndex].Peek();

            spawnerCommand.Execute();
        }

        private void TryToStartNextSpawnGroup(int queueIndex)
        {
            _spawnerRepository.CurrentSpawnCommandByQueueIndex[queueIndex].Dequeue();

            if (AllQueueSpawned(queueIndex))
            {
                _spawnerRepository.SpawnedQueuesCount++;
            }
        }

        private void TryCompleteCommands(int queueIndex)
        {
            if (_spawnerRepository.CurrentSpawnCommandByQueueIndex[queueIndex].Count == 0)
            {
                return;
            }

            var spawnerCommand = _spawnerRepository.CurrentSpawnCommandByQueueIndex[queueIndex].Peek();

            if (spawnerCommand.IsEnded)
            {
                TryToStartNextSpawnGroup(queueIndex);
            }
        }
    }
}