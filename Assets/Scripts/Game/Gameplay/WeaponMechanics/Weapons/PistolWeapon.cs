using Game.DataBase.Weapon;
using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Services;
using Game.Gameplay.Views.Weapons;
using Game.Gameplay.WeaponMechanics.WeaponComponents.ReloadComponents;
using Game.Gameplay.WeaponMechanics.WeaponComponents.ShootComponents;
using Game.Gameplay.WeaponMechanics.WeaponComponents.ShootPossibilityComponents;

namespace Game.Gameplay.WeaponMechanics.Weapons
{
    public class PistolWeapon : WeaponBase
    {
        public sealed override WeaponType WeaponType => WeaponType.Pistol;

        protected override IShootComponent ShootComponent { get; }
        protected override IReloadComponent ReloadComponent { get; }
        protected override IShootPossibilityComponent ShootPossibilityComponent { get; }

        public PistolWeapon(PistolView pistolView, WeaponMechanicsService weaponMechanicsService,
            CurrentCharacterWeaponsData currentCharacterWeaponsData)
        {
            ShootComponent =
                new BulletShootComponent(weaponMechanicsService, currentCharacterWeaponsData.WeaponsCharacteristics, ProjectileType.CommonBullet, WeaponType, pistolView.ShootingPoint);

            ReloadComponent =
                new CommonReloadComponent(currentCharacterWeaponsData.WeaponsCharacteristics, WeaponType);

            ShootPossibilityComponent = new FireRatePossibilityComponent(
                currentCharacterWeaponsData.WeaponsCharacteristics, weaponMechanicsService, WeaponType,
                pistolView.ShootingPoint);
        }
    }
}