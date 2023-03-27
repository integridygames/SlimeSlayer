using System.Collections.Generic;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Factories
{
    public abstract class PoolFactoryBase<TView, TKey> where TView : ViewBase
    {
        private readonly Dictionary<TKey, Stack<TView>> _pool = new();

        public void ClearPool()
        {
            foreach (var bulletStack in _pool.Values)
            {
                foreach (var bulletView in bulletStack)
                {
                    Object.Destroy(bulletView);
                }
            }

            _pool.Clear();
        }

        public TView GetElement(TKey key)
        {
            var stackForKey = GetStackForKey(key);

            var nextElement = stackForKey.Count == 0 ? CreateNewElement(key) : stackForKey.Pop();

            nextElement.gameObject.SetActive(true);

            return nextElement;
        }

        private Stack<TView> GetStackForKey(TKey key)
        {
            if (_pool.ContainsKey(key) == false)
            {
                _pool[key] = new Stack<TView>();
            }

            return _pool[key];
        }

        protected abstract TView CreateNewElement(TKey key);

        public void RecycleElement(TKey key, TView elementView)
        {
            elementView.gameObject.SetActive(false);

            OnRecycleInternal(elementView);

            GetStackForKey(key).Push(elementView);
        }

        protected virtual void OnRecycleInternal(TView elementView)
        {
        }
    }
}