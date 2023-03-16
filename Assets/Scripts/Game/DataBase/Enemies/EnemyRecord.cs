using System;
using Game.Gameplay.Views.Enemy;

namespace Game.DataBase.Enemies
{
    [Serializable]
    public class EnemyRecord
    {
        public EnemyType _enemyType;
        public EnemyViewBase _enemyViewBasePrefab;
        public EnemyDestructionStates _enemyDestructionStates;
    }
}