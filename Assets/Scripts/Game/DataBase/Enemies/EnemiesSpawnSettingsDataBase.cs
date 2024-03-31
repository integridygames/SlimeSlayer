using System.Collections.Generic;
using UnityEngine;

namespace Game.DataBase.Enemies
{
    [CreateAssetMenu(fileName = "EnemiesSpawnSettingsDataBase", menuName = "ScriptableObjects/EnemiesSpawnSettingsDataBase")]
    public class EnemiesSpawnSettingsDataBase : ScriptableObject
    {
        [SerializeField] private List<EnemyWave> _enemyWaves = new();

        public List<EnemyWave> EnemyWaves => _enemyWaves;
    }
}