using System;
using Game.Gameplay.Utils.Essences;
using System.Collections.Generic;

namespace Game.Gameplay.Models.Character 
{
    public class CharacterEssencesData
    {
        public event Action<EssenceType, int> OnEssenceQuantityChanged;

        private readonly Dictionary<EssenceType, int> _characterEssences = new();

        public int GetEssenceQuantity(EssenceType essenceType)
        {
            CreateEssenceIfNeed(essenceType);

            return _characterEssences[essenceType];
        }

        public void AddEssence(EssenceType essenceType, int count)
        {
            CreateEssenceIfNeed(essenceType);

            _characterEssences[essenceType] += count;

            OnEssenceQuantityChanged?.Invoke(essenceType, _characterEssences[essenceType]);
        }

        public void RemoveEssence(EssenceType essenceType, int count)
        {
            CreateEssenceIfNeed(essenceType);

            _characterEssences[essenceType] -= count;

            OnEssenceQuantityChanged?.Invoke(essenceType, _characterEssences[essenceType]);
        }

        private void CreateEssenceIfNeed(EssenceType essenceType)
        {
            if (_characterEssences.ContainsKey(essenceType) == false)
            {
                _characterEssences[essenceType] = 0;
            }
        }
    }   
}