using System;
using System.Collections.Generic;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Views.Zone
{
    public class SpawnBoundsView : ViewBase
    {
        [SerializeField] private EnemySpawnSettingsRecord[] _enemySpawnSettings;
        [SerializeField] private Vector3 _boundsSize;
        [SerializeField] private float _spawnTime;
        [SerializeField] private BattlefieldSpawnTimeoutView _battlefieldSpawnTimeoutView;

        private Bounds _bounds;

        public IReadOnlyList<EnemySpawnSettingsRecord> EnemySpawnSettings =>
            _enemySpawnSettings;

        public BattlefieldSpawnTimeoutView BattlefieldSpawnTimeoutView => _battlefieldSpawnTimeoutView;

        public Bounds Bounds => _bounds;

        public float SpawnTime => _spawnTime;

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1f, 0.75f, 0.79f, 0.5f);

            _bounds = new Bounds(transform.position, _boundsSize);

            Gizmos.DrawCube(_bounds.center, _bounds.size);
        }

        private void Start()
        {
            _bounds = new Bounds(transform.position, _boundsSize);
        }
    }
}