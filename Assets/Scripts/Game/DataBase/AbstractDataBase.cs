using TegridyUtils.Attributes;
using UnityEngine;

namespace Game.DataBase
{
    public abstract class AbstractDataBase<TRecord, TKey> : ScriptableObject
    {
        [ArrayWithKey]
        [SerializeField] private TRecord[] _records;

        public TRecord[] Records => _records;

        public abstract TRecord GetRecordByType(TKey recordType);
    }   
}