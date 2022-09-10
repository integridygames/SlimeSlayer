using Game.Gameplay.Utils.Essences;
using Game.Gameplay.Views.Essence;
using System;

namespace Game.ScriptableObjects 
{
    [Serializable]
    public class EssenceRecord
    {
        public EssenceType EssenceType;
        public EssenceView EssenceViewPrefab;
    }   
}