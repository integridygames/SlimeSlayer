using Game.DataBase.Weapon;
using Game.Gameplay.Models.Character;
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
            WeaponsCharacteristicsRepository weaponsCharacteristicsRepository,
            WeaponsCharacteristics weaponsCharacteristics) : base(uziView, playerWeaponData)
        {
            ShootComponent =
                new BulletShootComponent(uziView, weaponMechanicsService, ProjectileType.CommonBullet,
                    weaponsCharacteristicsRepository, playerWeaponData, uziView.ShootingPoint);

            ReloadComponent =
                new CommonReloadComponent(weaponsCharacteristics, playerWeaponData);

            ShootPossibilityComponent = new FireRatePossibilityComponent(
                weaponsCharacteristics, weaponMechanicsService,
                playerWeaponData, uziView.ShootingPoint);

            ShootingPoint = uziView.ShootingPoint;
        }
    }
}