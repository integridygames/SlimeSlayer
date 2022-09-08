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
        private readonly CharacterEssencesInfo _characterEssenceInfo;

        public EssencesController(ActiveEssencesContainer controlledEntity, EssencePoolFactory essencePoolFactory, CharacterEssencesInfo characterEssenceInfo) : base(controlledEntity)
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
            _essencePoolFactory.RecycleEssence(essenceView);

            foreach(var characterEssence in _characterEssenceInfo.CharacterEssences) 
            {
                if (characterEssence.EssenceType == essenceView.EssenceType)
                    characterEssence.Quantity += essenceView.Quantity;
            }
        }
    }
}