using Game.DataBase.Essence;
using Game.DataBase.GameResource;
using Game.Gameplay.Views.GameResources;
using Game.Gameplay.Views.Pool;
using UnityEngine;

namespace Game.Gameplay.Factories 
{
    public class GameResourcePoolFactory : PoolFactoryBase<GameResourceViewBase, GameResourceType>
    {
        private readonly PoolContainerView _poolContainerView;
        private readonly GameResourcesDataBase _gameResourcesDataBase;

        public GameResourcePoolFactory(PoolContainerView poolContainerView, GameResourcesDataBase gameResourcesDataBase)
        {
            _poolContainerView = poolContainerView;
            _gameResourcesDataBase = gameResourcesDataBase;
        }

        protected override GameResourceViewBase CreateNewElement(GameResourceType key)
        {
            var essenceRecord = _gameResourcesDataBase.GetRecordByType(key);

            return Object.Instantiate(essenceRecord._gameResourceView, _poolContainerView.transform);
        }
    }
}