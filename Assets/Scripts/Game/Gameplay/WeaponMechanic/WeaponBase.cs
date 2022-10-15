using Game.Gameplay.Models.Weapon;
using TegridyCore;

namespace Game.Gameplay.WeaponMechanic
{
    public abstract class WeaponBase
    {
        public abstract WeaponType WeaponType { get; }

        protected abstract IShootComponent ShootComponent { get; }
        protected abstract IReloadComponent ReloadComponent { get; }

        public IReadonlyRxField<float> ReloadProgress => ReloadComponent.ReloadProgress;

        private float _previousShootTime;

        public void Shoot()
        {
            if (ShootComponent.CanShoot())
            {
                ShootComponent.Shoot();
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