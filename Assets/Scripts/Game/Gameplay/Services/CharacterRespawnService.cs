using Game.DataBase.Character;
using Game.Gameplay.Models.Character;
using Game.Gameplay.Models.Level;
using Game.Gameplay.Views.Character;

namespace Game.Gameplay.Services
{
    public class CharacterRespawnService
    {
        private readonly LevelInfo _levelInfo;
        private readonly CharacterView _characterView;
        private readonly CharacterHealthData _characterHealthData;
        private readonly CharacterCharacteristics _characterCharacteristics;

        public CharacterRespawnService(LevelInfo levelInfo, CharacterView characterView, CharacterHealthData characterHealthData, CharacterCharacteristics characterCharacteristics)
        {
            _levelInfo = levelInfo;
            _characterView = characterView;
            _characterHealthData = characterHealthData;
            _characterCharacteristics = characterCharacteristics;
        }

        public void GoToSpawnPoint()
        {
            _characterHealthData.CurrentHealth.Value = (int) _characterCharacteristics.GetCharacteristic(CharacterCharacteristicType.MaxHealth);
            _characterHealthData.MaxHealth = _characterHealthData.CurrentHealth.Value;
            _characterView.transform.position = _levelInfo.CurrentLevelView.Value.SpawnPointView.transform.position;
        }
    }
}