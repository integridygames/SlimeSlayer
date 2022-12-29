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

        public void NextLevel(bool needLevelInfoActualization)
        {
            if (needLevelInfoActualization)
                ActualizeLevelInfo();

            int nextLevelIndex = _applicationData.PlayerData.CurrentLevel + 1;

            bool doesNextLevelExist = _levelsDataBase.CheckIfLevelExistsByIndex(nextLevelIndex);
            _levelInfo.DoesNextLevelExist = doesNextLevelExist;

            switch (doesNextLevelExist) 
            {
                case true:
                    _levelInfo.NextLevelView.Value =  CreateByIndex(nextLevelIndex);
                    break;
                case false:
                    _levelInfo.NextLevelView.Value = null;
                    break;
            }            
        }

        public LevelView CreateByIndex(int index) 
        {
            var levelViewPrefab = _levelsDataBase.GetLevelPrefabByIndex(index);
            var levelView = Object.Instantiate(levelViewPrefab, _levelInfo.ActualNextLevelPosition, Quaternion.identity);

            return levelView;
        }

        private void ActualizeLevelInfo()
        {
            _levelInfo.CurrentLevelView.Value = _levelInfo.NextLevelView.Value;
            _applicationData.PlayerData.CurrentLevel++;
            SetNextLevelPosition();
        }

        private void SetNextLevelPosition()
        {
            float x = _levelInfo.ActualNextLevelPosition.x + _levelInfo.DistanceBetweenNearestLevels.x;
            float y = _levelInfo.ActualNextLevelPosition.y + _levelInfo.DistanceBetweenNearestLevels.y;
            float z = _levelInfo.ActualNextLevelPosition.z + _levelInfo.DistanceBetweenNearestLevels.z;
            _levelInfo.ActualNextLevelPosition = new Vector3(x, y, z);
        }
    }
}