using Game.Gameplay.Models.Level;
using Game.Gameplay.Views.Character;

namespace Game.Gameplay.Services
{
    public class CharacterRespawnService
    {
        private readonly LevelInfo _levelInfo;
        private readonly CharacterView _characterView;

        public CharacterRespawnService(LevelInfo levelInfo, CharacterView characterView)
        {
            _levelInfo = levelInfo;
            _characterView = characterView;
        }

        public void GoToSpawnPoint()
        {
            _characterView.transform.position = _levelInfo.CurrentLevelView.Value.SpawnPointView.transform.position;
        }
    }
}