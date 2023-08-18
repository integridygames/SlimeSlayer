using Game.Gameplay.Models.Character;
using Game.Gameplay.Models.Enemy;
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

        public LevelDestroySystem(LevelInfo levelInfo, ActiveEnemiesContainer activeEnemiesContainer, CharacterMovementData characterMovementData)
        {
            _levelInfo = levelInfo;
            _activeEnemiesContainer = activeEnemiesContainer;
            _characterMovementData = characterMovementData;
        }
        
        public void Destroy()
        {
            Time.timeScale = 1;
            _activeEnemiesContainer.Clear();
            _characterMovementData.Reset();
            Object.Destroy(_levelInfo.CurrentLevelView.Value.gameObject);
        }
    }
}