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
        private readonly ActiveEnemiesContainer _activeEnemiesContainer;

        public EnemiesController(ActiveEnemiesContainer controlledEntity, EssencePoolFactory essencePoolFactory,
            ActiveEssencesContainer activeEssencesContainer, ActiveEnemiesContainer activeEnemiesContainer) : base(controlledEntity)
        {
            _essencePoolFactory = essencePoolFactory;
            _activeEssencesContainer = activeEssencesContainer;
            _activeEnemiesContainer = activeEnemiesContainer;
        }

        public void Initialize()
        {
            ControlledEntity.OnEnemyDied += OnEnemyDiedHandler;
        }

        public void Dispose()
        {
            ControlledEntity.OnEnemyDied -= OnEnemyDiedHandler;
        }

        private void OnEnemyDiedHandler(EssenceType essenceType, EnemyBase enemy)
        {
            _activeEnemiesContainer.RemoveEnemy(enemy, enemy.ZoneId);

            var essenceView = _essencePoolFactory.GetElement(essenceType);
            essenceView.transform.position = enemy.Position;
            _activeEssencesContainer.AddEssence(essenceView);

            enemy.Remove();
        }
    }
}