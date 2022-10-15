using System.Linq;
using Game.DataBase.Substructure;
using Game.Gameplay.Models.Weapon;
using UnityEngine;

namespace Game.DataBase.Weapon
{
    [CreateAssetMenu(fileName = "WeaponsDataBase", menuName = "ScriptableObjects/WeaponsDataBase")]
    public class WeaponsDataBase : AbstractDataBase<WeaponRecord, WeaponType>
    {
        public override WeaponRecord GetRecordByType(WeaponType recordType)
        {
            return Records.First(x => x._weaponPrefab);
        }
    }
}