using System.Collections.Generic;

namespace Game.Gameplay.Models.GameResources
{
    public abstract class ActiveObjectsContainerBase<T>
    {
        protected readonly List<T> ValuesInternal = new();

        public IReadOnlyList<T> Values => ValuesInternal;

        public void Add(T value)
        {
            ValuesInternal.Add(value);
            OnValueAdded(value);
        }

        public void Remove(T value)
        {
            ValuesInternal.Remove(value);
            OnValueRemoved(value);
        }

        protected virtual void OnValueAdded(T value)
        {
        }

        protected virtual void OnValueRemoved(T value)
        {
        }
    }
}