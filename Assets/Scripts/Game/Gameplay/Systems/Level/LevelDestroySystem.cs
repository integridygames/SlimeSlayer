using Game.Gameplay.Models.Character;
using Game.Gameplay.Models.Enemy;
using Game.Gameplay.Models.GameResources;
using Game.Gameplay.Models.Level;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Systems.Level
{
    public class LevelDestroySystem : IDestroySystem
    {
        private readonly LevelInfo _levelInfo;
        private readonly ActiveEnemiesContainer _activeEnemiesContainer;
        private readonly CharacterMovementData _characterMovementData;
        private readonly ActiveCoinsContainer _activeCoinsContainer;
        private readonly ActiveEssencesContainer _activeEssencesContainer;

        public LevelDestroySystem(LevelInfo levelInfo, ActiveEnemiesContainer activeEnemiesContainer,
            CharacterMovementData characterMovementData, ActiveCoinsContainer activeCoinsContainer,
            ActiveEssencesContainer activeEssencesContainer)
        {
            _levelInfo = levelInfo;
            _activeEnemiesContainer = activeEnemiesContainer;
            _characterMovementData = characterMovementData;
            _activeCoinsContainer = activeCoinsContainer;
            _activeEssencesContainer = activeEssencesContainer;
        }

        public void Destroy()
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