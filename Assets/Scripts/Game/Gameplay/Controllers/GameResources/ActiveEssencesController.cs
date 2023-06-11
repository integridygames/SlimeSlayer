using System;
using Game.Gameplay.Factories;
using Game.Gameplay.Models.Character;
using Game.Gameplay.Models.GameResources;
using Game.Gameplay.Views.GameResources;
using TegridyCore.Base;
using Zenject;

namespace Game.Gameplay.Controllers.GameResources
{
    public class ActiveEssencesController : ControllerBase<ActiveEssencesContainer>, IInitializable, IDisposable
    {
        private readonly GameResourcePoolFactory _gameResourcePoolFactory;
        private readonly GameResourceData _gameResourceData;

        public ActiveEssencesController(ActiveEssencesContainer controlledEntity, GameResourcePoolFactory gameResourcePoolFactory, GameResourceData gameResourceData) : base(controlledEntity)
        {
            _gameResourcePoolFactory = gameResourcePoolFactory;
            _gameResourceData = gameResourceData;
        }

        public void Initialize()
        {
            ControlledEntity.OnEssenceCollide += OnEssenceCollideHandler;
        }

        public void Dispose()
        {
            ControlledEntity.OnEssenceCollide -= OnEssenceCollideHandler;
        }

        private void OnEssenceCollideHandler(EssenceView essenceView)
        {
            essenceView.MovingProgress = 0;
            essenceView.IsMovingToCharacter = false;

            _gameResourceData.AddResource(essenceView.GameResourceType, 1);
            ControlledEntity.Remove(essenceView);
            _gameResourcePoolFactory.RecycleElement(essenceView.GameResourceType, essenceView);
        }
    }
}