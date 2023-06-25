using Game.Gameplay.Models.Character;
using Game.Gameplay.Models.Level;
using Game.Gameplay.Views.Character;

namespace Game.Gameplay.Services
{
    public class CharacterRespawnService
    {
        private readonly LevelInfo _levelInfo;
        private readonly CharacterView _characterView;
        private readonly CharacterCharacteristicsRepository _characterCharacteristicsRepository;

        public CharacterRespawnService(LevelInfo levelInfo, CharacterView characterView, CharacterCharacteristicsRepository characterCharacteristicsRepository)
        {
            _levelInfo = levelInfo;
            _characterView = characterView;
            _characterCharacteristicsRepository = characterCharacteristicsRepository;
        }

        public void GoToSpawnPoint()
        {
            _characterCharacteristicsRepository.UpdateCharacteristics();
            _characterView.transform.position = _levelInfo.CurrentLevelView.Value.SpawnPointView.transform.position;
        }
    }
}