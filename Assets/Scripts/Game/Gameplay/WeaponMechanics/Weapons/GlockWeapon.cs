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
    public class GlockWeapon : WeaponBase
    {
        public sealed override WeaponType WeaponType => WeaponType.Glock;

        protected override IShootComponent ShootComponent { get; }
        protected override IReloadComponent ReloadComponent { get; }
        protected override IShootPossibilityComponent ShootPossibilityComponent { get; }

        protected override Transform ShootingPoint { get; }

        public GlockWeapon(GlockView glockView, WeaponData weaponData, WeaponMechanicsService weaponMechanicsService,
            CurrentCharacterWeaponsData currentCharacterWeaponsData) : base(glockView, weaponData)
        {
            ShootComponent =
                new BulletShootComponent(glockView, weaponMechanicsService, ProjectileType.CommonBullet, WeaponType,
                    weaponData._rarityType, glockView.ShootingPoint);

            ReloadComponent =
                new CommonReloadComponent(currentCharacterWeaponsData.WeaponsCharacteristics, WeaponType, weaponData._rarityType);

            ShootPossibilityComponent = new FireRatePossibilityComponent(
                currentCharacterWeaponsData.WeaponsCharacteristics, weaponMechanicsService, WeaponType,
                weaponData._rarityType, glockView.ShootingPoint);

            ShootingPoint = glockView.ShootingPoint;
        }
    }
}