using System;
using Game.DataBase;
using Game.DataBase.Essence;

namespace Game.Gameplay.Models.Zone 
{
    [Serializable]
    public class ZoneTransitEssenceData
    {
        public EssenceType EssenceType;
        public int Quantity;
    }
}