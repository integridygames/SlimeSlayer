using Game.DataBase.Weapon;
using Game.Gameplay.Views.Weapons;
using TegridyCore;
using UnityEngine;

namespace Game.Gameplay.WeaponMechanics
{
    public abstract class WeaponBase
    {
        public abstract WeaponType WeaponType { get; }

        protected abstract IShootComponent ShootComponent { get; }
        protected abstract IReloadComponent ReloadComponent { get; }
        protected abstract IShootPossibilityComponent ShootPossibilityComponent { get; }

        protected abstract Transform ShootingPoint { get; }

        public IReadonlyRxField<float> ReloadProgress => ReloadComponent.ReloadProgress;

        private float _previousShootTime;

        public PlayerWeaponData Data { get; }
        public WeaponViewBase WeaponViewBase { get; }

        protected WeaponBase(WeaponViewBase weaponViewBase, PlayerWeaponData playerWeaponData)
        {
            WeaponViewBase = weaponViewBase;
            Data = playerWeaponData;
        }

        public void Reset()
        {
            ReloadComponent.Reset();
        }

        public void Shoot()
        {
            if (ShootPossibilityComponent.TryToGetTargetCollider(out var currentTarget))
            {
                var direction = (currentTarget.Position - ShootingPoint.position).normalized;

                ShootComponent.Shoot(direction);
                ShootPossibilityComponent.HandleShoot();
                ReloadComponent.CurrentCharge.Value--;
            }
        }

        public bool NeedReload()
        {
            return ReloadComponent.NeedReload();
        }

        public void ProcessReload()
        {
            ReloadComponent.Reload();
        }

        public void Destroy()
        {
            Object.Destroy(WeaponViewBase.gameObject);
        }
    }
}