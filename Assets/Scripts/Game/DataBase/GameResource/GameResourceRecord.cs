using System;
using Game.DataBase.Essence;
using Game.Gameplay.Views.GameResources;
using TegridyUtils.Attributes;

namespace Game.DataBase.GameResource
{
    [Serializable]
    public class GameResourceRecord
    {
        [ArrayKey]
        public GameResourceType _gameResourceType;
        public GameResourceViewBase _gameResourceView;
    }
}