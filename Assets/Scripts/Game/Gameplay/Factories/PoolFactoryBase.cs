using Game.Gameplay.Views.Container;
using System.Collections.Generic;
using TegridyCore.Base;
using Game.ScriptableObjects.Substructure;
using System;

namespace Game.Gameplay.Factories 
{
    public abstract class PoolFactoryBase<TRecord, TEnum, TView> where TView : ViewBase where TRecord : Record<TEnum, TView> where TEnum : Enum 
    {
        protected readonly PoolContainerView PoolContainerView;
        protected readonly Dictionary<TEnum, Stack<TView>> Pool = new();
        protected readonly PrefabsDataBase<TRecord, TEnum, TView> _dataBase;

        protected PoolFactoryBase(PoolContainerView poolContainerView, PrefabsDataBase<TRecord, TEnum, TView> dataBase)
        {
            PoolContainerView = poolContainerView;
            _dataBase = dataBase;
        }

        public void ClearPool()
        {
            foreach (var bulletList in Pool.Values)
            {
                foreach (var bulletView in bulletList)
                {
                    UnityEngine.Object.Destroy(bulletView);
                }
            }

            Pool.Clear();
        }

        public TView TakeNextElement(TEnum elementType)
        {
            TView nextElement;

            if (Pool.TryGetValue(elementType, out var elementList) == false)
            {
                elementList = new Stack<TView>();

                Pool[elementType] = elementList;
            }

            if (elementList.Count == 0)
            {
                var elementViewPrefab = CreateNewElement(elementType);

                nextElement = UnityEngine.Object.Instantiate(elementViewPrefab, PoolContainerView.transform);
            }
            else
            {
                nextElement = elementList.Pop();
            }

            return nextElement;
        }

        public void RecycleElement(TView elementView, TEnum elementType)
        {
            elementView.Recycle();

            Pool[elementType].Push(elementView);
        }

        protected TView GetPrefabFromRecord(TRecord record) 
        {
            return record._prefab;
        }

        private TView CreateNewElement(TEnum elementType)
        {
            var record = _dataBase.GetRecordByType(elementType);
            return GetPrefabFromRecord(record);
        }     
    }   
}