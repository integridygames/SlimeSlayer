using Game.DataBase.Character;
using Game.DataBase.Weapon;
using Game.Gameplay.Models.Weapon;

namespace Game.Gameplay.Models.Character
{
    public class WeaponsCharacteristicsRepository
    {
        private readonly WeaponsCharacteristics _weaponsCharacteristics;
        private readonly CharacterCharacteristics _characterCharacteristics;

        public WeaponsCharacteristicsRepository(WeaponsCharacteristics weaponsCharacteristics, CharacterCharacteristics characterCharacteristics)
        {
            _weaponsCharacteristics = weaponsCharacteristics;
            _characterCharacteristics = characterCharacteristics;
        }

        public float GetDamage(PlayerWeaponData playerWeaponData)
        {
            var weaponDamageModificator = _weaponsCharacteristics.GetCharacteristic(playerWeaponData, WeaponCharacteristicType.DamageModificator);
            var baseDamage = _characterCharacteristics.GetCharacteristic(CharacterCharacteristicType.BaseDamage);

            return baseDamage + baseDamage * weaponDamageModificator / 100f;
        }
    }
}