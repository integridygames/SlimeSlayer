using System;
using System.Collections.Generic;
using Game.DataBase.GameResource;

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

            OnResourceQuantityChanged?.Invoke(gameResourceType, count);
        }

        public void AddResource(GameResourceType gameResourceType, int count)
        {
            CreateEssenceIfNeed(gameResourceType);

            _characterResources[gameResourceType] += count;

            OnResourceQuantityChanged?.Invoke(gameResourceType, count);
        }

        public void RemoveResource(GameResourceType gameResourceType, int count)
        {
            CreateEssenceIfNeed(gameResourceType);

            _characterResources[gameResourceType] -= count;

            OnResourceQuantityChanged?.Invoke(gameResourceType, count);
        }

        private void CreateEssenceIfNeed(GameResourceType gameResourceType)
        {
            _characterResources.TryAdd(gameResourceType, 0);
        }
    }   
}