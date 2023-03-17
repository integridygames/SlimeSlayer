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

        public float SpawnProgressNormalized { get; set; }
        public float CurrentProgressPoint { get; set; }

        public int CurrentSpawnIndex { get; set; }

        public bool SpawnInProgress { get; set; }
        public bool AbleToSpawn { get; set; } = true;

        public float SpawnTime => _spawnBoundsView.SpawnTime;

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

        public bool InBoundsOfSpawn(Vector3 position)
        {
            return _spawnBoundsView.Bounds.Contains(position);
        }

        public void Recycle()
        {
            AbleToSpawn = true;
            CurrentTimeout = EnemyRespawnTime;
        }
    }
}