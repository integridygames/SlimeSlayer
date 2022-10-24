using System;
using Game.Gameplay.Views.Essence;

namespace Game.DataBase.Essence
{
    [Serializable]
    public class EssenceRecord
    {
        public EssenceType _essenceType;
        public EssenceView _essenceView;
    }
}