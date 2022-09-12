using Game.Gameplay.Utils.Essences;
using Game.Gameplay.Views.Container;
using Game.Gameplay.Views.Essence;
using Game.ScriptableObjects;

namespace Game.Gameplay.Factories 
{
    public class EssencePoolFactory : PoolFactoryBase<EssenceRecord, EssenceType, EssenceView>
    {      
        public EssencePoolFactory(PoolContainerView poolContainerView, EssenceDataBase dataBase) : 
            base(poolContainerView, dataBase) { }
    }
}