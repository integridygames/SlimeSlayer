namespace Game.ScriptableObjects.Substructure
{
    public abstract class Record<TEnum, TView>
    {
        public TEnum _recordType;
        public TView _prefab;
    }
}