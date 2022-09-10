using Game.Gameplay.Utils.Essences;
using Game.Gameplay.Views.Container;
using Game.Gameplay.Views.Essence;
using Game.ScriptableObjects;

namespace Game.Gameplay.Factories 
{
    public class EssencePoolFactory : PoolFactoryBase<EssenceRecord, EssenceType, EssenceView>
    {      
        public EssencePoolFactory(PoolContainerView poolContainerView) : base(poolContainerView) { }
           
        protected override EssenceView GetPrefabFromRecord(EssenceRecord record)
        {
            return (EssenceView)record._prefabs[0];
        }
    }
}