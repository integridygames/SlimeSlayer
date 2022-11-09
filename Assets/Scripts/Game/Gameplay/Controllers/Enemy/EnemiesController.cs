using Game.Gameplay.Factories;
using Game.Gameplay.Models.Enemy;
using Game.Gameplay.Models.Essence;
using System;
using Game.DataBase.Essence;
using Game.Gameplay.EnemiesMechanics;
using TegridyCore.Base;
using Zenject;

namespace Game.Gameplay.Controllers.Enemy 
{
    public class EnemiesController : ControllerBase<ActiveEnemiesContainer>, IInitializable, IDisposable
    {
        private readonly EssencePoolFactory _essencePoolFactory;
        private readonly ActiveEssencesContainer _activeEssencesContainer;

        public EnemiesController(ActiveEnemiesContainer controlledEntity, EssencePoolFactory essencePoolFactory, 
            ActiveEssencesContainer activeEssencesContainer) : base(controlledEntity)
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

        private void OnEnemyDiedHandler(EssenceType essenceType, EnemyBase enemyBase)
        {
            var essenceView =_essencePoolFactory.GetElement(essenceType);
            essenceView.transform.position = enemyBase.Position;
            _activeEssencesContainer.AddEssence(essenceView);
        }
    }   
}