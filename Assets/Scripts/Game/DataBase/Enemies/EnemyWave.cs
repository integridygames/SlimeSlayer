using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.DataBase.Enemies
{
    [Serializable]
    public class EnemyWave
    {
        [SerializeField] private List<EnemyQueue> _enemyQueues = new();
        [SerializeField] private float _pauseTime;

        /// <summary>
        /// Список очередей.
        /// Каждая очередь независима от остальных.
        /// </summary>
        public List<EnemyQueue> EnemyQueues => _enemyQueues;

        /// <summary>
        /// Пауза после волны.
        /// </summary>
        public float PauseTime => _pauseTime;
    }
}