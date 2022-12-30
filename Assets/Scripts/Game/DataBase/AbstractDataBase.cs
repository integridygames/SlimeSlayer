using UnityEngine;

namespace Game.DataBase
{
    public abstract class AbstractDataBase<TRecord, TKey> : ScriptableObject
    {
        [SerializeField] private TRecord[] _records;

        public TRecord[] Records => _records;

        public abstract TRecord GetRecordByType(TKey recordType);

        public TRecord GetRandomRecord() 
        {
            int randomIndex = Random.Range(0, _records.Length);

            return _records[randomIndex];
        }
    }   
}