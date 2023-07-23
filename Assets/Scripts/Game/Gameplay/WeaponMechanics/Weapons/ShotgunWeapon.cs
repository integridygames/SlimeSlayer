using Game.DataBase.FX;
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
    public class ShotgunWeapon : WeaponBase
    {
        public sealed override WeaponType WeaponType => WeaponType.Shotgun;

        protected override IShootComponent ShootComponent { get; }
        protected override IReloadComponent ReloadComponent { get; }
        protected override IShootPossibilityComponent ShootPossibilityComponent { get; }

        protected override Transform ShootingPoint { get; }

        public ShotgunWeapon(ShotgunView shotgunView, PlayerWeaponData playerWeaponData,
            WeaponMechanicsService weaponMechanicsService, WeaponsCharacteristicsRepository weaponsCharacteristicsRepository,
            WeaponsCharacteristics weaponsCharacteristics) : base(shotgunView, playerWeaponData)
        {
            ShootComponent = new ParticlesShootComponent(shotgunView, RecyclableParticleType.ShotgunProjectiles,
                playerWeaponData, weaponsCharacteristicsRepository, shotgunView.ShootingPoint, weaponMechanicsService);

            ReloadComponent =
                new CommonReloadComponent(weaponsCharacteristics, playerWeaponData);

            ShootPossibilityComponent = new FireRatePossibilityComponent(
                weaponsCharacteristics, weaponMechanicsService,
                playerWeaponData, shotgunView.ShootingPoint);

            ShootingPoint = shotgunView.ShootingPoint;
        }
    }
}