using System;
using Game.Gameplay.Views.Essence;

namespace Game.DataBase
{
    [Serializable]
    public class EssenceRecord
    {
        public EssenceType _essenceType;
        public EssenceView _essenceView;
    }
}