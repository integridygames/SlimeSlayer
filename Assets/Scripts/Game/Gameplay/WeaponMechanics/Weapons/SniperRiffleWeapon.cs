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
    public class SniperRiffleWeapon : WeaponBase
    {
        public sealed override WeaponType WeaponType => WeaponType.SniperRiffle;

        protected override IShootComponent ShootComponent { get; }
        protected override IReloadComponent ReloadComponent { get; }
        protected override IShootPossibilityComponent ShootPossibilityComponent { get; }

        protected override Transform ShootingPoint { get; }

        public SniperRiffleWeapon(SniperRiffleView sniperRiffleView, PlayerWeaponData playerWeaponData, WeaponsCharacteristicsRepository weaponsCharacteristicsRepository,
            WeaponMechanicsService weaponMechanicsService, WeaponsCharacteristics weaponsCharacteristics) : base(sniperRiffleView, playerWeaponData)
        {
            ShootComponent =
                new BulletShootComponent(sniperRiffleView, weaponMechanicsService,
                    ProjectileType.LongBullet, weaponsCharacteristicsRepository, playerWeaponData, sniperRiffleView.ShootingPoint);

            ReloadComponent =
                new CommonReloadComponent(weaponsCharacteristics, playerWeaponData);

            ShootPossibilityComponent = new FireRatePossibilityComponent(
                weaponsCharacteristics, weaponMechanicsService,
                playerWeaponData, sniperRiffleView.ShootingPoint);

            ShootingPoint = sniperRiffleView.ShootingPoint;
        }
    }
}