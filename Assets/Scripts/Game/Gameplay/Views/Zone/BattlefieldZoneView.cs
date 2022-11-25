using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Views.Zone
{
    public class BattlefieldZoneView : ZoneView
    {
        [SerializeField] private BattlefieldSpawnSettingsRecord[] _battlefieldSpawnSettings;

        [SerializeField] private Bounds _bounds;
        [SerializeField] private float _spawnTime;
        [SerializeField] private BattlefieldSpawnTimeoutView _battlefieldSpawnTimeoutView;

        public IReadOnlyList<BattlefieldSpawnSettingsRecord> BattlefieldSpawnSettings =>
            _battlefieldSpawnSettings;

        public Bounds Bounds => _bounds;
        public float SpawnTime => _spawnTime;

        public BattlefieldSpawnTimeoutView BattlefieldSpawnTimeoutView => _battlefieldSpawnTimeoutView;

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1f, 0.75f, 0.79f, 0.5f);
            Gizmos.DrawCube(_bounds.center, _bounds.size);
        }
    }
}