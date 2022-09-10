using Game.Gameplay.Factories;
using Game.Gameplay.Models.Character;
using Game.Gameplay.Models.Essence;
using Game.Gameplay.Views.Essence;
using System;
using TegridyCore.Base;
using Zenject;

namespace Game.Gameplay.Controllers.Essence 
{
    public class EssencesController : ControllerBase<ActiveEssencesContainer>, IInitializable, IDisposable
    {
        private readonly EssencePoolFactory _essencePoolFactory;
        private readonly CharacterEssencesData _characterEssenceInfo;

        public EssencesController(ActiveEssencesContainer controlledEntity, EssencePoolFactory essencePoolFactory, CharacterEssencesData characterEssenceInfo) : base(controlledEntity)
        {
            _essencePoolFactory = essencePoolFactory;
            _characterEssenceInfo = characterEssenceInfo;
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
            ControlledEntity.RemoveEssence(essenceView);
            _essencePoolFactory.RecycleElement(essenceView, essenceView.EssenceType);
            _characterEssenceInfo.CharacterEssences[essenceView.EssenceType] += essenceView.Quantity;
        }
    }
}