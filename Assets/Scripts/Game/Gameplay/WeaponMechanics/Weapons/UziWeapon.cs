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
    public class UziWeapon : WeaponBase
    {
        public sealed override WeaponType WeaponType => WeaponType.Uzi;

        protected override IShootComponent ShootComponent { get; }
        protected override IReloadComponent ReloadComponent { get; }
        protected override IShootPossibilityComponent ShootPossibilityComponent { get; }

        protected override Transform ShootingPoint { get; }

        public UziWeapon(UziView uziView, PlayerWeaponData playerWeaponData,
            WeaponMechanicsService weaponMechanicsService,
            CurrentCharacterWeaponsData currentCharacterWeaponsData) : base(uziView, playerWeaponData)
        {
            ShootComponent =
                new BulletShootComponent(uziView, weaponMechanicsService, ProjectileType.CommonBullet, playerWeaponData, uziView.ShootingPoint);

            ReloadComponent =
                new CommonReloadComponent(currentCharacterWeaponsData.WeaponsCharacteristics, playerWeaponData);

            ShootPossibilityComponent = new FireRatePossibilityComponent(
                currentCharacterWeaponsData.WeaponsCharacteristics, weaponMechanicsService,
                playerWeaponData, uziView.ShootingPoint);

            ShootingPoint = uziView.ShootingPoint;
        }
    }
}