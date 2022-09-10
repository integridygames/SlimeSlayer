using TegridyCore.Base;

namespace Game.ScriptableObjects.Base
{
    public abstract class Record<TEnum>
    {
        public TEnum _recordType;
        public ViewBase[] _prefabs;
    }
}