using System.Collections.Generic;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Views.Zone
{
    public class SpawnBoundsView : ViewBase
    {
        [SerializeField] private EnemySpawnSettingsRecord[] _enemySpawnSettings;
        [SerializeField] private Bounds _bounds;
        [SerializeField] private float _spawnTime;

        public IReadOnlyList<EnemySpawnSettingsRecord> EnemySpawnSettings =>
            _enemySpawnSettings;

        public Bounds Bounds => _bounds;

        public float SpawnTime => _spawnTime;

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1f, 0.75f, 0.79f, 0.5f);
            Gizmos.DrawCube(_bounds.center, _bounds.size);
        }
    }
}