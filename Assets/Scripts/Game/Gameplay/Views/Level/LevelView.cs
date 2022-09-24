using System.Collections.Generic;
using UnityEngine;
using Game.Gameplay.Views.Enemy;
using Game.Gameplay.Views.Zone;

namespace Game.Gameplay.Views.Level
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private ObstacleView[] _obstacleViews;
        [SerializeField] private EnemyView[] _enemiesViews;
        [SerializeField] private SpawnPointView _spawnPointView;
        [SerializeField] private GunCabinetView _gunCabinetView;
        [SerializeField] private FinishView _finishView;
        [SerializeField] private ZoneView[] _zonesViews;

        public IReadOnlyCollection<ObstacleView> ObstacleViews => _obstacleViews;
        public IReadOnlyCollection<EnemyView> EnemiesViews => _enemiesViews;
        public SpawnPointView SpawnPointView => _spawnPointView;
        public GunCabinetView GunCabinetView => _gunCabinetView;
        public FinishView FinishView => _finishView;
        public ZoneView[] ZonesViews => _zonesViews;

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