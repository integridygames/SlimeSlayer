using System.Linq;
using Game.Gameplay.Models.Weapon;
using Game.ScriptableObjects.Substructure;
using UnityEngine;

namespace Game.ScriptableObjects.Weapon
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