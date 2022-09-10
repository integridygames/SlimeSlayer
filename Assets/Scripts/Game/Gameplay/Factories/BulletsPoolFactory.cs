using Game.Gameplay.Utils.Weapons;
using Game.Gameplay.Views.Bullets;
using Game.Gameplay.Views.Container;
using Game.ScriptableObjects;

namespace Game.Gameplay.Factories 
{
    public class BulletsPoolFactory : PoolFactoryBase<WeaponRecord, WeaponType, BulletView>
    {    
        private const int BulletRecordID = 1;

        public BulletsPoolFactory(PoolContainerView poolContainerView) : base(poolContainerView) { }
      
        protected override BulletView GetPrefabFromRecord(WeaponRecord record)
        {
            return (BulletView)record._prefabs[BulletRecordID];
        }
    }
}