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
        private readonly CharacterEssencesData _characterEssenceData;

        public EssencesController(ActiveEssencesContainer controlledEntity, EssencePoolFactory essencePoolFactory, CharacterEssencesData characterEssenceData) : base(controlledEntity)
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
            ControlledEntity.RemoveEssence(essenceView);
            _essencePoolFactory.RecycleElement(essenceView, essenceView.EssenceType);
          
            _characterEssenceData.CharacterEssences[essenceView.EssenceType].
                Quantity += essenceView.Quantity;
            _characterEssenceData.CharacterEssences[essenceView.EssenceType].
                EssenceImageView.QuantityTMPText.text = _characterEssenceData.CharacterEssences[essenceView.EssenceType].
                Quantity.ToString();                              
        }
    }
}