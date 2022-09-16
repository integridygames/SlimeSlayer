using System;
using TegridyUtils.Extensions;
using UnityEngine;

namespace Game.ScriptableObjects.Substructure
{
    public abstract class PrefabsDataBase<TRecord, TEnum, TView> : ScriptableObject where TRecord : Record<TEnum, TView> where TEnum : Enum
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
                if (Compare(recordType, record))
                {
                    return record;
                }
            }

            return _records.GetRandomElement();
        }

        public bool Compare(TEnum recordType, TRecord record)
        {
            return recordType.CompareTo(record._recordType) == 0;
        }     
    }   
}