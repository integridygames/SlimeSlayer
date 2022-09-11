using Game.Gameplay.Utils.Weapons;
using Game.Gameplay.Views.Bullets;
using Game.Gameplay.Views.Container;
using Game.ScriptableObjects;

namespace Game.Gameplay.Factories 
{
    public class BulletsPoolFactory : PoolFactoryBase<WeaponRecord, WeaponType, BulletView>
    {    
        public BulletsPoolFactory(PoolContainerView poolContainerView, WeaponsDataBase dataBase) :
            base(poolContainerView, dataBase) { }        
    }
}