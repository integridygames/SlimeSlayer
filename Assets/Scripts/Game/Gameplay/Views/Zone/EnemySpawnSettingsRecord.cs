using System;
using Game.DataBase.Enemies;
using Game.DataBase.Essence;
using Game.DataBase.GameResource;
using UnityEngine;

namespace Game.Gameplay.Views.Zone
{
    [Serializable]
    public class EnemySpawnSettingsRecord
    {
        public EnemyType _enemyType;
        public GameResourceType _gameResourceType;

        public int _count;
    }
}