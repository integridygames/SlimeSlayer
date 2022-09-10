using Game.Gameplay.Views.Container;
using System.Collections.Generic;
using TegridyCore.Base;
using Game.ScriptableObjects.Base;

namespace Game.Gameplay.Factories 
{
    public abstract class PoolFactoryBase<TRecord, TEnum, TViewBase> where TViewBase : ViewBase
    {
        protected readonly PoolContainerView PoolContainerView;
        protected readonly Dictionary<TEnum, Stack<TViewBase>> Pool = new();

        public PoolFactoryBase(PoolContainerView poolContainerView)
        {
            PoolContainerView = poolContainerView;
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

        public TViewBase TakeNextElement(TEnum elementType, PrefabsDataBase<TRecord, TEnum> dataBase)
        {
            TViewBase nextElement;

            if (Pool.TryGetValue(elementType, out var elementList) == false)
            {
                elementList = new Stack<TViewBase>();

                Pool[elementType] = elementList;
            }

            if (elementList.Count == 0)
            {
                var elementViewPrefab = CreateNewElement(elementType, dataBase);

                nextElement = UnityEngine.Object.Instantiate(elementViewPrefab, PoolContainerView.transform);
            }
            else
            {
                nextElement = elementList.Pop();
            }

            return nextElement;
        }

        public void RecycleElement(TViewBase elementView, TEnum elementType)
        {
            elementView.Recycle();

            Pool[elementType].Push(elementView);
        }

        protected abstract TViewBase GetPrefabFromRecord(TRecord record);

        private TViewBase CreateNewElement(TEnum elementType, PrefabsDataBase<TRecord, TEnum> dataBase)
        {
            TRecord record = dataBase.GetRecordByType(elementType);
            return GetPrefabFromRecord(record);
        }     
    }   
}