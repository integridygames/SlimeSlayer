﻿using Game.DataBase.Weapon;
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
    public class ScarWeapon : WeaponBase
    {
        public sealed override WeaponType WeaponType => WeaponType.Scar;

        protected override IShootComponent ShootComponent { get; }
        protected override IReloadComponent ReloadComponent { get; }
        protected override IShootPossibilityComponent ShootPossibilityComponent { get; }

        protected override Transform ShootingPoint { get; }

        public ScarWeapon(ScarView scarView, PlayerWeaponData playerWeaponData,
            WeaponMechanicsService weaponMechanicsService, WeaponsCharacteristicsRepository weaponsCharacteristicsRepository,
            WeaponsCharacteristics weaponsCharacteristics,
            CharacterCharacteristicsRepository characterCharacteristicsRepository) : base(scarView, playerWeaponData)
        {
            ShootComponent =
                new BulletShootComponent(scarView, weaponMechanicsService, ProjectileType.CommonBullet,
                    weaponsCharacteristicsRepository, playerWeaponData, scarView.ShootingPoint);

            ReloadComponent =
                new CommonReloadComponent(weaponsCharacteristics, playerWeaponData, characterCharacteristicsRepository);

            ShootPossibilityComponent = new FireRatePossibilityComponent(
                weaponsCharacteristics, weaponMechanicsService,
                playerWeaponData, scarView.ShootingPoint, characterCharacteristicsRepository);

            ShootingPoint = scarView.ShootingPoint;
        }
    }
}