using System.Linq;
using UnityEngine;

namespace Game.DataBase.Weapon
{
    [CreateAssetMenu(fileName = "ProjectileDataBase", menuName = "ScriptableObjects/ProjectileDataBase")]
    public class ProjectileDataBase : AbstractDataBase<BulletRecord, ProjectileType>
    {
        public override BulletRecord GetRecordByType(ProjectileType recordType)
        {
            return Records.First(x => x._projectileType == recordType);
        }
    }
}