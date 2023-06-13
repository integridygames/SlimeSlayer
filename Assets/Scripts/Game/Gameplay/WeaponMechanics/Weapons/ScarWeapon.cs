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
    public class ScarWeapon : WeaponBase
    {
        public sealed override WeaponType WeaponType => WeaponType.Scar;

        protected override IShootComponent ShootComponent { get; }
        protected override IReloadComponent ReloadComponent { get; }
        protected override IShootPossibilityComponent ShootPossibilityComponent { get; }

        protected override Transform ShootingPoint { get; }

        public ScarWeapon(ScarView scarView, PlayerWeaponData playerWeaponData,
            WeaponMechanicsService weaponMechanicsService,
            CurrentCharacterWeaponsData currentCharacterWeaponsData) : base(scarView, playerWeaponData)
        {
            ShootComponent =
                new BulletShootComponent(scarView, weaponMechanicsService, ProjectileType.CommonBullet,
                    playerWeaponData, scarView.ShootingPoint);

            ReloadComponent =
                new CommonReloadComponent(currentCharacterWeaponsData.WeaponsCharacteristics, playerWeaponData);

            ShootPossibilityComponent = new FireRatePossibilityComponent(
                currentCharacterWeaponsData.WeaponsCharacteristics, weaponMechanicsService,
                playerWeaponData, scarView.ShootingPoint);

            ShootingPoint = scarView.ShootingPoint;
        }
    }
}