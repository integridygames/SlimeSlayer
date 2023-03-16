using Game.DataBase;
using Game.Gameplay.Models;
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

        public LevelFactory(LevelsDataBase levelsDataBase, ApplicationData applicationData)
        {
            _levelsDataBase = levelsDataBase;
            _applicationData = applicationData;
        }

        public LevelView Create()
        {
            var levelViewPrefab = _levelsDataBase.GetLevelPrefabByIndex(_applicationData.PlayerData.CurrentLevel);
            var levelView = Object.Instantiate(levelViewPrefab);

            return levelView;
        }
    }
}