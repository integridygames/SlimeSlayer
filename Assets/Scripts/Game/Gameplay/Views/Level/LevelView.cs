using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Gameplay.Views.Enemy;

namespace Game.Gameplay.Views.Level
{
    public class LevelView : MonoBehaviour
    {
        private ObstacleView[] _obstacleViews;
        private EnemyView[] _enemiesViews;
        public IReadOnlyCollection<ObstacleView> ObstacleViews => _obstacleViews;
        public IReadOnlyCollection<EnemyView> EnemiesViews => _enemiesViews;

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