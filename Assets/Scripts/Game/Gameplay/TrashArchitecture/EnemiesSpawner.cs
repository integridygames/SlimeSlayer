using System.Collections.Generic;
using System.Linq;
using Game.DataBase.Enemies;
using Game.Gameplay.Factories;
using Game.Gameplay.Models.Enemy;
using Game.Gameplay.Models.Level;
using Game.Gameplay.TrashArchitecture.Commands;
using TegridyCore.Base;

namespace Game.Gameplay.TrashArchitecture
{
    /// <summary>
    /// Гад обжект отвечающий за спавн врагов
    /// </summary>
    public class EnemiesSpawner : IInitializeSystem, IUpdateSystem
    {
        private readonly EnemyFactory _enemyFactory;
        private readonly EnemiesSpawnSettingsDataBase _powersSpawnSettingsDataBase;
        private readonly LevelInfo _levelInfo;

        private int _currentWaveIndex;

        /// <summary>
        /// По индексу очереди получаем индекс группы врагов для спавна внутри этой очереди
        /// </summary>
        private Dictionary<int, int> _currentSpawnGroupIndexByQueueIndex;

        /// <summary>
        /// По индкесу очереди получаем текущую команду спавнера
        /// </summary>
        private Dictionary<int, ISpawnerCommand> _currentSpawnCommandByQueueIndex;

        /// <summary>
        /// Текущая волна
        /// </summary>
        private EnemyWave CurrentWave => _powersSpawnSettingsDataBase.EnemyWaves[_currentWaveIndex];

        public EnemiesSpawner(EnemyFactory enemyFactory, EnemiesSpawnSettingsDataBase powersSpawnSettingsDataBase)
        {
            _enemyFactory = enemyFactory;
            _powersSpawnSettingsDataBase = powersSpawnSettingsDataBase;
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
            _currentSpawnGroupIndexByQueueIndex = new Dictionary<int, int>();
            _currentSpawnCommandByQueueIndex =  new Dictionary<int, ISpawnerCommand>();

            for (var queueIndex = 0; queueIndex < CurrentWave.EnemyQueues.Count; queueIndex++)
            {
                _currentSpawnGroupIndexByQueueIndex[queueIndex] = 0;

                var enemyGroupSpawnSettings = CurrentWave.EnemyQueues[queueIndex].EnemyGroupsSettings.FirstOrDefault();
                _currentSpawnCommandByQueueIndex[queueIndex] = new EnemySpawnGroupCommand(enemyGroupSpawnSettings, _enemyFactory, queueIndex);
                _currentSpawnCommandByQueueIndex[queueIndex].OnEnd += AllGroupSpawnedHandler;
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
                var enemySpawnGroup = _currentSpawnCommandByQueueIndex[queueIndex];
                enemySpawnGroup.Execute();
            }
        }

        private void TryToStartNextSpawnGroup(int queueIndex)
        {
            if (++_currentSpawnGroupIndexByQueueIndex[queueIndex] >= CurrentWave.EnemyQueues[queueIndex].EnemyGroupsSettings.Count)
            {
                return;
            }

            var nextGroupIndex = _currentSpawnGroupIndexByQueueIndex[queueIndex];
            var enemyGroupSpawnSettings = CurrentWave.EnemyQueues[queueIndex].EnemyGroupsSettings[nextGroupIndex];
            _currentSpawnCommandByQueueIndex[queueIndex] = new EnemySpawnGroupCommand(enemyGroupSpawnSettings, _enemyFactory, queueIndex);
        }
    }
}