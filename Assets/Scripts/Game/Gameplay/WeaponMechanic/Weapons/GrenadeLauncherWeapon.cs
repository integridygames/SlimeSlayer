﻿using Game.DataBase.Weapon;
using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Services;
using Game.Gameplay.Views.Weapons;
using Game.Gameplay.WeaponMechanic.WeaponComponents.ReloadComponents;
using Game.Gameplay.WeaponMechanic.WeaponComponents.ShootComponents;
using Game.Gameplay.WeaponMechanic.WeaponComponents.ShootPossibilityComponents;

namespace Game.Gameplay.WeaponMechanic.Weapons
{
    public class GrenadeLauncherWeapon : WeaponBase
    {
        public sealed override WeaponType WeaponType => WeaponType.GrenadeLauncher;

        protected override IShootComponent ShootComponent { get; }

        protected override IReloadComponent ReloadComponent { get; }

        protected override IShootPossibilityComponent ShootPossibilityComponent { get; }

        public GrenadeLauncherWeapon(GrenadeLauncherView grenadeLauncherView,
            WeaponMechanicsService weaponMechanicsService,
            CurrentCharacterWeaponsData currentCharacterWeaponsData)
        {
            ShootComponent =
                new GrenadeShootComponent(weaponMechanicsService, currentCharacterWeaponsData.WeaponsCharacteristics,
                    ProjectileType.Grenade, WeaponType, grenadeLauncherView.ShootingPoint);

            ReloadComponent =
                new CommonReloadComponent(currentCharacterWeaponsData.WeaponsCharacteristics, WeaponType);

            ShootPossibilityComponent = new FireRatePossibilityComponent(
                currentCharacterWeaponsData.WeaponsCharacteristics, weaponMechanicsService, WeaponType,
                grenadeLauncherView.ShootingPoint);
        }
    }
}