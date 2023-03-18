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
    public class WeaponFactory : IFactory<WeaponType, WeaponPlacer, bool, WeaponBase>
    {
        private readonly DiContainer _container;
        private readonly WeaponsDataBase _weaponsDataBase;

        public WeaponFactory(DiContainer container, WeaponsDataBase weaponsDataBase)
        {
            _container = container;
            _weaponsDataBase = weaponsDataBase;
        }

        public WeaponBase Create(WeaponType weaponType, WeaponPlacer weaponPlacer, bool isLeftHand)
        {
            var weaponRecord = _weaponsDataBase.GetRecordByType(weaponType);
            var weaponView = Object.Instantiate(weaponRecord._weaponPrefab, weaponPlacer.transform);

            if (isLeftHand)
            {
                var transform = weaponView.transform;

                var transformLocalScale = transform.localScale;
                transformLocalScale.x *= -1;
                transform.localScale = transformLocalScale;
            }


            switch (weaponType)
            {
                case WeaponType.Glock:
                    return CreateWeapon<GlockWeapon>(weaponView);
                case WeaponType.Shotgun:
                    return CreateWeapon<ShotgunWeapon>(weaponView);
                case WeaponType.GrenadeLauncher:
                    return CreateWeapon<GrenadeLauncherWeapon>(weaponView);
                case WeaponType.MiniGun:
                    return CreateWeapon<MiniGunWeapon>(weaponView);
                case WeaponType.Scar:
                    return CreateWeapon<ScarWeapon>(weaponView);
                case WeaponType.SniperRiffle:
                    return CreateWeapon<SniperRiffleWeapon>(weaponView);
                case WeaponType.Uzi:
                    return CreateWeapon<UziWeapon>(weaponView);
                default:
                    throw new ArgumentOutOfRangeException(nameof(weaponType), weaponType, null);
            }
        }

        private T CreateWeapon<T>(WeaponViewBase weaponView) where T : WeaponBase
        {
            return _container.Instantiate<T>(new object[] {weaponView});
        }
    }
}