using Game.Gameplay.Models.Character;
using Game.Gameplay.Services;
using TegridyCore.Base;

namespace Game.Gameplay.Systems.Character
{
    public class CharacterSpawnSystem : IInitializeSystem
    {
        private readonly CharacterRespawnService _characterRespawnService;
        private readonly CharacterCharacteristicsRepository _characterCharacteristicsRepository;

        public CharacterSpawnSystem(CharacterRespawnService characterRespawnService, CharacterCharacteristicsRepository characterCharacteristicsRepository)
        {
            _characterRespawnService = characterRespawnService;
            _characterCharacteristicsRepository = characterCharacteristicsRepository;
        }

        public void Initialize()
        {
            _characterRespawnService.GoToSpawnPoint();
            _characterCharacteristicsRepository.UpdateCharacteristics();
        }
    }
}