using System.Linq;
using UnityEngine;

namespace Game.DataBase.Weapon
{
    [CreateAssetMenu(fileName = "WeaponsDataBase", menuName = "ScriptableObjects/WeaponsDataBase")]
    public class WeaponsDataBase : AbstractDataBase<WeaponRecord, WeaponType>
    {
        public override WeaponRecord GetRecordByType(WeaponType recordType)
        {
            return Records.First(x => x._weaponType == recordType);
        }
    }
}