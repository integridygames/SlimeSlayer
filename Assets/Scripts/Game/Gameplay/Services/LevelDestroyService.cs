using Game.Gameplay.Models.Character;
using Game.Gameplay.Models.Enemy;
using Game.Gameplay.Models.GameResources;
using Game.Gameplay.Models.Level;
using UnityEngine;

namespace Game.Gameplay.Services
{
    public class LevelDestroyService
    {
        private readonly LevelInfo _levelInfo;
        private readonly ActiveEnemiesContainer _activeEnemiesContainer;
        private readonly CharacterMovementData _characterMovementData;
        private readonly ActiveCoinsContainer _activeCoinsContainer;
        private readonly ActiveEssencesContainer _activeEssencesContainer;

        public LevelDestroyService(LevelInfo levelInfo, ActiveEnemiesContainer activeEnemiesContainer,
            CharacterMovementData characterMovementData, ActiveCoinsContainer activeCoinsContainer,
            ActiveEssencesContainer activeEssencesContainer)
        {
            _levelInfo = levelInfo;
            _activeEnemiesContainer = activeEnemiesContainer;
            _characterMovementData = characterMovementData;
            _activeCoinsContainer = activeCoinsContainer;
            _activeEssencesContainer = activeEssencesContainer;
        }

        public void DestroyLevel()
        {
            Time.timeScale = 1;
            _activeEnemiesContainer.Clear();
            _characterMovementData.Reset();
            _activeEssencesContainer.Clear();
            _activeCoinsContainer.Clear();
            Object.Destroy(_levelInfo.CurrentLevelView.Value.gameObject);
        }
    }
}