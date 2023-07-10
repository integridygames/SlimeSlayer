using Game.Gameplay.Models.Abilities;
using TegridyCore.Base;

namespace Game.Gameplay.Systems.Abilities
{
    public class AbilitiesExecutionSystem : IUpdateSystem
    {
        private readonly AbilitiesRepository _abilitiesRepository;

        public AbilitiesExecutionSystem(AbilitiesRepository abilitiesRepository)
        {
            _abilitiesRepository = abilitiesRepository;
        }

        public void Update()
        {
            foreach (var ability in _abilitiesRepository.ActiveAbilities)
            {
                if (ability.EnableToExecute())
                {
                    ability.Execute();
                }
            }
        }
    }
}