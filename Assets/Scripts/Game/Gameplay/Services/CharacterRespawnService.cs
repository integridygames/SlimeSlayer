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
        private readonly CharacterStats _characterStats;

        public CharacterRespawnService(LevelInfo levelInfo, CharacterView characterView, CharacterHealthData characterHealthData, CharacterStats characterStats)
        {
            _levelInfo = levelInfo;
            _characterView = characterView;
            _characterHealthData = characterHealthData;
            _characterStats = characterStats;
        }

        public void GoToSpawnPoint()
        {
            _characterHealthData.CurrentHealth.Value = _characterStats.MaxHealth;
            _characterView.transform.position = _levelInfo.CurrentLevelView.Value.SpawnPointView.transform.position;
        }
    }
}