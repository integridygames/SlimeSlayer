using System.Linq;
using Game.Gameplay.Utils.Essences;
using Game.ScriptableObjects.Substructure;
using UnityEngine;

namespace Game.ScriptableObjects
{
    [CreateAssetMenu(fileName = "EssenceDataBase", menuName = "ScriptableObjects/EssenceDataBase")]
    public class EssenceDataBase : AbstractDataBase<EssenceRecord, EssenceType>
    {
        public override EssenceRecord GetRecordByType(EssenceType recordType)
        {
            return Records.First(x => x._essenceType == recordType);
        }
    }
}