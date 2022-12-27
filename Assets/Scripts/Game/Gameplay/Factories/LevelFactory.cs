using Game.DataBase;
using Game.Gameplay.Models;
using Game.Gameplay.Models.Level;
using Game.Gameplay.Views.Level;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Factories
{
    [UsedImplicitly]
    public class LevelFactory : IFactory<LevelView>
    {
        private readonly LevelsDataBase _levelsDataBase;
        private readonly ApplicationData _applicationData;
        private readonly LevelInfo _levelInfo;

        public LevelFactory(LevelsDataBase levelsDataBase, ApplicationData applicationData, LevelInfo levelInfo)
        {
            _levelsDataBase = levelsDataBase;
            _applicationData = applicationData;
            _levelInfo = levelInfo;
        }

        public LevelView Create()
        {
            var levelViewPrefab = _levelsDataBase.GetLevelPrefabByIndex(_applicationData.PlayerData.CurrentLevel);
            var levelView = Object.Instantiate(levelViewPrefab);

            return levelView;
        }

        public LevelView NextLevel(out bool doesNextLevelExist)
        {
            int nextLevelIndex = _applicationData.PlayerData.CurrentLevel + 1;

            doesNextLevelExist = _levelsDataBase.CheckIfLevelExistsByIndex(nextLevelIndex);

            switch (doesNextLevelExist) 
            {
                case true:
                    return CreateByIndex(nextLevelIndex);
                case false:
                    return null;
            }            
        }

        public LevelView CreateByIndex(int index) 
        {
            var levelViewPrefab = _levelsDataBase.GetLevelPrefabByIndex(index);
            var levelView = Object.Instantiate(levelViewPrefab, _levelInfo.DistanceBetweenLevels, Quaternion.identity);

            return levelView;
        }
    }
}