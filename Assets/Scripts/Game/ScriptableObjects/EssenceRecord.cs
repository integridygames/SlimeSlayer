using Game.Gameplay.Utils.Essences;
using Game.Gameplay.Views.Essence;
using Game.ScriptableObjects.Substructure;
using System;
using UnityEngine;

namespace Game.ScriptableObjects 
{
    [Serializable]
    public class EssenceRecord : Record<EssenceType, EssenceView>
    {
        public Color EssenceColor;
    }   
}