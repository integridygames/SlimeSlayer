using System.Collections.Generic;
using Game.DataBase.Abilities;
using Game.Gameplay.Models.Weapon;
using TegridyUtils.Extensions;

namespace Game.Gameplay.Models.Abilities
{
    public class AbilitiesDistributorService
    {
        public const int MaxAbilitiesForLevel = 3;

        private readonly AbilitiesDataBase _abilitiesDataBase;
        private readonly AbilitiesRepository _abilitiesRepository;
        private readonly CharacterWeaponsRepository _characterWeaponsRepository;

        public AbilitiesDistributorService(AbilitiesDataBase abilitiesDataBase, AbilitiesRepository abilitiesRepository,
            CharacterWeaponsRepository characterWeaponsRepository)
        {
            _abilitiesDataBase = abilitiesDataBase;
            _abilitiesRepository = abilitiesRepository;
            _characterWeaponsRepository = characterWeaponsRepository;
        }

        public List<AbilityRecord> GetAbilitiesForNextLevel()
        {
            var result = new List<AbilityRecord>(MaxAbilitiesForLevel);
            var randomRecords = _abilitiesDataBase.Records.GetRandomRange();

            foreach (var abilityRecord in randomRecords)
            {
                var hasNeededAbility = true;
                var hasNeededWeapon = true;

                if (abilityRecord.AbilityNeedsSet.Count > 0)
                {
                    hasNeededAbility = HasNeededAbility(abilityRecord);
                }

                if (abilityRecord.WeaponNeedsSet.Count > 0)
                {
                    hasNeededWeapon = HasNeededWeapon(abilityRecord);
                }

                if ((hasNeededAbility && hasNeededWeapon) == false)
                {
                    continue;
                }

                if (AlreadyReachedMaxLevel(abilityRecord))
                {
                    continue;
                }

                result.Add(abilityRecord);

                if (result.Count == 3)
                {
                    break;
                }
            }

            return result;
        }

        private bool HasNeededAbility(AbilityRecord abilityRecord)
        {
            return abilityRecord.AbilityNeedsSet.Overlaps(_abilitiesRepository.ActiveAbilitiesSet);
        }

        private bool HasNeededWeapon(AbilityRecord abilityRecord)
        {
            return abilityRecord.WeaponNeedsSet
                .Contains(_characterWeaponsRepository.CurrentWeaponViewLeft.Value.WeaponType) || abilityRecord.WeaponNeedsSet
                .Contains(_characterWeaponsRepository.CurrentWeaponViewRight.Value.WeaponType);
        }

        private bool AlreadyReachedMaxLevel(AbilityRecord abilityRecord)
        {
            return _abilitiesRepository.ActiveAbilitiesDict.TryGetValue(abilityRecord.AbilityType,
                out var abilityBase) && abilityBase.Level >= abilityRecord.MaxLevel;
        }
    }
}