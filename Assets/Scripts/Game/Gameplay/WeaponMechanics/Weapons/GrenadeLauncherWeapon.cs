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

        public GrenadeLauncherWeapon(GrenadeLauncherView grenadeLauncherView, WeaponData weaponData,
            WeaponMechanicsService weaponMechanicsService,
            CurrentCharacterWeaponsData currentCharacterWeaponsData) : base(grenadeLauncherView, weaponData)
        {
            ShootComponent = new GrenadeShootComponent(grenadeLauncherView, weaponMechanicsService,
                ProjectileType.Grenade, WeaponType, weaponData._rarityType, grenadeLauncherView.ShootingPoint);

            ReloadComponent =
                new CommonReloadComponent(currentCharacterWeaponsData.WeaponsCharacteristics, WeaponType, weaponData._rarityType);

            ShootPossibilityComponent = new FireRatePossibilityComponent(
                currentCharacterWeaponsData.WeaponsCharacteristics, weaponMechanicsService, WeaponType,
                weaponData._rarityType, grenadeLauncherView.ShootingPoint);

            ShootingPoint = grenadeLauncherView.ShootingPoint;
        }
    }
}