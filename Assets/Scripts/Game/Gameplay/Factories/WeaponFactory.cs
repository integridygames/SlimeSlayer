using System;
using Game.DataBase.Weapon;
using Game.Gameplay.Views.Character.Placers;
using Game.Gameplay.Views.Weapons;
using Game.Gameplay.WeaponMechanics;
using Game.Gameplay.WeaponMechanics.Weapons;
using Zenject;
using Object = UnityEngine.Object;

namespace Game.Gameplay.Factories
{
    public class WeaponFactory : IFactory<WeaponData, WeaponPlacer, bool, WeaponBase>
    {
        private readonly DiContainer _container;
        private readonly WeaponsDataBase _weaponsDataBase;

        public WeaponFactory(DiContainer container, WeaponsDataBase weaponsDataBase)
        {
            _container = container;
            _weaponsDataBase = weaponsDataBase;
        }

        public WeaponBase Create(WeaponData weaponData, WeaponPlacer weaponPlacer, bool isLeftHand)
        {
            var weaponRecord = _weaponsDataBase.GetRecordByType(weaponData._weaponType);
            var weaponView = Object.Instantiate(weaponRecord._weaponPrefab, weaponPlacer.transform);

            if (isLeftHand)
            {
                FlipWeapon(weaponView);
            }

            switch (weaponData._weaponType)
            {
                case WeaponType.Glock:
                    return CreateWeapon<GlockWeapon>(weaponView, weaponData);
                case WeaponType.Shotgun:
                    return CreateWeapon<ShotgunWeapon>(weaponView, weaponData);
                case WeaponType.GrenadeLauncher:
                    return CreateWeapon<GrenadeLauncherWeapon>(weaponView, weaponData);
                case WeaponType.MiniGun:
                    return CreateWeapon<MiniGunWeapon>(weaponView, weaponData);
                case WeaponType.Scar:
                    return CreateWeapon<ScarWeapon>(weaponView, weaponData);
                case WeaponType.SniperRiffle:
                    return CreateWeapon<SniperRiffleWeapon>(weaponView, weaponData);
                case WeaponType.Uzi:
                    return CreateWeapon<UziWeapon>(weaponView, weaponData);
                default:
                    throw new ArgumentOutOfRangeException(nameof(weaponData._weaponType), weaponData._weaponType, null);
            }
        }

        private static void FlipWeapon(WeaponViewBase weaponView)
        {
            var transform = weaponView.transform;

            var transformLocalScale = transform.localScale;
            transformLocalScale.x *= -1;
            transform.localScale = transformLocalScale;
        }

        private T CreateWeapon<T>(WeaponViewBase weaponView, WeaponData weaponData) where T : WeaponBase
        {
            var weaponBase = _container.Instantiate<T>(new object[] {weaponView, weaponData});
            return weaponBase;
        }
    }
}