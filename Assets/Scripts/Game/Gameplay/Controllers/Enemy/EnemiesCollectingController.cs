using Game.DataBase.Essence;
using Game.Gameplay.Factories;
using Game.Gameplay.Models.Character;
using Game.Gameplay.Models.Enemy;
using Game.Gameplay.Models.Level;
using Game.Gameplay.Views.Character;
using Game.Gameplay.Views.Level;
using System;
using TegridyCore;
using TegridyCore.Base;
using Zenject;

namespace Game.Gameplay.Controllers.Enemy 
{
    public class EnemiesCollectingController : ControllerBase<LevelInfo>, IInitializable, IDisposable
    {
        private readonly ActiveEnemiesContainer _activeEnemiesContainer;
        private readonly EnemyFactory _enemyFactory;
        private readonly CharacterView _characterView;
        private readonly CharacterHealthData _characterHealthData;

        public EnemiesCollectingController(LevelInfo levelInfo, ActiveEnemiesContainer activeEnemiesContainer, 
            EnemyFactory enemyFactory, CharacterView ñharacterView, 
             CharacterHealthData characterHealthData) : base(levelInfo) 
        {
            _activeEnemiesContainer = activeEnemiesContainer;
            _enemyFactory = enemyFactory;
            _characterHealthData = characterHealthData;
            _characterView = ñharacterView;
        }

        public void Initialize()
        {
            ControlledEntity.CurrentLevelView.OnUpdate += CollectEnemiesOnLevel;
        }

        public void Dispose()
        {
            ControlledEntity.CurrentLevelView.OnUpdate -= CollectEnemiesOnLevel;
        }

        private void CollectEnemiesOnLevel(RxValue<LevelView> rxValue) 
        {
            TryToRemovePrevious();

            CreateEnemiesDataAndPutInContainer(rxValue.NewValue);
        }

        private void TryToRemovePrevious() 
        {
            if (_activeEnemiesContainer.ActiveEnemies != null && 
                _activeEnemiesContainer.ActiveEnemies.Count > 0)
            {
                _activeEnemiesContainer.RemoveAll();
            }
        }

        private void CreateEnemiesDataAndPutInContainer(LevelView levelView) 
        {
            foreach(var enemy in levelView.EnemyViews) 
            {
                var enemyData =_enemyFactory.CreateWithoutSpawning(enemy, _characterView, _characterHealthData);
                _activeEnemiesContainer.AddEnemy(enemyData, 0);
            }
        }
    }
}