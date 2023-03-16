using System;
using Game.DataBase;
using Game.DataBase.Essence;
using Game.Gameplay.Models.Character;
using Game.Gameplay.Views.Character;
using TegridyCore.Base;

namespace Game.Gameplay.Controllers.Essence
{
    public class EssenceImageViewsController : ControllerBase<CharacterEssenceView>, IInitializeSystem, IDisposable
    {
        private readonly CharacterEssencesData _characterEssencesData;

        public EssenceImageViewsController(CharacterEssenceView controlledEntity, CharacterEssencesData characterEssencesData) : base(controlledEntity)
        {
            _characterEssencesData = characterEssencesData;
        }

        public void Initialize()
        {
            _characterEssencesData.OnEssenceQuantityChanged += OnEssenceQuantityChangedHandler;
        }

        public void Dispose()
        {
            _characterEssencesData.OnEssenceQuantityChanged -= OnEssenceQuantityChangedHandler;
        }

        private void OnEssenceQuantityChangedHandler(EssenceType essenceType, int newQuantity)
        {
            if (ControlledEntity.EssenceImageViewsByType.TryGetValue(essenceType, out var essenceImageView))
            {
                essenceImageView.Quantity.text = newQuantity.ToString();
            }
        }
    }
}