using Game.DataBase.Weapon;
using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Services;
using Game.Gameplay.Views.Weapons;
using Game.Gameplay.WeaponMechanics.Components.ReloadComponents;
using Game.Gameplay.WeaponMechanics.Components.ShootComponents;
using Game.Gameplay.WeaponMechanics.Components.ShootPossibilityComponents;
using UnityEngine;

namespace Game.Gameplay.WeaponMechanics.Weapons
{
    public class GrenadeLauncherWeapon : WeaponBase
    {
        public sealed override WeaponType WeaponType => WeaponType.GrenadeLauncher;

        protected override IShootComponent ShootComponent { get; }

        protected override IReloadComponent ReloadComponent { get; }

        protected override IShootPossibilityComponent ShootPossibilityComponent { get; }

        protected override Transform ShootingPoint { get; }

        public GrenadeLauncherWeapon(GrenadeLauncherView grenadeLauncherView, PlayerWeaponData playerWeaponData,
            WeaponMechanicsService weaponMechanicsService,
            CurrentCharacterWeaponsData currentCharacterWeaponsData) : base(grenadeLauncherView, playerWeaponData)
        {
            ShootComponent = new GrenadeShootComponent(grenadeLauncherView, weaponMechanicsService,
                ProjectileType.Grenade, playerWeaponData, grenadeLauncherView.ShootingPoint);

            ReloadComponent =
                new CommonReloadComponent(currentCharacterWeaponsData.WeaponsCharacteristics, playerWeaponData);

            ShootPossibilityComponent = new FireRatePossibilityComponent(
                currentCharacterWeaponsData.WeaponsCharacteristics, weaponMechanicsService,
                playerWeaponData, grenadeLauncherView.ShootingPoint);

            ShootingPoint = grenadeLauncherView.ShootingPoint;
        }
    }
}