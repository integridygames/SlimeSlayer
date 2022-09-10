using Game.Gameplay.Factories;
using Game.Gameplay.Models.Enemy;
using Game.Gameplay.Models.Essence;
using Game.Gameplay.Utils.Essences;
using Game.Gameplay.Views.Enemy;
using System;
using TegridyCore.Base;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Controllers.Enemy 
{
    public class EnemiesController : ControllerBase<ActiveEnemiesContainer>, IInitializable, IDisposable
    {
        private readonly EssencePoolFactory _essencePoolFactory;
        private readonly ActiveEssencesContainer _activeEssencesContainer;

        public EnemiesController(ActiveEnemiesContainer controlledEntity, EssencePoolFactory essencePoolFactory, ActiveEssencesContainer activeEssencesContainer) : base(controlledEntity)
        {
            _essencePoolFactory = essencePoolFactory;
            _activeEssencesContainer = activeEssencesContainer;
        }

        public void Initialize()
        {
            ControlledEntity.OnEnemyDied += OnEnemyDiedHandler;
        }

        public void Dispose()
        {
            ControlledEntity.OnEnemyDied -= OnEnemyDiedHandler;
        }

        private void OnEnemyDiedHandler(int quantity, Vector3 position, EssenceType essenceType, EnemyView enemyView) 
        {
            ControlledEntity.RemoveEnemy(enemyView);
            var essenceView =_essencePoolFactory.TakeNextEssence(essenceType);
            essenceView.SetQuantity(quantity);
            essenceView.transform.position = position;
            essenceView.gameObject.SetActive(true);
            _activeEssencesContainer.AddEssence(essenceView);
        }
    }   
}