using System.Linq;
using Game.Gameplay.Models.Abilities;
using UnityEngine;

namespace Game.DataBase.Abilities
{
    [CreateAssetMenu(fileName = "AbilitiesDataBase", menuName = "ScriptableObjects/AbilitiesDataBase")]
    public class AbilitiesDataBase : AbstractDataBase<AbilityRecord, AbilityType>
    {
        public override AbilityRecord GetRecordByType(AbilityType recordType)
        {
            return Records.First(x => x.AbilityType == recordType);
        }
    }
}