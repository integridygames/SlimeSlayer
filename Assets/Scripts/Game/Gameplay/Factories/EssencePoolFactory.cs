using Game.DataBase.Essence;
using Game.Gameplay.Views.Container;
using Game.Gameplay.Views.Essence;
using UnityEngine;

namespace Game.Gameplay.Factories 
{
    public class EssencePoolFactory : PoolFactoryBase<EssenceView, EssenceType>
    {
        private readonly PoolContainerView _poolContainerView;
        private readonly EssenceDataBase _essenceDataBase;

        public EssencePoolFactory(PoolContainerView poolContainerView, EssenceDataBase essenceDataBase)
        {
            _poolContainerView = poolContainerView;
            _essenceDataBase = essenceDataBase;
        }

        protected override EssenceView CreateNewElement(EssenceType key)
        {
            var essenceRecord = _essenceDataBase.GetRecordByType(key);

            return Object.Instantiate(essenceRecord._essenceView, _poolContainerView.transform);
        }
    }
}