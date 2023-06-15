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

        public GlockWeapon(GlockView glockView, PlayerWeaponData playerWeaponData, WeaponMechanicsService weaponMechanicsService,
            WeaponsCharacteristics weaponsCharacteristics) : base(glockView, playerWeaponData)
        {
            ShootComponent =
                new BulletShootComponent(glockView, weaponMechanicsService, ProjectileType.CommonBullet, playerWeaponData, glockView.ShootingPoint);

            ReloadComponent =
                new CommonReloadComponent(weaponsCharacteristics, playerWeaponData);

            ShootPossibilityComponent = new FireRatePossibilityComponent(
                weaponsCharacteristics, weaponMechanicsService,
                playerWeaponData, glockView.ShootingPoint);

            ShootingPoint = glockView.ShootingPoint;
        }
    }
}