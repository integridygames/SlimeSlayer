using Game.DataBase.Weapon;
using TegridyCore;

namespace Game.Gameplay.WeaponMechanics
{
    public abstract class WeaponBase
    {
        public abstract WeaponType WeaponType { get; }

        protected abstract IShootComponent ShootComponent { get; }
        protected abstract IReloadComponent ReloadComponent { get; }
        protected abstract IShootPossibilityComponent ShootPossibilityComponent { get; }

        public IReadonlyRxField<float> ReloadProgress => ReloadComponent.ReloadProgress;

        private float _previousShootTime;

        public void Shoot()
        {
            if (ShootPossibilityComponent.CanShoot())
            {
                ShootComponent.Shoot();
                ShootPossibilityComponent.HandleShoot();
                ReloadComponent.CurrentCharge.Value--;
            }
        }

        public bool NeedReload()
        {
            return ReloadComponent.NeedReload();
        }

        public void Reload()
        {
            ReloadComponent.Reload();
        }
    }
}