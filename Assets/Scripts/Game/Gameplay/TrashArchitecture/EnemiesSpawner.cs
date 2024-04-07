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
        private readonly EnemiesSpawnSettingsDataBase _powersSpawnSettingsDataBase;
        private readonly SpawnerCommandFactory _spawnerCommandFactory;
        private readonly GameScreenView _gameScreenView;
        private readonly LevelInfo _levelInfo;

        private int _currentWaveIndex;
        private int _spawnedQueuesCount;

        public float TimeToNextWave { get; private set; }

        /// <summary>
        /// По индкесу очереди получаем текущую команду спавнера
        /// </summary>
        private Dictionary<int, Queue<ISpawnerCommand>> _currentSpawnCommandByQueueIndex;

        private bool _isWin;

        /// <summary>
        /// Текущая волна
        /// </summary>
        private EnemyWave CurrentWave => _powersSpawnSettingsDataBase.EnemyWaves[_currentWaveIndex];

        public EnemiesSpawner(EnemiesSpawnSettingsDataBase powersSpawnSettingsDataBase, SpawnerCommandFactory spawnerCommandFactory, GameScreenView gameScreenView)
        {
            _powersSpawnSettingsDataBase = powersSpawnSettingsDataBase;
            _spawnerCommandFactory = spawnerCommandFactory;
            _gameScreenView = gameScreenView;
        }

        public void Initialize()
        {
            InitializeSpawner();
        }

        private void InitializeSpawner()
        {
            _currentWaveIndex = 0;
            _spawnedQueuesCount = 0;

            InitializeGroupsForSpawn();
        }

        private void NextWave()
        {
            _currentWaveIndex++;

            if (_currentWaveIndex >= _powersSpawnSettingsDataBase.EnemyWaves.Count)
            {
                Win();
                return;
            }

            _gameScreenView.TimeToNextWave.gameObject.SetActive(true);
            _spawnedQueuesCount = 0;
            InitializeGroupsForSpawn();
        }

        private void Win()
        {
            _isWin = true;
            Debug.Log("Win");
        }

        private void InitializeGroupsForSpawn()
        {
            _currentSpawnCommandByQueueIndex = new Dictionary<int, Queue<ISpawnerCommand>>();

            for (var queueIndex = 0; queueIndex < CurrentWave.EnemyQueues.Count; queueIndex++)
            {
                _currentSpawnCommandByQueueIndex[queueIndex] = new Queue<ISpawnerCommand>();

                foreach (var enemyGroupSpawnSettings in CurrentWave.EnemyQueues[queueIndex].EnemyGroupsSettings)
                {
                    var spawnerCommand = _spawnerCommandFactory.CreateCommand(enemyGroupSpawnSettings, queueIndex);
                    _currentSpawnCommandByQueueIndex[queueIndex].Enqueue(spawnerCommand);
                    spawnerCommand.OnEnd += AllGroupSpawnedHandler;
                }
            }
        }

        private void AllGroupSpawnedHandler(ISpawnerCommand spawnerCommand)
        {
            spawnerCommand.OnEnd -= AllGroupSpawnedHandler;
            TryToStartNextSpawnGroup(spawnerCommand.QueueIndex);
        }

        public void Update()
        {
            if (_isWin)
            {
                return;
            }
            
            if (TimeToNextWave > 0)
            {
                UpdateTimeToNextWave();
                return;
            }

            TrySpawn();
        }

        private void UpdateTimeToNextWave()
        {
            _gameScreenView.TimeToNextWave.text = TimeToNextWave.ToString("0");
            TimeToNextWave -= Time.deltaTime;

            if (TimeToNextWave > 0) return;

            _gameScreenView.TimeToNextWave.gameObject.SetActive(false);
        }

        private void TrySpawn()
        {
            for (var queueIndex = 0; queueIndex < CurrentWave.EnemyQueues.Count; queueIndex++)
            {
                if (_currentSpawnCommandByQueueIndex[queueIndex].Count == 0)
                {
                    continue;
                }
                
                _currentSpawnCommandByQueueIndex[queueIndex].Peek().Execute();
            }
        }

        private void TryToStartNextSpawnGroup(int queueIndex)
        {
            _currentSpawnCommandByQueueIndex[queueIndex].Dequeue();

            if (AllQueueSpawned(queueIndex))
            {
                _spawnedQueuesCount++;

                if (AllWaveSpawned())
                {
                    TimeToNextWave = CurrentWave.PauseTime;
                    _gameScreenView.TimeToNextWave.text = TimeToNextWave.ToString("0");
            
                    NextWave();
                }
            }
        }

        private bool AllWaveSpawned()
        {
            return _spawnedQueuesCount == CurrentWave.EnemyQueues.Count;
        }

        private bool AllQueueSpawned(int queueIndex)
        {
            return _currentSpawnCommandByQueueIndex[queueIndex].Count == 0;
        }
    }
}