using System.Collections.Generic;
using Game.Gameplay.Views.Zone;
using TegridyUtils.Extensions;
using UnityEngine;

namespace Game.Gameplay.Models.Zone
{
    public class BattlefieldZoneData : ZoneData
    {
        public const int EnemyRespawnTime = 10;

        private readonly BattlefieldZoneView _battleFieldZoneView;

        private float _currentTimeout;

        public float SpawnProgressNormalized { get; set; }
        public float CurrentProgressPoint { get; set; }

        public int CurrentSpawnIndex { get; set; }

        public bool SpawnInProgress { get; set; }
        public bool AbleToSpawn { get; set; }

        public float SpawnTime => _battleFieldZoneView.SpawnTime;

        public float CurrentTimeout
        {
            get => _currentTimeout;
            set
            {
                _battleFieldZoneView.BattlefieldSpawnTimeoutView.CurrentTimeout = value;
                _currentTimeout = value;
            }
        }

        public IReadOnlyList<BattlefieldSpawnSettingsRecord> BattlefieldSpawnSettings =>
            _battleFieldZoneView.BattlefieldSpawnSettings;

        public BattlefieldZoneData(ZoneView zoneView) : base(zoneView)
        {
            _battleFieldZoneView = zoneView as BattlefieldZoneView;
        }

        public Vector3 GetRandomPoint()
        {
            return _battleFieldZoneView.Bounds.GetRandomPoint();
        }

        public bool InBoundsOfSpawn(Vector3 position)
        {
            return _battleFieldZoneView.Bounds.Contains(position);
        }

        public void Recycle()
        {
            AbleToSpawn = true;
            CurrentTimeout = EnemyRespawnTime;
        }
    }
}