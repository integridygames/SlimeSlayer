using Game.DataBase.FX;
using Game.DataBase.Weapon;
using Game.Gameplay.Factories;
using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Services;
using Game.Gameplay.Views.Weapons;
using Game.Gameplay.WeaponMechanic.WeaponComponents.ReloadComponents;
using Game.Gameplay.WeaponMechanic.WeaponComponents.ShootComponents;
using Game.Gameplay.WeaponMechanic.WeaponComponents.ShootPossibilityComponents;

namespace Game.Gameplay.WeaponMechanic.Weapons
{
    public class ShotgunWeapon : WeaponBase
    {
        public sealed override WeaponType WeaponType => WeaponType.Shotgun;

        protected override IShootComponent ShootComponent { get; }
        protected override IReloadComponent ReloadComponent { get; }
        protected override IShootPossibilityComponent ShootPossibilityComponent { get; }

        public ShotgunWeapon(ShotgunView shotgunView, WeaponMechanicsService weaponMechanicsService,
            CurrentCharacterWeaponsData currentCharacterWeaponsData,
            RecyclableParticlesPoolFactory recyclableParticlesPoolFactory)
        {
            ShootComponent = new ParticlesShootComponent(recyclableParticlesPoolFactory,
                currentCharacterWeaponsData.WeaponsCharacteristics, RecyclableParticleType.ShotgunProjectiles,
                WeaponType, shotgunView.ShootingPoint);

            ReloadComponent =
                new CommonReloadComponent(currentCharacterWeaponsData.WeaponsCharacteristics, WeaponType);

            ShootPossibilityComponent = new FireRatePossibilityComponent(
                currentCharacterWeaponsData.WeaponsCharacteristics, weaponMechanicsService, WeaponType,
                shotgunView.ShootingPoint);
        }
    }
}