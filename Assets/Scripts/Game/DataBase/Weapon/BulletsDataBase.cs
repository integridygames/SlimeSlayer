using System.Linq;
using UnityEngine;

namespace Game.DataBase.Weapon
{
    [CreateAssetMenu(fileName = "BulletsDataBase", menuName = "ScriptableObjects/BulletsDataBase")]
    public class BulletsDataBase : AbstractDataBase<BulletRecord, BulletType>
    {
        public override BulletRecord GetRecordByType(BulletType recordType)
        {
            return Records.First(x => x._bulletType == recordType);
        }
    }
}