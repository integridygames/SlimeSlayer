using System.Collections.Generic;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Factories
{
    public abstract class PoolFactoryBase<TView, TKey> where TView : ViewBase
    {
        private readonly Stack<TView> _pool = new();

        public void ClearPool()
        {
            foreach (var bulletView in _pool)
            {
                Object.Destroy(bulletView);
            }

            _pool.Clear();
        }

        public TView GetElement(TKey key)
        {
            var nextElement = _pool.Count == 0 ? CreateNewElement(key) : _pool.Pop();

            nextElement.gameObject.SetActive(true);

            return nextElement;
        }

        protected abstract TView CreateNewElement(TKey key);

        public void RecycleElement(TView elementView)
        {
            elementView.gameObject.SetActive(false);

            _pool.Push(elementView);
        }
    }
}