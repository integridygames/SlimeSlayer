using System.Collections.Generic;
using Game.Gameplay.AbilitiesMechanics;

namespace Game.Gameplay.Models.Abilities
{
    public class AbilitiesRepository
    {
        private readonly List<AbilityBase> _activeAbilities = new();
        public IReadOnlyList<AbilityBase> ActiveAbilities => _activeAbilities;

        public HashSet<AbilityType> ActiveAbilitiesSet { get; } = new();
        public Dictionary<AbilityType, AbilityBase> ActiveAbilitiesDict { get; } = new();

        public void AddAbility(AbilityType abilityType, AbilityBase abilityBase)
        {
            _activeAbilities.Add(abilityBase);
            ActiveAbilitiesDict[abilityType] = abilityBase;
            ActiveAbilitiesSet.Add(abilityType);
        }
    }
}