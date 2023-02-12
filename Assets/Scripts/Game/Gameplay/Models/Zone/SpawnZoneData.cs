using System.Collections.Generic;
using Game.Gameplay.Views.Zone;
using TegridyUtils.Extensions;
using UnityEngine;

namespace Game.Gameplay.Models.Zone
{
    public class SpawnZoneData
    {
        private readonly SpawnBoundsView _spawnBoundsView;

        private float _currentTimeout;

        public float SpawnProgressNormalized { get; set; }
        public float CurrentProgressPoint { get; set; }

        public int CurrentSpawnIndex { get; set; }

        public bool SpawnInProgress { get; set; }
        public bool AbleToSpawn { get; set; } = true;

        public float SpawnTime => _spawnBoundsView.SpawnTime;

        public IReadOnlyList<EnemySpawnSettingsRecord> BattlefieldSpawnSettings =>
            _spawnBoundsView.EnemySpawnSettings;

        public SpawnZoneData(SpawnBoundsView spawnBoundsView)
        {
            _spawnBoundsView = spawnBoundsView;
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
        }
    }
}