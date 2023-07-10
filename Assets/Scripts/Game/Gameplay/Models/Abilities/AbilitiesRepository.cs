using System.Collections.Generic;
using Game.Gameplay.AbilitiesMechanics;

namespace Game.Gameplay.Models.Abilities
{
    public class AbilitiesRepository
    {
        public List<AbilityBase> ActiveAbilities { get; private set; }
    }
}