using System.Collections.Generic;
using UnityEngine;
using Game.Gameplay.Views.Enemy;
using Game.Gameplay.Views.Zone;

namespace Game.Gameplay.Views.Level
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private ObstacleView[] _obstacleViews;
        [SerializeField] private ZoneView[] _zonesViews;
        [SerializeField] private SpawnPointView _spawnPointView;

        public IReadOnlyCollection<ObstacleView> ObstacleViews => _obstacleViews;
        public IReadOnlyCollection<ZoneView> ZonesViews => _zonesViews;
        public SpawnPointView SpawnPointView => _spawnPointView;

        private void Awake()
        {
            _obstacleViews = GetComponentsInChildren<ObstacleView>();
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}