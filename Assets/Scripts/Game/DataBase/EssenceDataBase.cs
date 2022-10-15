using System.Linq;
using Game.DataBase.Substructure;
using UnityEngine;

namespace Game.DataBase
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