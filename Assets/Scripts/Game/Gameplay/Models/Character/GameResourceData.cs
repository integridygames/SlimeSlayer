using System;
using System.Collections.Generic;
using System.Linq;
using Game.DataBase.Essence;

namespace Game.Gameplay.Models.Character 
{
    public class GameResourceData
    {
        public event Action<GameResourceType, int> OnResourceQuantityChanged;

        private readonly Dictionary<GameResourceType, int> _characterResources = new();

        public int GetResourceQuantity(GameResourceType gameResourceType)
        {
            CreateEssenceIfNeed(gameResourceType);

            return _characterResources[gameResourceType];
        }

        public void SetResource(GameResourceType gameResourceType, int count)
        {
            CreateEssenceIfNeed(gameResourceType);

            _characterResources[gameResourceType] = count;

            OnResourceQuantityChanged?.Invoke(gameResourceType, _characterResources[gameResourceType]);
        }

        public void AddResource(GameResourceType gameResourceType, int count)
        {
            CreateEssenceIfNeed(gameResourceType);

            _characterResources[gameResourceType] += count;

            OnResourceQuantityChanged?.Invoke(gameResourceType, _characterResources[gameResourceType]);
        }

        public void RemoveResource(GameResourceType gameResourceType, int count)
        {
            CreateEssenceIfNeed(gameResourceType);

            _characterResources[gameResourceType] -= count;

            OnResourceQuantityChanged?.Invoke(gameResourceType, _characterResources[gameResourceType]);
        }

        private void CreateEssenceIfNeed(GameResourceType gameResourceType)
        {
            if (_characterResources.ContainsKey(gameResourceType) == false)
            {
                _characterResources[gameResourceType] = 0;
            }
        }

        public void ClearAll()
        {
            var essenceTypes = _characterResources.Keys.ToList();

            foreach (var essencesType in essenceTypes)
            {
                _characterResources[essencesType] = 0;
                OnResourceQuantityChanged?.Invoke(essencesType, 0);
            }
        }
    }   
}