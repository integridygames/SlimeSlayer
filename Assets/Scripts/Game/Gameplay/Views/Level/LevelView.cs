using System.Collections.Generic;
using UnityEngine;
using Game.Gameplay.Views.Enemy;

namespace Game.Gameplay.Views.Level
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private ObstacleView[] _obstacleViews;
        [SerializeField] private EnemyView[] _enemiesViews;
        [SerializeField] private SpawnPointView _spawnPointView;

        public IReadOnlyCollection<ObstacleView> ObstacleViews => _obstacleViews;
        public IReadOnlyCollection<EnemyView> EnemiesViews => _enemiesViews;
        public SpawnPointView SpawnPointView => _spawnPointView;

        private void Awake()
        {
            _obstacleViews = GetComponentsInChildren<ObstacleView>();
            _enemiesViews = GetComponentsInChildren<EnemyView>();
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}