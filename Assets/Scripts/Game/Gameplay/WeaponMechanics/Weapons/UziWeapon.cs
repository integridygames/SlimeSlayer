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

        public UziWeapon(UziView uziView, WeaponData weaponData, WeaponMechanicsService weaponMechanicsService,
            CurrentCharacterWeaponsData currentCharacterWeaponsData) : base(uziView, weaponData)
        {
            ShootComponent =
                new BulletShootComponent(uziView, weaponMechanicsService, ProjectileType.CommonBullet, WeaponType, uziView.ShootingPoint);

            ReloadComponent =
                new CommonReloadComponent(currentCharacterWeaponsData.WeaponsCharacteristics, WeaponType);

            ShootPossibilityComponent = new FireRatePossibilityComponent(
                currentCharacterWeaponsData.WeaponsCharacteristics, weaponMechanicsService, WeaponType,
                uziView.ShootingPoint);

            ShootingPoint = uziView.ShootingPoint;
        }
    }
}