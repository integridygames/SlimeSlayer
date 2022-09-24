using UnityEngine;

namespace Game.ScriptableObjects.Substructure
{
    public abstract class PrefabsDataBase<TRecord, TKey> : ScriptableObject
    {
        [SerializeField] private TRecord[] _records;

        protected TRecord[] Records => _records;

        public abstract TRecord GetRecordByType(TKey recordType);
    }   
}