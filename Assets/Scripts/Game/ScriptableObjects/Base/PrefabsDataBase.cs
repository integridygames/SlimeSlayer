using TegridyUtils.Extensions;
using UnityEngine;

namespace Game.ScriptableObjects.Base 
{
    public abstract class PrefabsDataBase<TRecord, TEnum> : ScriptableObject
    {
        [SerializeField] protected TRecord[] _records;

        public TRecord[] Records => _records;

        public TRecord GetRecordByIndex(int index)
        {
            if (index < _records.Length)
            {
                return _records[index];
            }

            return _records.GetRandomElement();
        }

        public TRecord GetRecordByType(TEnum recordType)
        {
            foreach (TRecord record in _records)
            {
                if (SetCondition(recordType, record))
                {
                    return record;
                }
            }

            return _records.GetRandomElement();
        }

        public abstract bool SetCondition(TEnum recordType, TRecord record);
    }   
}