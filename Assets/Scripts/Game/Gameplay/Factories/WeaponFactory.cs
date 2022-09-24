using System;
using Game.Gameplay.Models.Weapon;
using Game.Gameplay.Views.Character.Placers;
using Game.Gameplay.WeaponMechanic;
using Game.Gameplay.WeaponMechanic.Weapons;
using Game.ScriptableObjects.Weapon;
using Zenject;
using Object = UnityEngine.Object;

namespace Game.Gameplay.Factories
{
    public class WeaponFactory : IFactory<WeaponType, WeaponPlacer, IWeapon>
    {
        private readonly DiContainer _container;
        private readonly WeaponsDataBase _weaponsDataBase;

        public WeaponFactory(DiContainer container, WeaponsDataBase weaponsDataBase)
        {
            _container = container;
            _weaponsDataBase = weaponsDataBase;
        }

        public IWeapon Create(WeaponType weaponType, WeaponPlacer weaponPlacer)
        {
            var weaponRecord = _weaponsDataBase.GetRecordByType(weaponType);
            var weaponView = Object.Instantiate(weaponRecord._weaponPrefab, weaponPlacer.transform);

            switch (weaponType)
            {
                case WeaponType.Pistol:
                    return _container.Instantiate<PistolWeapon>(new object[] {weaponView});
                default:
                    throw new ArgumentOutOfRangeException(nameof(weaponType), weaponType, null);
            }
        }
    }
}