using System.Collections.Generic;
using System.Linq;
using Game.Gameplay.Views.Zone;
using TegridyUtils.Extensions;
using UnityEngine;

namespace Game.Gameplay.Models.Zone
{
    public class SpawnZoneData
    {
        public const int EnemyRespawnTime = 10;

        private readonly SpawnBoundsView _spawnBoundsView;

        private float _currentTimeout;

        /// <summary>
        /// Спавн работает пока это значение дошло до 1.
        /// </summary>
        public float SpawnProgressNormalized { get; set; }

        /// <summary>
        /// Индекс следующего врага для спавна
        /// </summary>
        public int CurrentSpawnIndex { get; set; }

        /// <summary>
        /// Происходит ли спавн прямо сейчас
        /// </summary>
        public bool SpawnInProgress { get; set; }

        /// <summary>
        /// Общее время спавна
        /// </summary>
        public float SpawnTime => _spawnBoundsView.SpawnTime;

        /// <summary>
        /// Общее кол-во всех врагов для спавна
        /// </summary>
        public int MaxEnemiesCount { get; }

        public IReadOnlyList<EnemySpawnSettingsRecord> BattlefieldSpawnSettings =>
            _spawnBoundsView.EnemySpawnSettings;

        public float CurrentTimeout
        {
            get => _currentTimeout;
            set
            {
                _spawnBoundsView.BattlefieldSpawnTimeoutView.CurrentTimeout = value;
                _currentTimeout = value;
            }
        }

        public SpawnZoneData(SpawnBoundsView spawnBoundsView)
        {
            _spawnBoundsView = spawnBoundsView;

            MaxEnemiesCount = _spawnBoundsView.EnemySpawnSettings.Sum(x => x._count);
        }

        public Vector3 GetRandomPoint()
        {
            return _spawnBoundsView.Bounds.GetRandomPoint();
        }

        public void Recycle()
        {
            SpawnInProgress = false;
            CurrentTimeout = EnemyRespawnTime;
        }
    }
}