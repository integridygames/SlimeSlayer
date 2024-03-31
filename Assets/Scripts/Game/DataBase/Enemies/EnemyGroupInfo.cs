using System;
using UnityEngine;

namespace Game.DataBase.Enemies
{
    [Serializable]
    public class EnemyGroupInfo
    {
        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private int _count;
        [SerializeField] private float _spawnDelay;

        public EnemyType EnemyType => _enemyType;

        public int Count => _count;

        public float SpawnDelay => _spawnDelay;
    }
}