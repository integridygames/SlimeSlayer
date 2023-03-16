using Game.Gameplay.Factories;
using Game.Gameplay.Models.Character;
using Game.Gameplay.Models.Essence;
using Game.Gameplay.Views.Essence;
using System;
using TegridyCore.Base;
using Zenject;

namespace Game.Gameplay.Controllers.Essence 
{
    public class ActiveEssencesController : ControllerBase<ActiveEssencesContainer>, IInitializable, IDisposable
    {
        private readonly EssencePoolFactory _essencePoolFactory;
        private readonly CharacterEssencesData _characterEssenceData;

        public ActiveEssencesController(ActiveEssencesContainer controlledEntity, EssencePoolFactory essencePoolFactory, CharacterEssencesData characterEssenceData) : base(controlledEntity)
        {
            _essencePoolFactory = essencePoolFactory;
            _characterEssenceData = characterEssenceData;
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
            _characterEssenceData.AddEssence(essenceView.EssenceType, essenceView.Quantity);

            ControlledEntity.RemoveEssence(essenceView);
            _essencePoolFactory.RecycleElement(essenceView.EssenceType, essenceView);
        }
    }
}