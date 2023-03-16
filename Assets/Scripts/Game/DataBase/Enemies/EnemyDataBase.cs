using System.Linq;
using UnityEngine;

namespace Game.DataBase.Enemies
{
    [CreateAssetMenu(fileName = "EnemyDataBase", menuName = "ScriptableObjects/EnemyDataBase")]
    public class EnemyDataBase : AbstractDataBase<EnemyRecord, EnemyType>
    {
        public override EnemyRecord GetRecordByType(EnemyType recordType)
        {
            return Records.First(x => x._enemyType == recordType);
        }
    }
}