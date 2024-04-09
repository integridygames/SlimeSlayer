using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Views.Level
{
    public class LevelView : MonoBehaviour
    {
        private SpawnPointView _spawnPointView;
        private FinishView _finishView;

        [SerializeField] private List<Transform> _enemySpawnPoints;
        [SerializeField] private Transform _spawnRoot;

        public SpawnPointView SpawnPointView => _spawnPointView ??= GetComponentInChildren<SpawnPointView>();

        public IReadOnlyList<Transform> EnemySpawnPoints => _enemySpawnPoints;

        public Transform SpawnRoot => _spawnRoot;
    }
}