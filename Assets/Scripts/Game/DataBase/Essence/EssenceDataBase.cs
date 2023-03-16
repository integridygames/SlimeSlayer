using System.Linq;
using UnityEngine;

namespace Game.DataBase.Essence
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