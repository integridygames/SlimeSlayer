using Game.Gameplay.Utils.Essences;
using System;
using UnityEngine;

namespace Game.Gameplay.Models.Zone 
{
    [Serializable]
    public class ZoneTransitEssenceData
    {
        public EssenceType EssenceType;
        public int Quantity;
        public Color EssenceColor;
    }
}