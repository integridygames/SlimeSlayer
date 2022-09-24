using System;
using Game.Gameplay.Utils.Essences;
using Game.Gameplay.Views.Essence;

namespace Game.ScriptableObjects 
{
    [Serializable]
    public class EssenceRecord
    {
        public EssenceType _essenceType;
        public EssenceView _essenceView;
    }   
}