using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Services;
using Game.Gameplay.Views.Weapons.Pistols;
using Game.Gameplay.WeaponMechanic.WeaponComponents.ReloadComponents;
using Game.Gameplay.WeaponMechanic.WeaponComponents.ShootComponents;

namespace Game.Gameplay.WeaponMechanic.Weapons
{
    public class PistolWeapon : WeaponBase
    {
        private readonly BulletShootComponent _bulletShootComponent;
        private readonly CommonReloadComponent _commonReloadComponent;

        public PistolWeapon(PistolView pistolView, WeaponMechanicsService weaponMechanicsService,
            CurrentCharacterWeaponsData currentCharacterWeaponsData)
        {
            _bulletShootComponent =
                new BulletShootComponent(weaponMechanicsService, currentCharacterWeaponsData.WeaponsCharacteristics, WeaponType, pistolView.ShootingPoint);
            _commonReloadComponent =
                new CommonReloadComponent(currentCharacterWeaponsData.WeaponsCharacteristics, WeaponType.Pistol);
        }

        public sealed override WeaponType WeaponType => WeaponType.Pistol;

        protected override IShootComponent ShootComponent => _bulletShootComponent;

        protected override IReloadComponent ReloadComponent => _commonReloadComponent;
    }
}