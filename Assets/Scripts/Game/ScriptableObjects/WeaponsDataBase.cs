using UnityEngine;
using Game.ScriptableObjects.Base;
using Game.Gameplay.Utils.Weapons;

namespace Game.ScriptableObjects 
{
    [CreateAssetMenu(fileName = "WeaponsDataBase", menuName = "ScriptableObjects/WeaponsDataBase")]
    public class WeaponsDataBase : PrefabsDataBase<WeaponRecord, WeaponType>
    {
        public override bool SetCondition(WeaponType recordType, WeaponRecord record) 
        {
            return record._recordType == recordType;
        }
    }
}