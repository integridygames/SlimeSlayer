using UnityEngine;

namespace Game.DataBase
{
    public abstract class AbstractDataBase<TRecord, TKey> : ScriptableObject
    {
        [SerializeField] private TRecord[] _records;

        public TRecord[] Records => _records;

        public abstract TRecord GetRecordByType(TKey recordType);
    }   
}