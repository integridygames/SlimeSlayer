using System;
using TegridyUtils.Attributes;
using UnityEngine;

namespace Game.DataBase.Enemies
{
    [Serializable]
    public class EnemyGroupSpawnSettings
    {
        [SerializeField, ArrayKey] private EnemyQueueType _enemyQueueType;

        [SerializeField]
        private EnemyGroupInfo _enemyGroupInfo;

        public EnemyQueueType QueueType => _enemyQueueType;

        public EnemyGroupInfo GroupInfo => _enemyGroupInfo;
    }
}