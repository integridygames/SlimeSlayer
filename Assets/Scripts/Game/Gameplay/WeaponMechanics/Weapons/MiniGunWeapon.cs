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
    public class MiniGunWeapon : WeaponBase
    {
        public sealed override WeaponType WeaponType => WeaponType.MiniGun;

        protected override IShootComponent ShootComponent { get; }
        protected override IReloadComponent ReloadComponent { get; }
        protected override IShootPossibilityComponent ShootPossibilityComponent { get; }

        protected override Transform ShootingPoint { get; }

        public MiniGunWeapon(MiniGunView miniGunView, PlayerWeaponData playerWeaponData,
            WeaponMechanicsService weaponMechanicsService,
            WeaponsCharacteristics weaponsCharacteristics,
            WeaponsCharacteristicsRepository weaponsCharacteristicsRepository,
            CharacterCharacteristicsRepository characterCharacteristicsRepository) : base(miniGunView, playerWeaponData)
        {
            ShootComponent =
                new BulletShootComponent(miniGunView, weaponMechanicsService, ProjectileType.LargeBullet,
                    weaponsCharacteristicsRepository, playerWeaponData, miniGunView.ShootingPoint);

            ReloadComponent =
                new CommonReloadComponent(weaponsCharacteristics, playerWeaponData);

            ShootPossibilityComponent = new FireRatePossibilityComponent(
                weaponsCharacteristics, weaponMechanicsService,
                playerWeaponData, miniGunView.ShootingPoint, characterCharacteristicsRepository);

            ShootingPoint = miniGunView.ShootingPoint;
        }
    }
}