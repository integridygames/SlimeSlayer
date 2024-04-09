using System;
using System.Collections.Generic;
using Game.DataBase.Enemies;
using Game.Gameplay.Views.UI.Screens.Gameplay;

namespace Game.Gameplay.TrashArchitecture
{
    public class SpawnerRepository
    {
        public event Action OnAllWaveCompleted;
        
        private readonly EnemiesSpawnSettingsDataBase _powersSpawnSettingsDataBase;
        private readonly GameScreenView _gameScreenView;
        private readonly SpawnerCommandFactory _spawnerCommandFactory;

        public float TimeToNextWave { get; set; }

        /// <summary>
        /// Текущая волна
        /// </summary>
        public EnemyWave CurrentWave => _powersSpawnSettingsDataBase.EnemyWaves[CurrentWaveIndex];

        public bool AllWaveCompleted { get; private set; }

        /// <summary>
        /// По индкесу очереди получаем текущую команду спавнера
        /// </summary>
        public Dictionary<int, Queue<ISpawnerCommand>> CurrentSpawnCommandByQueueIndex { get; private set; } = new();

        public int CurrentWaveIndex { get; set; }
        public int SpawnedQueuesCount { get; set; }

        public SpawnerRepository(EnemiesSpawnSettingsDataBase powersSpawnSettingsDataBase)
        {
            _powersSpawnSettingsDataBase = powersSpawnSettingsDataBase;
        }

        public void Refresh()
        {
            CurrentWaveIndex = 0;
            SpawnedQueuesCount = 0;
        }

        public void Win()
        {
            AllWaveCompleted = true;
            OnAllWaveCompleted?.Invoke();
        }
    }
}