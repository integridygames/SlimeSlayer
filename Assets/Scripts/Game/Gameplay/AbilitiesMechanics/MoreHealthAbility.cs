using Game.Gameplay.Models.Character;

namespace Game.Gameplay.AbilitiesMechanics
{
    public class MoreHealthAbility : AbilityBase
    {
        private readonly CharacterCharacteristicsRepository _characterCharacteristicsRepository;

        public MoreHealthAbility(CharacterCharacteristicsRepository characterCharacteristicsRepository)
        {
            _characterCharacteristicsRepository = characterCharacteristicsRepository;
        }

        public override void OnStart()
        {
            _characterCharacteristicsRepository.AddHealth(0);
        }
    }
}