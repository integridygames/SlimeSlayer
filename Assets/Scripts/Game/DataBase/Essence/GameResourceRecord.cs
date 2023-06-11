using System;
using Game.Gameplay.Views.GameResources;

namespace Game.DataBase.Essence
{
    [Serializable]
    public class GameResourceRecord
    {
        public GameResourceType _gameResourceType;
        public GameResourceViewBase _gameResourceView;
    }
}