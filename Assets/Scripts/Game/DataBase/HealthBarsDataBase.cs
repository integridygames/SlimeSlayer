using Game.Gameplay.Views.UI;
using UnityEngine;

namespace Game.DataBase
{
    [CreateAssetMenu(fileName = "HealthBarsDataBase", menuName = "ScriptableObjects/HealthBarsDataBase")]
    public class HealthBarsDataBase : ScriptableObject
    {
        [SerializeField] private EnemyHealthView _enemyHealthView;

        public EnemyHealthView HealthView => _enemyHealthView;
    }
}