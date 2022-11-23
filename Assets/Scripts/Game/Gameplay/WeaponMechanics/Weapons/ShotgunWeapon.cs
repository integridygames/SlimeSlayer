using Game.DataBase.FX;
using Game.DataBase.Weapon;
using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Services;
using Game.Gameplay.Views.Weapons;
using Game.Gameplay.WeaponMechanics.Components.ReloadComponents;
using Game.Gameplay.WeaponMechanics.Components.ShootComponents;
using Game.Gameplay.WeaponMechanics.Components.ShootPossibilityComponents;

namespace Game.Gameplay.WeaponMechanics.Weapons
{
    public class ShotgunWeapon : WeaponBase
    {
        public sealed override WeaponType WeaponType => WeaponType.Shotgun;

        protected override IShootComponent ShootComponent { get; }
        protected override IReloadComponent ReloadComponent { get; }
        protected override IShootPossibilityComponent ShootPossibilityComponent { get; }

        public ShotgunWeapon(ShotgunView shotgunView, WeaponMechanicsService weaponMechanicsService,
            CurrentCharacterWeaponsData currentCharacterWeaponsData)
        {
            ShootComponent = new ParticlesShootComponent(RecyclableParticleType.ShotgunProjectiles,
                WeaponType, shotgunView.ShootingPoint, weaponMechanicsService);

            ReloadComponent =
                new CommonReloadComponent(currentCharacterWeaponsData.WeaponsCharacteristics, WeaponType);

            ShootPossibilityComponent = new FireRatePossibilityComponent(
                currentCharacterWeaponsData.WeaponsCharacteristics, weaponMechanicsService, WeaponType,
                shotgunView.ShootingPoint);
        }
    }
}