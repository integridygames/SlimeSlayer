using Game.Gameplay.Utils.Essences;
using Game.ScriptableObjects.Base;
using UnityEngine;

namespace Game.ScriptableObjects 
{
    [CreateAssetMenu(fileName = "EssenceDataBase", menuName = "ScriptableObjects/EssenceDataBase")]
    public class EssenceDataBase : PrefabsDataBase<EssenceRecord, EssenceType>
    {      
        public override bool SetCondition(EssenceType recordType, EssenceRecord record)
        {
            return record._recordType == recordType;
        }
    }
}