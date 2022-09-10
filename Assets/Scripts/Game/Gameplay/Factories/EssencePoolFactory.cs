using Game.Gameplay.Utils.Essences;
using Game.Gameplay.Views.Essence;
using Game.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Factories 
{
    public class EssencePoolFactory
    {
        private readonly EssenceContainerView _essenceContainerView;
        private readonly EssenceDataBase _essenceDataBase;

        private readonly Dictionary<EssenceType, Stack<EssenceView>> _pool = new();

        public EssencePoolFactory(EssenceContainerView essenceContainerView, EssenceDataBase essenceDataBase) 
        {
            _essenceContainerView = essenceContainerView;
            _essenceDataBase = essenceDataBase;
        }

       public void ClearPool() 
        {
            foreach (var essenceList in _pool.Values)
            {
                foreach (var essenceView in essenceList)
                {
                    Object.Destroy(essenceView);
                }
            }
            
            _pool.Clear();
        }

        public EssenceView TakeNextEssence(EssenceType essenceType)
        {
            EssenceView nextEssence;

            if (_pool.TryGetValue(essenceType, out var essenceList) == false)
            {
                essenceList = new Stack<EssenceView>();

                _pool[essenceType] = essenceList;
            }

            if (essenceList.Count == 0)
            {
                var essenceViewPrefab = CreateNewEssence(essenceType);

                nextEssence = Object.Instantiate(essenceViewPrefab, _essenceContainerView.transform);
            }
            else
            {
                nextEssence = essenceList.Pop();
            }

            return nextEssence;
        }

        public void RecycleEssence(EssenceView essenceView)
        {
            essenceView.Recycle();

            _pool[essenceView.EssenceType].Push(essenceView);
        }

        private EssenceView CreateNewEssence(EssenceType essenceType)
        {
            return _essenceDataBase.GetEssenceRecordByType(essenceType).EssenceViewPrefab;
        }
    }
}