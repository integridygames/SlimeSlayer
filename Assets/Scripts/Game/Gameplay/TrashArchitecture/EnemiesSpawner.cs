using System.Collections.Generic;
using Game.DataBase.Enemies;
using Game.Gameplay.Factories;
using Game.Gameplay.Models.Level;
using TegridyCore.Base;

namespace Game.Gameplay.TrashArchitecture
{
    /// <summary>
    /// Гад обжект отвечающий за спавн врагов
    /// </summary>
    public class EnemiesSpawner : IInitializeSystem, IUpdateSystem
    {
        private readonly EnemiesSpawnSettingsDataBase _powersSpawnSettingsDataBase;
        private readonly SpawnerCommandFactory _spawnerCommandFactory;
        private readonly LevelInfo _levelInfo;

        private int _currentWaveIndex;

        /// <summary>
        /// По индкесу очереди получаем текущую команду спавнера
        /// </summary>
        private Dictionary<int, Queue<ISpawnerCommand>> _currentSpawnCommandByQueueIndex;

        /// <summary>
        /// Текущая волна
        /// </summary>
        private EnemyWave CurrentWave => _powersSpawnSettingsDataBase.EnemyWaves[_currentWaveIndex];

        public EnemiesSpawner(EnemyFactory enemyFactory, EnemiesSpawnSettingsDataBase powersSpawnSettingsDataBase, SpawnerCommandFactory spawnerCommandFactory)
        {
            _powersSpawnSettingsDataBase = powersSpawnSettingsDataBase;
            _spawnerCommandFactory = spawnerCommandFactory;
        }

        public void Initialize()
        {
            InitializeSpawner();
        }

        private void InitializeSpawner()
        {
            _currentWaveIndex = 0;

            InitializeGroupsForSpawn();
        }

        private void NextWave()
        {
            _currentWaveIndex++;
            InitializeGroupsForSpawn();
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
            TrySpawn();
        }

        private void TrySpawn()
        {
            for (var queueIndex = 0; queueIndex < CurrentWave.EnemyQueues.Count; queueIndex++)
            {
                if (_currentSpawnCommandByQueueIndex[queueIndex].Count == 0)
                {
                    return;
                }
                
                _currentSpawnCommandByQueueIndex[queueIndex].Peek().Execute();
            }
        }

        private void TryToStartNextSpawnGroup(int queueIndex)
        {
            if (_currentSpawnCommandByQueueIndex[queueIndex].Count == 0)
            {
                return;
            }

            _currentSpawnCommandByQueueIndex[queueIndex].Dequeue();
        }
    }
}