using System;
using Game.Gameplay.Views.Enemy;
using TegridyUtils.Attributes;

namespace Game.DataBase.Enemies
{
    [Serializable]
    public class EnemyRecord
    {
        [ArrayKey]
        public EnemyType _enemyType;
        public EnemyViewBase _enemyViewBasePrefab;
    }
}