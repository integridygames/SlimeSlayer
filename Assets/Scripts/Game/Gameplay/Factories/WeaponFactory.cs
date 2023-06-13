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
    public class WeaponFactory : IFactory<PlayerWeaponData, WeaponPlacer, bool, WeaponBase>
    {
        private readonly DiContainer _container;
        private readonly WeaponsDataBase _weaponsDataBase;

        public WeaponFactory(DiContainer container, WeaponsDataBase weaponsDataBase)
        {
            _container = container;
            _weaponsDataBase = weaponsDataBase;
        }

        public WeaponBase Create(PlayerWeaponData playerWeaponData, WeaponPlacer weaponPlacer, bool isLeftHand)
        {
            var weaponRecord = _weaponsDataBase.GetRecordByType(playerWeaponData._weaponType);
            var weaponView = Object.Instantiate(weaponRecord._weaponPrefab, weaponPlacer.transform);

            if (isLeftHand)
            {
                FlipWeapon(weaponView);
            }

            switch (playerWeaponData._weaponType)
            {
                case WeaponType.Glock:
                    return CreateWeapon<GlockWeapon>(weaponView, playerWeaponData);
                case WeaponType.Shotgun:
                    return CreateWeapon<ShotgunWeapon>(weaponView, playerWeaponData);
                case WeaponType.GrenadeLauncher:
                    return CreateWeapon<GrenadeLauncherWeapon>(weaponView, playerWeaponData);
                case WeaponType.MiniGun:
                    return CreateWeapon<MiniGunWeapon>(weaponView, playerWeaponData);
                case WeaponType.Scar:
                    return CreateWeapon<ScarWeapon>(weaponView, playerWeaponData);
                case WeaponType.SniperRiffle:
                    return CreateWeapon<SniperRiffleWeapon>(weaponView, playerWeaponData);
                case WeaponType.Uzi:
                    return CreateWeapon<UziWeapon>(weaponView, playerWeaponData);
                default:
                    throw new ArgumentOutOfRangeException(nameof(playerWeaponData._weaponType), playerWeaponData._weaponType, null);
            }
        }

        private static void FlipWeapon(WeaponViewBase weaponView)
        {
            var transform = weaponView.transform;

            var transformLocalScale = transform.localScale;
            transformLocalScale.x *= -1;
            transform.localScale = transformLocalScale;
        }

        private T CreateWeapon<T>(WeaponViewBase weaponView, PlayerWeaponData playerWeaponData) where T : WeaponBase
        {
            var weaponBase = _container.Instantiate<T>(new object[] {weaponView, playerWeaponData});
            return weaponBase;
        }
    }
}