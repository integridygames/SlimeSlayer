using Game.Gameplay.Models.Character;

namespace Game.Gameplay.AbilitiesMechanics
{
    public class HealthOneTimeRecoveryAbility : AbilityBase
    {
        private readonly CharacterCharacteristicsRepository _characterCharacteristicsRepository;

        public HealthOneTimeRecoveryAbility(CharacterCharacteristicsRepository characterCharacteristicsRepository)
        {
            _characterCharacteristicsRepository = characterCharacteristicsRepository;
        }

        public override void OnStart()
        {
            _characterCharacteristicsRepository.AddHealth(_characterCharacteristicsRepository.MaxHealth);
        }
    }
}