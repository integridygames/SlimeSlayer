using System.Linq;
using UnityEngine;

namespace Game.DataBase.Weapon
{
    [CreateAssetMenu(fileName = "WeaponCardsDataBase", menuName = "ScriptableObjects/WeaponCardsDataBase")]
    public class WeaponCardsDataBase : AbstractDataBase<WeaponCardData, RarityType>
    {
        public override WeaponCardData GetRecordByType(RarityType recordType)
        {
            return Records.First(x => x._rarityType == recordType);
        }
    }
}