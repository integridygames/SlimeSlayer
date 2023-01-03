using System;
using Game.DataBase.Enemies;
using Game.DataBase.Essence;
using UnityEngine;

namespace Game.Gameplay.Views.Zone
{
    [Serializable]
    public class EnemySpawnSettingsRecord
    {
        public EnemyType _enemyType;
        public EssenceType _essenceType;

        public int _count;
    }
}